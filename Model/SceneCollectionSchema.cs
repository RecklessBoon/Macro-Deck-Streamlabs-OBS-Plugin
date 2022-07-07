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
        public object[] Scenes { get; set; }

        [JsonProperty("sources")]
        public object[] Sources { get; set; }

    }
}
