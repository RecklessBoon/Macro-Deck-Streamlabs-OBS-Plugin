namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface IPerformanceState
    {
        double Bandwidth { get; set; }
        double Cpu { get; set; }
        double FrameRate { get; set; }
        double NumberDroppedFrames { get; set; }
        double PercentageDroppedFrames { get; set; }
    }
}