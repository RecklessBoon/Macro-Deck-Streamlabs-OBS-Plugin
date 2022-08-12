using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public class FloatSlider : PropertyBase
    {
        public new float Value { get; set; }
        public float MinVal { get; set; }
        public float MaxVal { get; set; }
        public float StepVal { get; set; }

    }
}
