using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class SourceAddOptions : ISourceAddOptions
    {
        [JsonProperty("channel")]
        public double Channel { get; set; }

        [JsonProperty("isTemporary")]
        public bool IsTemporary { get; set; }
    }
}
