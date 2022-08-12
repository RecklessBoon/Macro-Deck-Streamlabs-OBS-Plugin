using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum GroupPropertyType
    {
        OBS_GROUP_NORMAL,
        OBS_GROUP_CHECKABLE
    }

    public class Group : PropertyBase
    {
        public PropertyBase GroupVal { get; set; }
    }
}
