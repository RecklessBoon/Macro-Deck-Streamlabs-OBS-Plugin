namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property
{
    public enum PropertyType
    {
        OBS_PROPERTY_INVALID,
        OBS_PROPERTY_BOOL,
        OBS_PROPERTY_INT,
        OBS_PROPERTY_FLOAT,
        OBS_PROPERTY_TEXT,
        OBS_PROPERTY_PATH,
        OBS_PROPERTY_FILE,
        OBS_PROPERTY_LIST,
        OBS_PROPERTY_COLOR,
        OBS_PROPERTY_BUTTON,
        OBS_PROPERTY_FONT,
        OBS_PROPERTY_EDITABLE_LIST,
        OBS_PROPERTY_FRAME_RATE,
        OBS_PROPERTY_GROUP,
        OBS_PROPERTY_SLIDER
    }

    public interface IPropertyBase
    {
        string Description { get; set; }
        string Name { get; set; }

        object Value { get; set; }
        bool Enabled { get; set; }
        bool Visible { get; set; }
        PropertyType Type { get; set; }
        object[] Options { get; set; }
    }
}