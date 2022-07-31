namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemSettings
    {
        bool Locked { get; set; }
        bool RecordingVisible { get; set; }
        bool StreamVisible { get; set; }
        Transform Transform { get; set; }
        bool Visible { get; set; }
    }
}