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

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS
{
    public class FormDataChangedEventArgs : EventArgs
    {
        public JArray FormData { get; set; }
    }

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

            DrawingHelper.BeginControlUpdate(this);
            SuspendLayout();
            foreach (JObject data in Value)
            {
                var property = data.ToObject<PropertyBase>();
                if (!property.Enabled) continue;

                Label label = tblPropertyContainer.Controls.Find(property.Name + "Label", false).OfType<Label>().FirstOrDefault();
                Control control = tblPropertyContainer.Controls.Find(property.Name, false).FirstOrDefault();
                var labelFound = label != null;
                var controlFound = control != null;
                label ??= new Label { Name = property.Name + "Label", Text = property.Description + ":", Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0), Margin = new Padding(2) };
                control ??= CreateControl(data, property, control) ?? new Control();

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

            ResumeLayout(true);
            DrawingHelper.EndControlUpdate(this);
        }

        private Control CreateControl(JObject data, PropertyBase property, Control control)
        {
            switch (property.Type)
            {
                case PropertyType.OBS_PROPERTY_BOOL:
                    var boolData = data.ToObject<Model.OBS.Property.Boolean>();
                    control = new CheckBox { Checked = boolData.Value };
                    control.Click += Checkbox_Clicked;
                    break;
                case PropertyType.OBS_PROPERTY_INT:
                    var intData = data.ToObject<Int>();
                    control = new NumericUpDown { Minimum = intData.MinVal, Maximum = intData.MaxVal, Value = (int)intData.Value, DecimalPlaces = 0 };
                    control.TextChanged += TextBase_TextChanged;
                    break;
                case PropertyType.OBS_PROPERTY_SLIDER:
                    var sliderData = data.ToObject<IntSlider>();
                    control = new TrackBar { BackColor = System.Drawing.Color.FromArgb(45, 45, 45), Maximum = (sliderData.MaxVal + sliderData.StepVal), Minimum = sliderData.MinVal, Value = sliderData.Value, TickFrequency = ((sliderData.MaxVal - sliderData.MinVal) / 20), SmallChange = sliderData.StepVal, TickStyle = TickStyle.Both };
                    control.TextChanged += TextBase_TextChanged;
                    break;
                case PropertyType.OBS_PROPERTY_FLOAT:
                    var floatData = data.ToObject<Float>();
                    control = new NumericUpDown { Minimum = (decimal)floatData.MinVal, Maximum = (decimal)floatData.MaxVal, Value = (decimal)floatData.Value };
                    control.TextChanged += TextBase_TextChanged;
                    break;
                case PropertyType.OBS_PROPERTY_TEXT:
                    var textData = data.ToObject<Text>();
                    control = new RoundedTextBox { Text = textData.Value };
                    control.TextChanged += TextBase_TextChanged;
                    break;
                case PropertyType.OBS_PROPERTY_PATH:
                    var pathData = data.ToObject<Path>();
                    control = new RoundedTextBox { Text = (pathData.Value ?? pathData.DefaultPath), ReadOnly = true };
                    control.TextChanged += TextBase_TextChanged;
                    var fileDialog = new OpenFileDialog { Filter = pathData.Filter, FileName = pathData.Value ?? pathData.DefaultPath, };
                    control.Disposed += (object sender, EventArgs e) =>
                    {
                        fileDialog.Dispose();
                    };
                    control.Click += (object sender, EventArgs e) =>
                    {
                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            control.Text = fileDialog.SafeFileName;
                        }
                    };
                    break;
                case PropertyType.OBS_PROPERTY_FILE:
                    var fileData = data.ToObject<Path>();
                    control = new RoundedTextBox { Text = (fileData.Value ?? fileData.DefaultPath), ReadOnly = true };
                    control.TextChanged += TextBase_TextChanged;
                    var folderDialog = new FolderBrowserDialog { SelectedPath = fileData.Value ?? fileData.DefaultPath };
                    control.Disposed += (object sender, EventArgs e) =>
                    {
                        folderDialog.Dispose();
                    };
                    control.Click += (object sender, EventArgs e) =>
                    {
                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            control.Text = folderDialog.SelectedPath;
                        }
                    };
                    break;
                //case PropertyType.OBS_PROPERTY_LIST:
                case PropertyType.OBS_PROPERTY_COLOR:
                    var colorData = data.ToObject<Model.OBS.Property.Color>();
                    control = new RoundedTextBox { ReadOnly = true };
                    control.TextChanged += TextBase_TextChanged;
                    var colorDialog = new ColorDialog { Color = System.Drawing.Color.FromArgb(colorData.Value) };
                    control.Disposed += (object sender, EventArgs e) =>
                    {
                        colorDialog.Dispose();
                    };
                    control.Click += (object sender, EventArgs e) =>
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            control.BackColor = colorDialog.Color;
                        }
                    };
                    break;
                //case PropertyType.OBS_PROPERTY_BUTTON:
                case PropertyType.OBS_PROPERTY_FONT:
                    var fontData = data.ToObject<Model.OBS.Property.Font>();
                    control = new RoundedTextBox { ReadOnly = true };
                    control.TextChanged += TextBase_TextChanged;
                    fontData.Value["style"] = !fontData.Value["style"].ToString().Equals(String.Empty) ? fontData.Value["style"] : FontStyle.Regular.ToString();
                    var fontDialog = new FontDialog { Font = new System.Drawing.Font(fontData.Value["face"].ToString(), (float)fontData.Value["size"], Enum.Parse<System.Drawing.FontStyle>(fontData.Value["style"].ToString())) };
                    control.Disposed += (object sender, EventArgs e) => 
                    {
                        fontDialog.Dispose();
                    };
                    control.Click += (object sender, EventArgs e) =>
                    {
                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            control.Text = fontDialog.ToString();
                        }
                    };
                    break;
                //case PropertyType.OBS_PROPERTY_EDITABLE_LIST:
                case PropertyType.OBS_PROPERTY_FRAME_RATE:
                    var frameRateData = data.ToObject<FrameRate>();
                    control = new System.Windows.Forms.ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, DataSource = frameRateData.Options, SelectedValue = frameRateData.Value };
                    control.TextChanged += TextBase_TextChanged;
                    break;
                    //case PropertyType.OBS_PROPERTY_GROUP:
            }

            return control;
        }

        private void TextBase_TextChanged(object sender, EventArgs e)
        {
            var control = sender as TextBoxBase;
            this.Value.Where(prop => prop["name"].ToString() == control.Name).First()["value"] = control.Text;
            OnFormDataChanged?.Invoke(control, new FormDataChangedEventArgs { FormData = this.Value });
        }

        private void Checkbox_Clicked(object sender, EventArgs e)
        {
            var control = sender as CheckBox;
            this.Value.Where(prop => prop["name"].ToString() == control.Name).First()["value"] = control.Checked;
            OnFormDataChanged?.Invoke(control, new FormDataChangedEventArgs { FormData = this.Value });
        }
    }
}
