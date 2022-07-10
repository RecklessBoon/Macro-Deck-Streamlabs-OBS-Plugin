using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class SceneCollectionsService : BaseService
    {
        public async Task<SceneCollectionsManifestEntry> ActiveCollectionAsync() => await MakeCallAsync<SceneCollectionsManifestEntry>(this.GetType().Name);
        public async Task<SceneCollectionsManifestEntry[]> CollectionsAsync() => await MakeCallAsync<SceneCollectionsManifestEntry[]>(this.GetType().Name);

        public event EventHandler<SceneCollectionsManifestEntry> CollectionAdded { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SceneCollectionsManifestEntry> CollectionRemoved { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SceneCollectionsManifestEntry> CollectionSwitched { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SceneCollectionsManifestEntry> CollectionUpdated { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler CollectionWillSwitch { add => AddSubscriber(value); remove => RemoveSubscriber(value); }

        public async Task<SceneCollectionsManifestEntry> CreateAsync(SceneCollectionCreateOptions options) => await MakeCallAsync<SceneCollectionsManifestEntry>(this.GetType().Name, new { options });
        public async Task DeleteAsync(string id) => await MakeCallAsync(this.GetType().Name, new { id });
        public async Task<SceneCollectionSchema[]> FetchSceneCollectionsSchemaAsync() => await MakeCallAsync<SceneCollectionSchema[]>(this.GetType().Name);
        public async Task LoadAsync(string id) => await MakeCallAsync(this.GetType().Name, new { id });
        public async Task RenameAsync(string newName, string id) => await MakeCallAsync(this.GetType().Name, new { newName, id });
    }
}
