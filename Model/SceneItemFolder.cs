namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SceneItemFolder : ISceneItemFolderModel
    {
        public string[] ChildrenIds { get; set; }
        public string Id { get; set; }
        public bool Locked { get; set; }
        public string Name { get; set; }
        public string NodeId { get; set; }
        public string ParentId { get; set; }
        public bool RecordingVisible { get; set; }
        public string SceneId { get; set; }
        public string SceneItemId { get; set; }
        public TSceneNodeType SceneNodeType { get; set; }
        public string SourceId { get; set; }
        public bool StreamVisible { get; set; }
        public ITransform Transform { get; set; }
        public bool Visible { get; set; }
    }
}
