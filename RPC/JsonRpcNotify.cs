using Newtonsoft.Json;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC
{
    public class JsonRpcNotify : JsonRpcRequest
    {
        [JsonProperty("id")]
        public new string Id => null;
    }
}
