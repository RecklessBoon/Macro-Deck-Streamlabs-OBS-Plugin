using Newtonsoft.Json;
using System;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneItem : SceneNode, ISceneItemActions, ISceneItemModel
    {
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
        public ITransform Transform { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        public void CenterOnScreen()
        {
            throw new NotImplementedException();
        }

        public void FitToScreen()
        {
            throw new NotImplementedException();
        }

        public void FlipX()
        {
            throw new NotImplementedException();
        }

        public void FlipY()
        {
            throw new NotImplementedException();
        }

        public void ResetTransform()
        {
            throw new NotImplementedException();
        }

        public void Rotate(double deg)
        {
            throw new NotImplementedException();
        }

        public void SetContentCrop()
        {
            throw new NotImplementedException();
        }

        public void SetSettings(ISceneItemSettings settings)
        {
            throw new NotImplementedException();
        }

        public void SetTransform(ITransform transform)
        {
            throw new NotImplementedException();
        }

        public void SetVisibility(bool visible)
        {
            throw new NotImplementedException();
        }

        public void StretchToScreen()
        {
            throw new NotImplementedException();
        }
    }
}
