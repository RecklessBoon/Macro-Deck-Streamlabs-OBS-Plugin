using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC
{
    public class RPCResult
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("emitter")]
        public string Emitter { get; set; }

        [JsonProperty("data")]
        public JToken Data { get; set; }
    }
}
