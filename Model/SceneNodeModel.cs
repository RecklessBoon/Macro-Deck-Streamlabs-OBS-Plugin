using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneNodeModel : ISceneNodeModel
    {
        [JsonProperty("childrenIds")]
        public string[] ChildrenIds { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        [JsonProperty("sceneNodeType")]
        public TSceneNodeType SceneNodeType { get; set; }
    }
}
