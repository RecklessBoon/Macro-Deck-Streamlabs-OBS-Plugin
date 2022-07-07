using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Source> DuplicateAsync() => await BaseService.MakeCallAsync<Source>(this.ResourceId);

        public async Task<Dictionary<string, object>> GetSettingsAsync() => await BaseService.MakeCallAsync<Dictionary<string, object>>(this.ResourceId);

        public async Task<bool> HasPropsAsync() => await BaseService.MakeCallAsync<bool>(this.ResourceId);

        public async Task RefreshAsync() => await BaseService.MakeCallAsync(this.ResourceId);

        public async Task SetNameAsync(string newName) => await BaseService.MakeCallAsync<string>(this.ResourceId, new { newName });

        public async Task UpdateSettingsAsync(Dictionary<string, object> settings) => await BaseService.MakeCallAsync(this.ResourceId, new { settings });

    }
}
