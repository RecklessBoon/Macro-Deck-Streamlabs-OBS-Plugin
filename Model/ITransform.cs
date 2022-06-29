using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ITransform
    {
        public ICrop Crop { get; set; }
        public IVec2 Position { get; set; }
        public double Rotation { get; set; }
        public IVec2 Scale { get; set; }
    }
}
