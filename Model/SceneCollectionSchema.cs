using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneCollectionSchema : ISceneCollectionSchema
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("scenes")]
        public Scene[] Scenes { get; set; }

        [JsonProperty("sources")]
        public Source[] Sources { get; set; }

    }
}
