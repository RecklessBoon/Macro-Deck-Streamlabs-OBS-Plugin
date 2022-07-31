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
        public async Task<AudioSource> GetSourceAsync(string sourceId) => await MakeCallAsync<AudioSource>(this.GetType().Name, sourceId);
        public async Task<AudioSource[]> GetSourcesAsync() => await MakeCallAsync<AudioSource[]>(this.GetType().Name);
        public async Task<AudioSource[]> GetSourcesForCurrentSceneAsync() => await MakeCallAsync<AudioSource[]>(this.GetType().Name);
        public async Task<AudioSource> GetSourcesForSceneAsync(string sourceId) => await MakeCallAsync<AudioSource>(this.GetType().Name, sourceId);
    }
}
