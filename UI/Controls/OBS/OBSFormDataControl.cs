using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Properties;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS
{
    public partial class OBSFormDataControl : UserControl
    {
        public event EventHandler<FormDataChangedEventArgs> OnFormDataChanged;

        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Black;
        public int BorderWidth { get; set; } = 1;

        public JArray Value { get; set; }

        public OBSFormDataControl()
        {
            InitializeComponent();
            tblPropertyContainer.CellPaint += TblPropertyContainer_CellPaint;
            RefreshControls();
        }

        private void TblPropertyContainer_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var topLeft = e.CellBounds.Location;
            var topRight = new Point(e.CellBounds.Right, e.CellBounds.Top);
            e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), topLeft, topRight);
        }

        public void RefreshControls()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    _PopulateForm();
                });
            }
            else
            {
                _PopulateForm();
            }
        }

        private void _PopulateForm()
        {
            if (Value == null) return;

            SuspendLayout();
            DrawingHelper.BeginControlUpdate(this);
            var properties = Value.ToObject<PropertyBase[]>();
            Control[] controls = new Control[tblPropertyContainer.Controls.Count];
            tblPropertyContainer.Controls.CopyTo(controls, 0);
            foreach(var control in controls)
            {
                PropertyBase property = properties.FirstOrDefault(prop => prop.Name == control.Name || prop.Name+"Label" == control.Name);
                if (property == default)
                {
                    tblPropertyContainer.Controls.Remove(control);
                }
            }
            foreach (JObject data in Value)
            {
                var property = data.ToObject<PropertyBase>();
                if (!property.Enabled) continue;

                Label label = tblPropertyContainer.Controls.Find(property.Name + "Label", false).OfType<Label>().FirstOrDefault();
                Control control = tblPropertyContainer.Controls.Find(property.Name, false).FirstOrDefault();
                var labelFound = label != null;
                var controlFound = control != null;
                label ??= new Label { Name = property.Name + "Label", Text = property.Description + ":", Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0), Margin = new Padding(2) };
                control ??= CreateControl(data, property);

                var padding = new Padding(0, 10, 0, 10);
                label.Margin = padding;
                control.Margin = padding;
                control.Dock = DockStyle.Fill;
                label.Visible = property.Visible;
                control.Visible = property.Visible;
                control.Name = property.Name;

                if (!labelFound) tblPropertyContainer.Controls.Add(label);
                if (!controlFound) tblPropertyContainer.Controls.Add(control);
            }

            DrawingHelper.EndControlUpdate(this);
            ResumeLayout(true);
        }

        private Control CreateControl(JObject data, PropertyBase property)
        {
            switch (property.Type)
            {
                case PropertyType.OBS_PROPERTY_BOOL:
                    var boolData = data.ToObject<Model.OBS.Property.Boolean>();
                    var boolControl = new CheckBox
                    {
                        Checked = boolData.Value
                    };  
                    boolControl.Click += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == boolControl.Name).First()["value"] = boolControl.Checked;
                        OnFormDataChanged?.Invoke(boolControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return boolControl;
                case PropertyType.OBS_PROPERTY_INT:
                    var intData = data.ToObject<Int>();
                    var intControl = new NumericUpDown
                    {
                        Minimum = intData.MinVal,
                        Maximum = intData.MaxVal,
                        Value = (int)intData.Value,
                        DecimalPlaces = 0,
                        Font = new System.Drawing.Font("Tahoma", 9.0f),
                        BackColor = System.Drawing.Color.FromArgb(65, 65, 65),
                        ForeColor = System.Drawing.Color.White,
                    };
                    intControl.ValueChanged += (object sender, EventArgs e) => 
                    {
                        this.Value.Where(prop => prop["name"].ToString() == intControl.Name).First()["value"] = intControl.Value;
                        OnFormDataChanged?.Invoke(intControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return intControl;
                case PropertyType.OBS_PROPERTY_SLIDER:
                    var sliderData = data.ToObject<IntSlider>();
                    var sliderControl = new TrackBar 
                    { 
                        Maximum = (sliderData.MaxVal + sliderData.StepVal), 
                        Minimum = sliderData.MinVal, 
                        Value = sliderData.Value, 
                        TickFrequency = ((sliderData.MaxVal - sliderData.MinVal) / 20), 
                        SmallChange = sliderData.StepVal, 
                        TickStyle = TickStyle.Both,
                        BackColor = System.Drawing.Color.FromArgb(65, 65, 65),
                        ForeColor = System.Drawing.Color.White,
                    };
                    sliderControl.ValueChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == sliderControl.Name).First()["value"] = sliderControl.Value;
                        OnFormDataChanged?.Invoke(sliderControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return sliderControl;
                case PropertyType.OBS_PROPERTY_FLOAT:
                    var floatData = data.ToObject<Float>();
                    var floatControl = new NumericUpDown 
                    { 
                        Minimum = (decimal)floatData.MinVal, 
                        Maximum = (decimal)floatData.MaxVal, 
                        Value = (decimal)floatData.Value, 
                        Font = new System.Drawing.Font("Tahoma", 9.0f),
                        BackColor = System.Drawing.Color.FromArgb(65, 65, 65),
                        ForeColor = System.Drawing.Color.White,
                    };
                    floatControl.ValueChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == floatControl.Name).First()["value"] = floatControl.Value;
                        OnFormDataChanged?.Invoke(floatControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return floatControl;
                case PropertyType.OBS_PROPERTY_TEXT:
                    var textData = data.ToObject<Text>();
                    var textControl = new RoundedTextBox 
                    { 
                        Text = textData.Value 
                    };
                    textControl.TextChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == textControl.Name).First()["value"] = textControl.Text;
                        OnFormDataChanged?.Invoke(textControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return textControl;
                case PropertyType.OBS_PROPERTY_PATH:
                    var pathData = data.ToObject<Path>();
                    var pathControl = new RoundedTextBox 
                    {
                        Text = (pathData.Value ?? pathData.DefaultPath), 
                        ReadOnly = true
                    };
                    pathControl.TextChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == pathControl.Name).First()["value"] = pathControl.Text;
                        OnFormDataChanged?.Invoke(pathControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    var folderDialog = new FolderBrowserDialog { SelectedPath = pathData.Value ?? pathData.DefaultPath, };
                    pathControl.Disposed += (object sender, EventArgs e) =>
                    {
                        folderDialog.Dispose();
                    };
                    pathControl.Click += (object sender, EventArgs e) =>
                    {
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            pathControl.Text = folderDialog.RootFolder + folderDialog.SelectedPath;
                        }
                    };
                    return pathControl;
                case PropertyType.OBS_PROPERTY_FILE:
                    var fileData = data.ToObject<Path>();
                    var fileControl = new RoundedTextBox 
                    { 
                        Text = (fileData.Value ?? fileData.DefaultPath), 
                        ReadOnly = true
                    };
                    fileControl.TextChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == fileControl.Name).First()["value"] = fileControl.Text;
                        OnFormDataChanged?.Invoke(fileControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    var fileDialog = new OpenFileDialog { Filter = fileData.Filter, FileName = fileData.Value ?? fileData.DefaultPath };
                    fileControl.Disposed += (object sender, EventArgs e) =>
                    {
                        fileDialog.Dispose();
                    };
                    fileControl.Click += (object sender, EventArgs e) =>
                    {
                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            fileControl.Text = fileDialog.FileName;
                        }
                    };
                    return fileControl;
                case PropertyType.OBS_PROPERTY_LIST:
                    var listData = data.ToObject<List>();
                    var listControl = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox 
                    { 
                        DataSource = listData.Options, 
                        DisplayMember = "description", 
                        ValueMember = "value", 
                        DropDownStyle = ComboBoxStyle.DropDownList, 
                        Font = new System.Drawing.Font("Tahoma", 9.0f)
                    };
                    LayoutEventHandler layoutChanged = null;
                    layoutChanged = delegate
                    {
                        listControl.SelectedValue = listData.Options.Where(o => o["value"].ToString().Equals(listData.Value.ToString())).FirstOrDefault()?["value"];
                        this.Layout -= layoutChanged;
                    };
                    this.Layout += layoutChanged;
                    listControl.SelectedValueChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == listControl.Name).First()["value"] = (JToken)listControl.SelectedValue;
                        OnFormDataChanged?.Invoke(listControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return listControl;
                case PropertyType.OBS_PROPERTY_EDITABLE_LIST:
                    var editableListData = data.ToObject<List>();
                    var editableListControl = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox 
                    { 
                        DataSource = editableListData.Options, 
                        DisplayMember = "description", 
                        ValueMember = "value", 
                        DropDownStyle = ComboBoxStyle.DropDown, 
                        Font = new System.Drawing.Font("Tahoma", 9.0f)
                    };
                    LayoutEventHandler editableLayoutChanged = null;
                    editableLayoutChanged = delegate
                    {
                        editableListControl.SelectedValue = editableListData.Options.Where(o => o["value"].ToString().Equals(editableListData.Value.ToString())).FirstOrDefault()?["value"];
                        this.Layout -= editableLayoutChanged;
                    };
                    this.Layout += editableLayoutChanged;
                    editableListControl.SelectedValue = editableListData.Value;
                    editableListControl.SelectedValueChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == editableListControl.Name).First()["value"] = (JToken)editableListControl.SelectedValue;
                        OnFormDataChanged?.Invoke(editableListControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return editableListControl;
                case PropertyType.OBS_PROPERTY_COLOR:
                    var colorData = data.ToObject<Model.OBS.Property.Color>();
                    var pic = new Bitmap(Resources.Image1, 12, 12);
                    pic.MakeTransparent(System.Drawing.Color.Gray);
                    var colorPreview = new Panel { Height = 24, Dock = DockStyle.Fill };
                    var colorControl = new Panel
                    {
                        Height = 24,
                        BackgroundImage = pic,
                        BackgroundImageLayout = ImageLayout.Tile
                    };
                    colorControl.Controls.Add(colorPreview);
                    var existingColor = System.Drawing.Color.FromArgb(colorData.Value);
                    existingColor = System.Drawing.Color.FromArgb(existingColor.A, existingColor.B, existingColor.G, existingColor.R);
                    colorPreview.BackColor = existingColor;
                    var colorDialog = new Cyotek.Windows.Forms.ColorPickerDialog { Color = existingColor };
                    colorControl.Disposed += (object sender, EventArgs e) =>
                    {
                        colorDialog.Dispose();
                    };
                    colorPreview.Click += (object sender, EventArgs e) =>
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            colorPreview.BackColor = colorDialog.Color;
                            this.Value.Where(prop => prop["name"].ToString() == colorControl.Name).First()["value"] = System.Drawing.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.B, colorDialog.Color.G, colorDialog.Color.R).ToArgb();
                            OnFormDataChanged?.Invoke(colorControl, new FormDataChangedEventArgs { FormData = this.Value });
                        }
                    };
                    return colorControl;
                //case PropertyType.OBS_PROPERTY_BUTTON:
                case PropertyType.OBS_PROPERTY_FONT:
                    var fontData = data.ToObject<Model.OBS.Property.Font>();
                    var text = String.Format("Font: {0} ({1}) - Size: {2}", fontData?.Value?["face"], fontData?.Value?["style"], fontData?.Value?["size"]);
                    System.Drawing.Font font = default;
                    try
                    {
                        font = new System.Drawing.Font(fontData.Value["face"].ToString(), 9.0f, Enum.Parse<System.Drawing.FontStyle>(fontData.Value["style"].ToString()));
                    }
                    catch (System.ArgumentException) { }
                    var fontControl = new RoundedTextBox
                    {
                        ReadOnly = true, 
                        Text = text,
                        Font = font,
                    };
                    fontData.Value["style"] = !fontData.Value["style"].ToString().Equals(String.Empty) ? fontData.Value["style"] : FontStyle.Regular.ToString();
                    var fontDialog = new FontDialog { Font = new System.Drawing.Font(fontData.Value["face"].ToString(), (float)fontData.Value["size"], Enum.Parse<System.Drawing.FontStyle>(fontData.Value["style"].ToString())) };
                    fontControl.Disposed += (object sender, EventArgs e) => 
                    {
                        fontDialog.Dispose();
                    };
                    fontControl.Click += (object sender, EventArgs e) =>
                    {
                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            fontControl.Font = new System.Drawing.Font(fontDialog.Font.FontFamily.Name, 9.0f, fontDialog.Font.Style);
                            fontControl.Text = String.Format("Font: {0} ({1}) - Size: {2}", fontDialog.Font.FontFamily.Name, fontDialog.Font.Style, fontDialog.Font.Size);
                            this.Value.Where(prop => prop["name"].ToString() == fontControl.Name).First()["value"] = JToken.FromObject(new { face = fontDialog.Font.FontFamily.Name, style = fontDialog.Font.Style, size = fontDialog.Font.Size });
                            OnFormDataChanged?.Invoke(fontControl, new FormDataChangedEventArgs { FormData = this.Value });
                        }
                    };
                    return fontControl;
                case PropertyType.OBS_PROPERTY_FRAME_RATE:
                    var frameRateData = data.ToObject<FrameRate>();
                    var frameRateControl = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox 
                    { 
                        DropDownStyle = ComboBoxStyle.DropDownList, 
                        DataSource = frameRateData.Options, 
                        SelectedValue = frameRateData.Value, 
                        Font = new System.Drawing.Font("Tahoma", 9.0f)
                    };
                    LayoutEventHandler frameRateLayoutChanged = null;
                    frameRateLayoutChanged = delegate
                    {
                        frameRateControl.SelectedValue = frameRateData.Options.Where(o => o["value"].ToString().Equals(frameRateData.Value.ToString())).FirstOrDefault()?["value"];
                        this.Layout -= frameRateLayoutChanged;
                    };
                    this.Layout += frameRateLayoutChanged;
                    frameRateControl.SelectedValueChanged += (object sender, EventArgs e) =>
                    {
                        this.Value.Where(prop => prop["name"].ToString() == frameRateControl.Name).First()["value"] = JToken.FromObject(frameRateControl.SelectedValue);
                        OnFormDataChanged?.Invoke(frameRateControl, new FormDataChangedEventArgs { FormData = this.Value });
                    };
                    return frameRateControl;
                //case PropertyType.OBS_PROPERTY_GROUP:
            }

            return new Control();
        }
    }

    public class FormDataChangedEventArgs : EventArgs
    {
        public JArray FormData { get; set; }
    }
}
