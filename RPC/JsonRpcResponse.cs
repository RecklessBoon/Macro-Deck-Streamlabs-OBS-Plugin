using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC
{
    public class JsonRpcResponse
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonProperty("result")]
        public RPCResult Result { get; set; }

        [JsonProperty("error")]
        public JsonRpcError Error { get; set; }

        [JsonIgnore]
        public bool IsError => Error != null;

        [JsonProperty("id")]
        public string Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
