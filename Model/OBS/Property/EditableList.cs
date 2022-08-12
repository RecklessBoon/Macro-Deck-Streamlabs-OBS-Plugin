using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum EditableListPropertyType
    {
        OBS_EDITABLE_LIST_TYPE_STRINGS,
        OBS_EDITABLE_LIST_TYPE_FILES,
        OBS_EDITABLE_LIST_TYPE_FILES_AND_URLS
    }

    public class EditableList : PropertyBase
    {
        public new object[] Value { get; set; }
        public string Filter { get; set; }
        public string DefaultPath { get; set; }
        public bool Files { get; set; } = false;
        public bool FilesAndUrls { get; set; } = false;
        public EditableListPropertyType EditableListType
        {
            get
            {
                return Files ? EditableListPropertyType.OBS_EDITABLE_LIST_TYPE_FILES
                             : FilesAndUrls ? EditableListPropertyType.OBS_EDITABLE_LIST_TYPE_FILES_AND_URLS
                                            : EditableListPropertyType.OBS_EDITABLE_LIST_TYPE_STRINGS;
            }
        }
    }
}
