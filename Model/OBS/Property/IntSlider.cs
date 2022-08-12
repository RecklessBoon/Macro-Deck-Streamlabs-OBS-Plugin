using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public class IntSlider : PropertyBase
    {
        public new int Value { get; set; }
        public int MinVal { get; set; }
        public int MaxVal { get; set; }
        public int StepVal { get; set; }

    }
}
