using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    class AudioSource : IAudioSourceModel
    {
        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("audioMixers")]
        public double AudioMixers { get; set; }

        [JsonProperty("fader")]
        public IFader Fader { get; set; }

        [JsonProperty("forceMono")]
        public bool ForceMono { get; set; }

        [JsonProperty("mixerHidden")]
        public bool MixerHidden { get; set; }

        [JsonProperty("monitoringType")]
        public OBSMonitoringType MonitoringType { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("syncOffset")]
        public double SyncOffset { get; set; }

        public async Task SetDeflectionAsync(double deflection) => await BaseService.MakeCallAsync(this.ResourceId, deflection);

        public async Task SetMutedAsync(bool muted) => await BaseService.MakeCallAsync(this.ResourceId, muted);
    }
}
