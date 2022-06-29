using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneItemSettings
    {
        public bool Locked { get; set; }
        public bool RecordingVisible { get; set; }
        public bool StreamVisible { get; set; }
        public ITransform Transform { get; set; }
        public bool Visible { get; set; }
    }
}
