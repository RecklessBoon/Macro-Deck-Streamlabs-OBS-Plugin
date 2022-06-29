using Newtonsoft.Json;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class JsonRpcNotify : JsonRpcRequest
    {
        [JsonProperty("id")]
        public new string Id => null;
    }
}
