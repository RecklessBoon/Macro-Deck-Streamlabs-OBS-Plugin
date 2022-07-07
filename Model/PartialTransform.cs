using Newtonsoft.Json;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class PartialTransform : IPartialTransform
    {
        [JsonProperty("crop")]
        public Crop Crop { get; set; }

        [JsonProperty("position")]
        public Vec2 Position { get; set; }

        [JsonProperty("rotation")]
        public double Rotation { get; set; }

        [JsonProperty("scale")]
        public Vec2 Scale { get; set; }
    }
}
