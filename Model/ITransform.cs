namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ITransform
    {
        Crop Crop { get; set; }
        Vec2 Position { get; set; }
        double Rotation { get; set; }
        Vec2 Scale { get; set; }
    }
}