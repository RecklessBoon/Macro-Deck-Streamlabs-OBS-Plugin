using Newtonsoft.Json;
using static RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services.BaseService;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class Scene : ISceneModel
    {
        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nodes")]
        public SceneNodeModel[] Nodes { get; set; }

        public async Task<SceneNode> AddFileAsync(string path, string folderId = null) => await MakeCallAsync<SceneNode>(this.ResourceId, Args(path, folderId));

        public async Task<SceneItem> AddSourceAsync(string sourceId, ISceneNodeAddOptions options = null) => await MakeCallAsync<SceneItem>(this.ResourceId, Args(sourceId, options));

        public async Task<bool> CanAddSourceAsync(string sourceId) => await MakeCallAsync<bool>(this.ResourceId, sourceId);

        public async Task ClearAsync() => await MakeCallAsync(this.ResourceId);

        public async Task<SceneItem> CreateAndAddSourceAsync(string name, TSourceType type, Dictionary<string, object> settings = null) => await MakeCallAsync<SceneItem>(this.ResourceId, Args(name, type, settings));

        public async Task<SceneItemFolder> CreateFolderAsync(string name) => await MakeCallAsync<SceneItemFolder>(this.ResourceId, name);

        public async Task<SceneItemFolder> GetFolderAsync(string sceneFolderId) => await MakeCallAsync<SceneItemFolder>(this.ResourceId, sceneFolderId);

        public async Task<SceneItemFolder[]> GetFoldersAsync() => await MakeCallAsync<SceneItemFolder[]>(this.ResourceId);

        public async Task<SceneItem> GetItemAsync(string sceneItemId) => await MakeCallAsync<SceneItem>(this.ResourceId, sceneItemId);

        public async Task<SceneItem[]> GetItemsAsync() => await MakeCallAsync<SceneItem[]>(this.ResourceId);

        public async Task<SceneItem[]> GetNestedItemsAsync() => await MakeCallAsync<SceneItem[]>(this.ResourceId);

        public async Task<Scene[]> GetNestedScenesAsync() => await MakeCallAsync<Scene[]>(this.ResourceId);

        public async Task<Source[]> GetNestedSourcesAsync() => await MakeCallAsync<Source[]>(this.ResourceId);
    }
}
