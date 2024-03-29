﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneCollectionsManifestEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
