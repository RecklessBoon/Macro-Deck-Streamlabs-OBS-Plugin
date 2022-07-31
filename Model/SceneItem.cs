using Newtonsoft.Json;
using static RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services.BaseService;
using System;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneItem : SceneNode, ISceneItemActions, ISceneItemModel
    {
        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("childrenids")]
        public string[] ChildrenIds { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recordingVisible")]
        public bool RecordingVisible { get; set; }

        [JsonProperty("sceneItemId")]
        public string SceneItemId { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("streamVisible")]
        public bool StreamVisible { get; set; }

        [JsonProperty("transform")]
        public Transform Transform { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        public async Task CenterOnScreenAsync() => await MakeCallAsync(this.ResourceId);
        public async Task FitToScreenAsync() => await MakeCallAsync(this.ResourceId);
        public async Task FlipXAsync() => await MakeCallAsync(this.ResourceId);
        public async Task FlipYAsync() => await MakeCallAsync(this.ResourceId);
        public async Task ResetTransformAsync() => await MakeCallAsync(this.ResourceId);
        public async Task RotateAsync(double deg) => await MakeCallAsync(this.ResourceId, deg);
        public async Task RemoveAsync() => await MakeCallAsync(this.ResourceId);
        public async Task SetContentCropAsync() => await MakeCallAsync(this.ResourceId);
        public async Task SetSettingsAsync(ISceneItemSettings settings) => await MakeCallAsync(this.ResourceId, settings);
        public async Task SetTransformAsync(ITransform transform) => await MakeCallAsync(this.ResourceId, transform);
        public async Task SetVisibilityAsync(bool visible) => await MakeCallAsync(this.ResourceId, visible);
        public async Task StretchToScreenAsync() => await MakeCallAsync(this.ResourceId);
    }
}
