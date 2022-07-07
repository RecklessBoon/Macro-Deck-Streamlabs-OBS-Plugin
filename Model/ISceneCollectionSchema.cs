namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneCollectionSchema
    {
        string Id { get; set; }
        string Name { get; set; }
        object[] Scenes { get; set; }
        object[] Sources { get; set; }
    }
}