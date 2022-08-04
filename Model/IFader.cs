namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface IFader
    {
        double? Db { get; set; }
        double? Deflection { get; set; }
        double? Mul { get; set; }
    }
}