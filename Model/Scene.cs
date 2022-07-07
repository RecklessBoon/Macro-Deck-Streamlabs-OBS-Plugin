using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
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

        public async Task<SceneNode> AddFileAsync(string path, string folderId = null) => await BaseService.MakeCallAsync<SceneNode>(this.ResourceId, new { path, folderId });

        public async Task<SceneItem> AddSourceAsync(string sourceId, ISceneNodeAddOptions options = null) => await BaseService.MakeCallAsync<SceneItem>(this.ResourceId, new { sourceId, options });

        public async Task<bool> CanAddSourceAsync(string sourceId) => await BaseService.MakeCallAsync<bool>(this.ResourceId, new { sourceId });

        public async Task ClearAsync() => await BaseService.MakeCallAsync(this.ResourceId);

        public async Task<SceneItem> CreateAndAddSourceAsync(string name, TSourceType type, Dictionary<string, object> settings = null) => await BaseService.MakeCallAsync<SceneItem>(this.ResourceId, new { name, type, settings });

        public async Task<SceneItemFolder> CreateFolderAsync(string name) => await BaseService.MakeCallAsync<SceneItemFolder>(this.ResourceId, new { name });

        public async Task<SceneItemFolder> GetFolderAsync(string sceneFolderId) => await BaseService.MakeCallAsync<SceneItemFolder>(this.ResourceId, new { sceneFolderId });

        public async Task<SceneItemFolder[]> GetFoldersAsync() => await BaseService.MakeCallAsync<SceneItemFolder[]>(this.ResourceId);

        public async Task<SceneItem> GetItemAsync(string sceneItemId) => await BaseService.MakeCallAsync<SceneItem>(this.ResourceId, new { sceneItemId });

        public async Task<SceneItem[]> GetItemsAsync() => await BaseService.MakeCallAsync<SceneItem[]>(this.ResourceId);

        public async Task<SceneItem[]> GetNestedItemsAsync() => await BaseService.MakeCallAsync<SceneItem[]>(this.ResourceId);

        public async Task<Scene[]> GetNestedScenesAsync() => await BaseService.MakeCallAsync<Scene[]>(this.ResourceId);

        public async Task<Source[]> GetNestedSourcesAsync() => await BaseService.MakeCallAsync<Source[]>(this.ResourceId);
    }
}
