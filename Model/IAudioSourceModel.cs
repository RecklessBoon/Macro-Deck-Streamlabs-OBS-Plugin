using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface IAudioSourceModel
    {
        public double AudioMixers { get; set; }
        public IFader Fader { get; set; }
        public bool ForceMono { get; set; }
        public bool MixerHidden { get; set; }
        public OBSMonitoringType MonitoringType { get; set; }
        public bool Muted { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        public double SyncOffset { get; set; }
    }
}
