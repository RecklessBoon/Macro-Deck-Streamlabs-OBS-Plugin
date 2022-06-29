using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemSettings
    {
        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("recordingVisible")]
        public bool RecordingVisible { get; set; }

        [JsonProperty("streamVisible")]
        public bool StreamVisible { get; set; }

        [JsonProperty("transform")]
        public ITransform Transform { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
