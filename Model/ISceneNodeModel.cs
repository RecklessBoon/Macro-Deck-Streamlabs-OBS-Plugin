using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneNodeModel
    {
        public string[] ChildrenIds { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string SceneId { get; set; }
        public TSceneNodeType SceneNodeType { get; set; }
    }
}
