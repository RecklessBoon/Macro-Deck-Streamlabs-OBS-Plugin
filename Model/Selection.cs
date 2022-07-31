using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services.BaseService;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class Selection
    {
        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        public async Task<Selection> AddAsync(string[] ids) => await MakeCallAsync<Selection>(this.SceneId, ids);
        public async Task CenterOnScreenAsync() => await MakeCallAsync(this.SceneId);
        public async Task<Selection> CloneAsync() => await MakeCallAsync<Selection>(this.SceneId);
        public async Task CopyToAsync(string sceneId, string folderId = null, Nullable<bool> duplicateSources = null) => await MakeCallAsync(this.SceneId, Args(sceneId, folderId, duplicateSources));
        public async Task<Selection> DeselectAsync(string[] ids) => await MakeCallAsync<Selection>(this.SceneId, ids);
        public async Task FitToSceneAsync() => await MakeCallAsync(this.SceneId);
        public async Task FlipXAsync() => await MakeCallAsync(this.SceneId);
        public async Task FlipYAsync() => await MakeCallAsync(this.SceneId);
        public async Task<object> GetBoundingRectAsync() => await MakeCallAsync<object>(this.SceneId);
        public async Task<SceneItemFolder[]> GetFoldersAsync() => await MakeCallAsync<SceneItemFolder[]>(this.SceneId);
        public async Task<string[]> GetIdsAsync() => await MakeCallAsync<string[]>(this.SceneId);
        public async Task<string[]> GetInvertedIdsAsync() => await MakeCallAsync<string[]>(this.SceneId);
        public async Task<SceneItem[]> GetItemsAsync() => await MakeCallAsync<SceneItem[]>(this.SceneId);
        public async Task<SceneNode> GetLastSelectedAsync() => await MakeCallAsync<SceneNode>(this.SceneId);
        public async Task<string> GetLastSelectedIdAsync() => await MakeCallAsync<string>(this.SceneId);
        public async Task<SelectionModel> GetModelAsync() => await MakeCallAsync<SelectionModel>(this.SceneId);
        public async Task<SceneNode[]> GetRootNodesAsync() => await MakeCallAsync<SceneNode[]>(this.SceneId);
        public async Task<Scene> GetSceneAsync() => await MakeCallAsync<Scene>(this.SceneId);
        public async Task<double> GetSizeAsync() => await MakeCallAsync<double>(this.SceneId);
        public async Task<Source[]> GetSourcesAsync() => await MakeCallAsync<Source[]>(this.SceneId);
        public async Task<SceneItem[]> GetVisualItemsAsync() => await MakeCallAsync<SceneItem[]>(this.SceneId);
        public async Task<Selection> InvertAsync() => await MakeCallAsync<Selection>(this.SceneId);
        public async Task<bool> IsSceneFolderAsync() => await MakeCallAsync<bool>(this.SceneId);
        public async Task<bool> IsSceneItemAsync() => await MakeCallAsync<bool>(this.SceneId);
        public async Task<bool> IsSelectedAsync(string nodeId) => await MakeCallAsync<bool>(this.SceneId, nodeId);
        public async Task MovetoAsync(string sceneId, string folderId = null) => await MakeCallAsync(this.SceneId, Args(sceneId, folderId));
        public async Task PlaceAfterAsync(string sceneNodeId) => await MakeCallAsync(this.SceneId, sceneNodeId);
        public async Task PlaceBeforerAsync(string sceneNodeId) => await MakeCallAsync(this.SceneId, sceneNodeId);
        public async Task RemoveAsync() => await MakeCallAsync(this.SceneId);
        public async Task<Selection> ResetAsync() => await MakeCallAsync<Selection>(this.SceneId);
        public async Task ResetTransformAsync() => await MakeCallAsync(this.SceneId);
        public async Task RotateAsync(double deg) => await MakeCallAsync(this.SceneId, deg);
        public async Task ScaleAsync(IVec2 scale, IVec2 origin = null) => await MakeCallAsync(this.SceneId, Args(scale, origin));
        public async Task ScaleWithOffsetAsync(IVec2 scale, IVec2 offset = null) => await MakeCallAsync(this.SceneId, Args(scale, offset));
        public async Task<Selection> SelectAsync(string[] ids) => await MakeCallAsync<Selection>(this.SceneId, ids);
        public async Task<Selection> SelectAllAsync() => await MakeCallAsync<Selection>(this.SceneId);
        public async Task SetContentCropAsync() => await MakeCallAsync(this.SceneId);
        public async Task SetParentAsync(string folderId) => await MakeCallAsync(this.SceneId, folderId);
        public async Task SetRecordingVisibleAsync(bool recordingVisible) => await MakeCallAsync(this.SceneId, recordingVisible);
        public async Task SetSettingsAsync(SceneItemSettings settings) => await MakeCallAsync(this.SceneId, settings);
        public async Task SetStreamVisibleAsync(bool streamVisible) => await MakeCallAsync(this.SceneId, streamVisible);
        public async Task SetTransformAsync(PartialTransform transform) => await MakeCallAsync(this.SceneId, transform);
        public async Task SetVisibilityAsync(bool visible) => await MakeCallAsync(this.SceneId, visible);
        public async Task StretchToScreenAsync() => await MakeCallAsync(this.SceneId);
    }
}
