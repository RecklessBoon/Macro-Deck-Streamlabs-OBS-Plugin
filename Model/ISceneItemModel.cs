using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemModel
    {
        public string[] ChildrenIds { get; set; }
        public string Id { get; set; }
        public bool Locked { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public bool RecordingVisible { get; set; }
        public string SceneId { get; set; }
        public string SceneItemId { get; set; }
        public TSceneNodeType SceneNodeType { get; set; }
        public string SourceId { get; set; }
        public bool StreamVisible { get; set; }
        public Transform Transform { get; set; }
        public bool Visible { get; set; }
    }
}
