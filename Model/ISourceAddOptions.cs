using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISourceAddOptions
    {
        public double Channel { get; set; }
        public bool IsTemporary { get; set; }
    }
}
