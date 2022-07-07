namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface IPartialTransform
    {
        Crop Crop { get; set; }
        Vec2 Position { get; set; }
        double Rotation { get; set; }
        Vec2 Scale { get; set; }
    }
}