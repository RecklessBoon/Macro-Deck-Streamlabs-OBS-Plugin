using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class Selection
    {
        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        public async Task<Selection> AddAsync(string[] ids) => await BaseService.MakeCallAsync<Selection>(this.SceneId, new { ids });
        public async Task CenterOnScreenAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task<Selection> CloneAsync() => await BaseService.MakeCallAsync<Selection>(this.SceneId);
        public async Task CopyToAsync(string sceneId, string folderId = null, Nullable<bool> duplicateSources = null) => await BaseService.MakeCallAsync(this.SceneId, new { sceneId, folderId, duplicateSources });
        public async Task<Selection> DeselectAsync(string[] ids) => await BaseService.MakeCallAsync<Selection>(this.SceneId, new { ids });
        public async Task FitToSceneAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task FlipXAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task FlipYAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task<object> GetBoundingRectAsync() => await BaseService.MakeCallAsync<object>(this.SceneId);
        public async Task<SceneItemFolder[]> GetFoldersAsync() => await BaseService.MakeCallAsync<SceneItemFolder[]>(this.SceneId);
        public async Task<string[]> GetIdsAsync() => await BaseService.MakeCallAsync<string[]>(this.SceneId);
        public async Task<string[]> GetInvertedIdsAsync() => await BaseService.MakeCallAsync<string[]>(this.SceneId);
        public async Task<SceneItem[]> GetItemsAsync() => await BaseService.MakeCallAsync<SceneItem[]>(this.SceneId);
        public async Task<SceneNode> GetLastSelectedAsync() => await BaseService.MakeCallAsync<SceneNode>(this.SceneId);
        public async Task<string> GetLastSelectedIdAsync() => await BaseService.MakeCallAsync<string>(this.SceneId);
        public async Task<SelectionModel> GetModelAsync() => await BaseService.MakeCallAsync<SelectionModel>(this.SceneId);
        public async Task<SceneNode[]> GetRootNodesAsync() => await BaseService.MakeCallAsync<SceneNode[]>(this.SceneId);
        public async Task<Scene> GetSceneAsync() => await BaseService.MakeCallAsync<Scene>(this.SceneId);
        public async Task<double> GetSizeAsync() => await BaseService.MakeCallAsync<double>(this.SceneId);
        public async Task<Source[]> GetSourcesAsync() => await BaseService.MakeCallAsync<Source[]>(this.SceneId);
        public async Task<SceneItem[]> GetVisualItemsAsync() => await BaseService.MakeCallAsync<SceneItem[]>(this.SceneId);
        public async Task<Selection> InvertAsync() => await BaseService.MakeCallAsync<Selection>(this.SceneId);
        public async Task<bool> IsSceneFolderAsync() => await BaseService.MakeCallAsync<bool>(this.SceneId);
        public async Task<bool> IsSceneItemAsync() => await BaseService.MakeCallAsync<bool>(this.SceneId);
        public async Task<bool> IsSelectedAsync(string nodeId) => await BaseService.MakeCallAsync<bool>(this.SceneId, new { nodeId });
        public async Task MovetoAsync(string sceneId, string folderId = null) => await BaseService.MakeCallAsync(this.SceneId, new { sceneId, folderId });
        public async Task PlaceAfterAsync(string sceneNodeId) => await BaseService.MakeCallAsync(this.SceneId, new { sceneNodeId });
        public async Task PlaceBeforerAsync(string sceneNodeId) => await BaseService.MakeCallAsync(this.SceneId, new { sceneNodeId });
        public async Task RemoveAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task<Selection> ResetAsync() => await BaseService.MakeCallAsync<Selection>(this.SceneId);
        public async Task ResetTransformAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task RotateAsync(double deg) => await BaseService.MakeCallAsync(this.SceneId, new { deg });
        public async Task ScaleAsync(IVec2 scale, IVec2 origin = null) => await BaseService.MakeCallAsync(this.SceneId, new { scale, origin });
        public async Task ScaleWithOffsetAsync(IVec2 scale, IVec2 offset = null) => await BaseService.MakeCallAsync(this.SceneId, new { scale, offset });
        public async Task<Selection> SelectAsync(string[] ids) => await BaseService.MakeCallAsync<Selection>(this.SceneId, new { ids });
        public async Task<Selection> SelectAllAsync() => await BaseService.MakeCallAsync<Selection>(this.SceneId);
        public async Task SetContentCropAsync() => await BaseService.MakeCallAsync(this.SceneId);
        public async Task SetParentAsync(string folderId) => await BaseService.MakeCallAsync(this.SceneId, new { folderId });
        public async Task SetRecordingVisibleAsync(bool recordingVisible) => await BaseService.MakeCallAsync(this.SceneId, new { recordingVisible });
        public async Task SetSettingsAsync(ISceneItemSettings settings) => await BaseService.MakeCallAsync(this.SceneId, new { settings });
        public async Task SetStreamVisibleAsync(bool streamVisible) => await BaseService.MakeCallAsync(this.SceneId, new { streamVisible });
        public async Task SetTransformAsync(PartialTransform transform) => await BaseService.MakeCallAsync(this.SceneId, new { transform });
        public async Task SetVisibilityAsync(bool visible) => await BaseService.MakeCallAsync(this.SceneId, new { visible });
        public async Task StretchToScreenAsync() => await BaseService.MakeCallAsync(this.SceneId);
    }
}
