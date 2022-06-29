using Newtonsoft.Json;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ICrop
    {
        [JsonProperty("bottom")]
        public double Bottom { get; set; }

        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("right")]
        public double Right { get; set; }

        [JsonProperty("top")]
        public double Top { get; set; }
    }
}
