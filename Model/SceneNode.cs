using Newtonsoft.Json;
using System;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneNode
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("sceneId")]
        public string SceneId { get; set; }

        [JsonProperty("sceneNodeType")]
        public TSceneNodeType SceneNodeType { get; set; }

        public void AddToSelection()
        {
            throw new NotImplementedException();
        }

        public void Deselect()
        {
            throw new NotImplementedException();
        }

        public void DetachParent()
        {
            throw new NotImplementedException();
        }

        public double GetItemIndex()
        {
            throw new NotImplementedException();
        }

        public ISceneNodeModel GetModel()
        {
            throw new NotImplementedException();
        }

        public SceneItem GetNextItem()
        {
            throw new NotImplementedException();
        }

        public SceneNode GetNextNode()
        {
            throw new NotImplementedException();
        }

        public double GetNodeIndex()
        {
            throw new NotImplementedException();
        }

        public SceneItemFolder GetParent()
        {
            throw new NotImplementedException();
        }

        public string[] GetPath()
        {
            throw new NotImplementedException();
        }

        public SceneItem GetPrevItem()
        {
            throw new NotImplementedException();
        }

        public SceneNode GetPrevNode()
        {
            throw new NotImplementedException();
        }

        public Scene GetScene()
        {
            throw new NotImplementedException();
        }

        public bool HasParent()
        {
            throw new NotImplementedException();
        }

        public SceneNode IsFolder()
        {
            throw new NotImplementedException();
        }

        public SceneNode IsItem()
        {
            throw new NotImplementedException();
        }

        public bool IsSelected()
        {
            throw new NotImplementedException();
        }

        public void PlaceAfter(string nodeId)
        {
            throw new NotImplementedException();
        }

        public void PlaceBefore(string nodeId)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public void SetParent(string parentId)
        {
            throw new NotImplementedException();
        }
    }
}
