namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneCollectionSchema
    {
        string Id { get; set; }
        string Name { get; set; }
        Scene[] Scenes { get; set; }
        Source[] Sources { get; set; }
    }
}