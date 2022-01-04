using System.Text.Json;
public class GosundPlug : MIIOSession
{
    public GosundPlug(string ip, string token) : base(ip, token)
    {

    }

    public void On()
    {
        this.send("{\"id\": 1, \"method\": \"set_properties\", \"params\": [{\"did\": \"state\", \"siid\": 2, \"piid\": 1, \"value\": true}]}");
    }

    public void Off()
    {
        this.send("{\"id\": 1, \"method\": \"set_properties\", \"params\": [{\"did\": \"state\", \"siid\": 2, \"piid\": 1, \"value\": false}]}");
    }

    public bool Status()
    {
        JsonDocument document = JsonDocument.Parse(this.send("{\"id\": 1, \"method\": \"get_prop\", \"params\": [{\"did\": \"state\", \"siid\": 2, \"piid\": 1}]}"));
        return document.RootElement.GetProperty("result")[0].GetProperty("value").GetBoolean();
    }
}