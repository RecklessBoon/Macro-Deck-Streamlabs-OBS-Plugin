using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SourceModel : ISourceModel
    {
        [JsonProperty("async")]
        public bool Async { get; set; }

        [JsonProperty("audio")]
        public bool Audio { get; set; }

        [JsonProperty("channel")]
        public double Channel { get; set; }

        [JsonProperty("doNotDuplicate")]
        public bool DoNotDuplicate { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("type")]
        public TSourceType Type { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }
    }
}
