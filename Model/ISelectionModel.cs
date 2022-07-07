namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISelectionModel
    {
        string LastSelectedId { get; set; }
        string[] SelectedIds { get; set; }
    }
}