using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class SourcesService : BaseService
    {
        public event EventHandler<SourceModel> SourceAdded { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SourceModel> SourceRemoved { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SourceModel> SourceUpdated { add => AddSubscriber(value); remove => RemoveSubscriber(value); }

        public async Task<Source> AddFileAsync(string path) => await MakeCallAsync<Source>(this.GetType().Name, path);
        public async Task<Source> CreateSourceAsync(string name, TSourceType type, Dictionary<string, object> settings = null, SourceAddOptions options = null) => await MakeCallAsync<Source>(this.GetType().Name, Args(name, type, settings, options));
        public async Task<List<TSourceType>> GetAvailableSourcesTypesListAsync() => await MakeCallAsync<List<TSourceType>>(this.GetType().Name);
        public async Task<Source> GetSourceAsync(string sourceId) => await MakeCallAsync<Source>(this.GetType().Name, sourceId);
        public async Task<Source[]> GetSourcesAsync() => await MakeCallAsync<Source[]>(this.GetType().Name);
        public async Task<Source[]> GetSourcesByNameAsync(string name) => await MakeCallAsync<Source[]>(this.GetType().Name, name);
        public async Task RemoveSourceAsync(string id) => await MakeCallAsync(this.GetType().Name, id);
        public async Task ShowAddSourceAsync(TSourceType sourceType) => await MakeCallAsync(this.GetType().Name, sourceType);
        public async Task ShowShowcaseAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task ShowSourcePropertiesAsync(string sourceId) => await MakeCallAsync(this.GetType().Name, sourceId);
    }
}
