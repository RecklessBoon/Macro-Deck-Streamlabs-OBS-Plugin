using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public class PropertyBase : IPropertyBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        public PropertyType Type { get; set; }
        public JArray Options { get; set; }
    }
}
