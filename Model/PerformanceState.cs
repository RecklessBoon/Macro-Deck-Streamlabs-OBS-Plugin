using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class PerformanceState : IPerformanceState
    {
        [JsonProperty("cpu")]
        public double Cpu { get; set; }

        [JsonProperty("bandwidth")]
        public double Bandwidth { get; set; }

        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }

        [JsonProperty("numberDroppedFrames")]
        public double NumberDroppedFrames { get; set; }

        [JsonProperty("percentageDroppedFrames")]
        public double PercentageDroppedFrames { get; set; }
    }
}
