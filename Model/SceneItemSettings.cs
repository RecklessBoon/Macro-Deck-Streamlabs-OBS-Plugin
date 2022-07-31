using Newtonsoft.Json;
using System.Collections.Generic;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneItemSettings : ISceneItemSettings
    {
        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("recordingVisible")]
        public bool RecordingVisible { get; set; }

        [JsonProperty("streamVisible")]
        public bool StreamVisible { get; set; }

        [JsonProperty("transform")]
        public Transform Transform { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
