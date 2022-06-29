using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ITransform
    {
        [JsonProperty("crop")]
        public ICrop Crop { get; set; }

        [JsonProperty("position")]
        public IVec2 Position { get; set; }

        [JsonProperty("rotation")]
        public double Rotation { get; set; }

        [JsonProperty("scale")]
        public IVec2 Scale { get; set; }
    }
}
