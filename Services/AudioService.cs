using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class AudioService : BaseService
    {
        public async Task GetSourceAsync(string sourceId) => await MakeCallAsync<AudioSource>(this.GetType().Name, new { sourceId });
        public async Task GetSourcesAsync() => await MakeCallAsync<AudioSource[]>(this.GetType().Name);
        public async Task GetSourcesForCurrentSceneAsync() => await MakeCallAsync<AudioSource[]>(this.GetType().Name);
        public async Task GetSourcesForSceneAsync(string sourceId) => await MakeCallAsync<AudioSource>(this.GetType().Name, new { sourceId });
    }
}
