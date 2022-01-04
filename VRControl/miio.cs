using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

public class MIIOHead
{
    public UInt16 magic = 0x2131;
    public UInt16 length = 0;
    public UInt32 unk_data = 0x0;
    public UInt32 DID = 0;
    public UInt32 time = 0;
    public byte[] token = new byte[16];

    public MIIOHead(string token)
    {
        for (int i = 0; i < 16; i++)
            this.token[i] = (Convert.ToByte(token.Substring(i * 2, 2), 16));
    }
    public byte[] Serialize()
    {
        byte[] magic = BitConverter.GetBytes(this.magic);
        byte[] length = BitConverter.GetBytes(this.length);
        byte[] unk_data = BitConverter.GetBytes(this.unk_data);
        byte[] DID = BitConverter.GetBytes(this.DID);
        byte[] time = BitConverter.GetBytes(this.time);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(magic);
            Array.Reverse(length);
            Array.Reverse(unk_data);
            Array.Reverse(DID);
            Array.Reverse(time);
        }
        byte[] rv = new byte[32];
        System.Buffer.BlockCopy(magic, 0, rv, 0, 2);
        System.Buffer.BlockCopy(length, 0, rv, 2, 2);
        System.Buffer.BlockCopy(unk_data, 0, rv, 4, 4);
        System.Buffer.BlockCopy(DID, 0, rv, 8, 4);
        System.Buffer.BlockCopy(time, 0, rv, 12, 4);
        System.Buffer.BlockCopy(this.token, 0, rv, 16, 16);
        return rv;
    }

    public MIIOHead DeSerialize(byte[] data, bool withToken = false)
    {
        this.magic = BitConverter.ToUInt16(data);
        this.length = BitConverter.ToUInt16(data.Skip(2).ToArray());
        this.unk_data = BitConverter.ToUInt32(data.Skip(4).ToArray());
        this.DID = BitConverter.ToUInt32(data.Skip(8).ToArray());
        this.time = BitConverter.ToUInt32(data.Skip(12).ToArray());
        if (withToken)
        {
            System.Buffer.BlockCopy(data, 0, this.token, 16, 16);
        }
        if (BitConverter.IsLittleEndian)
        {
            this.magic = (UInt16)((magic & 0xFFU) << 8 | (magic & 0xFF00U) >> 8);
            this.length = (UInt16)((length & 0xFFU) << 8 | (length & 0xFF00U) >> 8);
            this.unk_data = (this.unk_data & 0x000000FFU) << 24 | (this.unk_data & 0x0000FF00U) << 8 | (this.unk_data & 0x00FF0000U) >> 8 | (this.unk_data & 0xFF000000U) >> 24;
            this.DID = (this.DID & 0x000000FFU) << 24 | (this.DID & 0x0000FF00U) << 8 | (this.DID & 0x00FF0000U) >> 8 | (this.DID & 0xFF000000U) >> 24;
            this.time = (this.time & 0x000000FFU) << 24 | (this.time & 0x0000FF00U) << 8 | (this.time & 0x00FF0000U) >> 8 | (this.time & 0xFF000000U) >> 24;
        }
        return this;
    }
}

public class MIIOSession
{
    private MIIOHead mIIOHead;
    private Aes aes = Aes.Create();
    private UdpClient client = new UdpClient();
    private IPEndPoint iPEndPoint;

    private byte[] md5(byte[] data)
    {
        byte[] hash;
        using (MD5 md5 = MD5.Create())
        {
            hash = md5.ComputeHash(data);
        }
        return hash;
    }

    private byte[] md5(string data) => md5(System.Text.Encoding.ASCII.GetBytes(data));

    public MIIOSession(string ip, string token)
    {
        // Initialize MIIOHead Object
        mIIOHead = new MIIOHead(token);
        // Initialize Encrypter
        byte[] bToken = new byte[16];
        byte[] bSalted = new byte[32];
        for (int i = 0; i < 16; i++)
            bToken[i] = (Convert.ToByte(token.Substring(i * 2, 2), 16));
        aes.BlockSize = 128;
        aes.KeySize = 128;
        aes.Key = md5(bToken);
        System.Buffer.BlockCopy(aes.Key, 0, bSalted, 0, 16);
        System.Buffer.BlockCopy(bToken, 0, bSalted, 16, 16);
        aes.IV = md5(bSalted);
        aes.Mode = CipherMode.CBC;

        // Initialize UDP Client
        this.client.Client.SendTimeout = 1000;
        this.client.Client.ReceiveTimeout = 1000;
        this.iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 54321);
        this.client.Connect(this.iPEndPoint);
    }

    public byte[] send(byte[] data)
    {
        byte[] encData = this.aes.EncryptCbc(data, this.aes.IV, PaddingMode.PKCS7);
        byte[] buf = new byte[encData.Length + 32];
        // Send Hello Packet
        this.client.Send(new byte[] { 0x21, 0x31, 0x00, 0x20, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff });
        // Receive and deserialize the packet head
        mIIOHead.DeSerialize(this.client.Receive(ref this.iPEndPoint));
        mIIOHead.time += 1;
        mIIOHead.length = (UInt16)buf.Length;
        // Construct the Packet
        System.Buffer.BlockCopy(mIIOHead.Serialize(), 0, buf, 0, 32);
        System.Buffer.BlockCopy(encData, 0, buf, 32, encData.Length);
        System.Buffer.BlockCopy(md5(buf), 0, buf, 16, 16); // Overwrite the checksum value
        this.client.Send(buf);

        byte[] recv = this.client.Receive(ref this.iPEndPoint);
        mIIOHead.DeSerialize(recv);
        return this.aes.DecryptCbc(recv.Skip(32).ToArray(), this.aes.IV, PaddingMode.PKCS7);
    }
    public byte[] send(string data) => send(System.Text.Encoding.ASCII.GetBytes(data));
}