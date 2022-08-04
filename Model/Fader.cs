using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class Fader : IFader
    {
        public double? Db { get; set; }
        public double? Deflection { get; set; }
        public double? Mul { get; set; }
    }
}
