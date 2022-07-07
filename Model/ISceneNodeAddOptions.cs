using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface ISceneNodeAddOptions
    {
        public string Id { get; set; }
        public SourceAddOptions SourceAddOptions { get; set; }
    }
}
