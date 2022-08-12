using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum TextPropertyType
    {
        OBS_TEXT_DEFAULT,
        OBS_TEXT_PASSWORD,
        OBS_TEXT_MULTILINE
    }

    public class Text : PropertyBase
    {
        public new string Value { get; set; }
        public bool Multiline { get; set; }
        public bool Password { get; set; }

        public TextPropertyType TextType
        {
            get
            {
                return Multiline ? TextPropertyType.OBS_TEXT_MULTILINE 
                                 : Password ? TextPropertyType.OBS_TEXT_PASSWORD 
                                            : TextPropertyType.OBS_TEXT_DEFAULT;
            }
        }
    }
}
