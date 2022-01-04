
namespace VRControl
{
    public partial class Form1 : Form
    {
        private GosundPlug? plug = null;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Icon = Properties.Resources.Icon1;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void onBtn_Click(object sender, EventArgs e)
        {
            refTimer.Stop();
            plug = new GosundPlug(ipTextBox.Text, tokenTextBox.Text);
            plug.On();
            refTimer.Start();
        }
        private void offBtn_Click(object sender, EventArgs e)
        {
            refTimer.Stop();
            plug = new GosundPlug(ipTextBox.Text, tokenTextBox.Text);
            plug.Off();
            refTimer.Start();
        }

        private void label_status_Click(object sender, EventArgs e)
        {
            refTimer.Stop();
            plug = new GosundPlug(ipTextBox.Text, tokenTextBox.Text);
            refTimer.Start();
        }

        private void refTimer_Tick(object sender, EventArgs e)
        {
            if (plug != null)
            {
                if (plug.Status())
                {
                    label_status.Text = "Status: On";
                }
                else
                {
                    label_status.Text = "Status: Off";
                }
            }
        }

    }
}