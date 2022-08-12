using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum PathPropertyType
    {
        OBS_PATH_FILE,
        OBS_PATH_FILE_SAVE,
        OBS_PATH_DIRECTORY
    }

    public class Path : PropertyBase
    {
        public new string Value { get; set; }
        public string Filter { get; set; }
        public string DefaultPath { get; set; }
    }
}
