namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISourceModel
    {
        bool Async { get; set; }
        bool Audio { get; set; }
        double Channel { get; set; }
        bool DoNotDuplicate { get; set; }
        double Height { get; set; }
        string Id { get; set; }
        bool Muted { get; set; }
        string Name { get; set; }
        string SourceId { get; set; }
        TSourceType Type { get; set; }
        bool Video { get; set; }
        double Width { get; set; }
    }
}