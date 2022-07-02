using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SceneNodeModel[] Nodes { get; set; }
    }
}
