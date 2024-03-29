﻿using Newtonsoft.Json;
using static RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property;
using Newtonsoft.Json.Linq;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class Source : ISourceModel
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

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("type")]
        public TSourceType Type { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        public async Task<Source> DuplicateAsync() => await MakeCallAsync<Source>(this.ResourceId);

        public async Task<JArray> GetPropertiesFormDataAsync() => await MakeCallAsync<JArray>(this.ResourceId);

        public async Task<JObject> GetSettingsAsync() => await MakeCallAsync<JObject>(this.ResourceId);

        public async Task<bool> HasPropsAsync() => await MakeCallAsync<bool>(this.ResourceId);

        public async Task RefreshAsync() => await MakeCallAsync(this.ResourceId);

        public async Task SetNameAsync(string newName) => await MakeCallAsync<string>(this.ResourceId, newName);

        public async Task<JArray> SetPropertiesFormDataAsync(JArray properties) => await MakeCallAsync<JArray>(this.ResourceId, properties);

        public async Task UpdateSettingsAsync(JObject settings) => await MakeCallAsync(this.ResourceId, settings);

    }
}
