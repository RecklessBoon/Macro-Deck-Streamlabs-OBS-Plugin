using Newtonsoft.Json;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class JsonRpcError
    {
        [JsonProperty("code")]
        public double Code { get; }

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("data")]
        public object Data { get; }
    }
}
