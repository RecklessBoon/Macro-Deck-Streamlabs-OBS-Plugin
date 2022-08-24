using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum ListPropertyType
    {
        OBS_COMBO_TYPE_EDITABLE,
        OBS_COMBO_TYPE_LIST
    }

    public enum ListPropertyFormat
    {
        OBS_COMBO_FORMAT_INT,
        OBS_COMBO_FORMAT_FLOAT,
        OBS_COMBO_FORMAT_STRING
    }

    public class List : PropertyBase
    {
        public new object Value { get; set; }
        public ListPropertyFormat Format { get; set; }
        public bool Editable { get; set; } = false;
        public ListPropertyType ListPropertyType
        {
            get
            {
                return Editable ? ListPropertyType.OBS_COMBO_TYPE_EDITABLE
                                : ListPropertyType.OBS_COMBO_TYPE_LIST;
            }
        }
    }
}
