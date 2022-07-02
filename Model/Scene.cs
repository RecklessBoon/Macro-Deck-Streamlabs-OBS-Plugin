using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
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

        public async Task<SceneItemFolder[]> GetFoldersAsync() => await BaseService.MakeCallAsync<SceneItemFolder[]>(this.ResourceId);
    }
}
