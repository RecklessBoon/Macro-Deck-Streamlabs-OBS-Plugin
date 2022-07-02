using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneItemModel : ISceneItemModel
    {
        [JsonProperty("childrenIds")]
        public string[] ChildrenIds { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("recordingVisible")]
        public bool RecordingVisible { get; set; }

        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        [JsonProperty("sceneItemId")]
        public string SceneItemId { get; set; }

        [JsonProperty("sceneNodeType")]
        public TSceneNodeType SceneNodeType { get; set; }

        [JsonProperty("sourceId")]
        public string SourceId { get; set; }

        [JsonProperty("streamVisible")]
        public bool StreamVisible { get; set; }

        [JsonProperty("transform")]
        public ITransform Transform { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
