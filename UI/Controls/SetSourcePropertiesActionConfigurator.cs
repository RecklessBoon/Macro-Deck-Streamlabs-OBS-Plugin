using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SetSourcePropertiesActionConfigurator : ActionConfigControl
    {
        internal class SourceOption
        {
            public Source Source { get; set; }

            public SourceOption(Source value)
            {
                Source = value;
            }

            public override string ToString() => string.Format("{0}", Source.Name);
        }

        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetSourcePropertiesAction _macroDeckAction;

        protected SetSourcePropertiesActionConfig _config;

        public SetSourcePropertiesActionConfigurator(SetSourcePropertiesAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetSourcePropertiesActionConfig>(action.Configuration) : null;

            ddlItem.SelectedIndexChanged += OnSourceChanged;
            _ = PopulateSourcesAsync();
            // Items populate from scene change
        }

        protected async Task PopulateSourcesAsync()
        {
            var sources = await PluginCache.SourcesService.GetSourcesAsync();
            var source_opts = new Dictionary<string, SourceOption>();
            foreach (var source in sources)
            {
                source_opts.Add(source.Id, new SourceOption(source));
            }
            ddlItem.DataSource = source_opts.ToList();
            ddlItem.DisplayMember = "Value";
            ddlItem.ValueMember = "Key";

            if (_config != null && _config.Source != default(Source))
            {
                ddlItem.SelectedValue = _config.Source.Id;
            }
        }

        private void OnSourceChanged(object sender, EventArgs e)
        {
            var selectedSource = (KeyValuePair<string, SourceOption>)ddlItem.SelectedItem;
            tblSettingsContainer.SuspendLayout();
            tblSettingsContainer.Controls.Clear();
            _ = Task.Run(async () =>
            {
                if (!selectedSource.Key.Equals(String.Empty))
                {
                    var source = await PluginCache.SourcesService.GetSourceAsync(selectedSource.Value.Source.Id);
                    var formData = await source.GetPropertiesFormDataAsync();
                    foreach(JObject data in formData)
                    {
                        var property = data.ToObject<PropertyBase>();
                        if (!property.Enabled) continue;

                        var label = new Label { Text = property.Description + ":", Dock = DockStyle.Fill };
                        Control control = new Control();

                        switch (property.Type)
                        {
                            case PropertyType.OBS_PROPERTY_BOOL:
                                var boolData = data.ToObject<Model.OBS.Property.Boolean>();
                                control = new CheckBox { Checked = boolData.Value };
                                break;
                            case PropertyType.OBS_PROPERTY_INT:
                                var intData = data.ToObject<Int>();
                                control = new NumericUpDown { Minimum = intData.MinVal, Maximum = intData.MaxVal, Value = (int)intData.Value, DecimalPlaces = 0 };
                                break;
                            case PropertyType.OBS_PROPERTY_SLIDER:
                                var sliderData = data.ToObject<IntSlider>();
                                control = new TrackBar { Maximum = (sliderData.MaxVal + sliderData.StepVal), Minimum = sliderData.MinVal, Value = sliderData.Value, TickFrequency = ((sliderData.MaxVal - sliderData.MinVal) / 20), SmallChange = sliderData.StepVal, TickStyle = TickStyle.Both };
                                break;
                            case PropertyType.OBS_PROPERTY_FLOAT:
                                var floatData = data.ToObject<Float>();
                                control = new NumericUpDown { Minimum = (decimal)floatData.MinVal, Maximum = (decimal)floatData.MaxVal, Value = (decimal)floatData.Value };
                                break;
                            case PropertyType.OBS_PROPERTY_TEXT:
                                var textData = data.ToObject<Text>();
                                control = new RoundedTextBox { Text = textData.Value };
                                break;
                            case PropertyType.OBS_PROPERTY_PATH:
                                var pathData = data.ToObject<Path>();
                                control = new RoundedTextBox { Text = (pathData.Value ?? pathData.DefaultPath), ReadOnly = true };
                                var fileDialog = new OpenFileDialog { Filter = pathData.Filter, FileName = pathData.Value ?? pathData.DefaultPath,  };
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
                                var folderDialog = new FolderBrowserDialog { SelectedPath = fileData.Value ?? fileData.DefaultPath };
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
                                var colorData = data.ToObject<Color>();
                                control = new RoundedTextBox { ReadOnly = true };
                                var colorDialog = new ColorDialog { Color = System.Drawing.Color.FromArgb(colorData.Value) };
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
                                var fontData = data.ToObject<Font>();
                                control = new RoundedTextBox { ReadOnly = true };
                                var fontDialog = new FontDialog { Font = new System.Drawing.Font(fontData.Value["face"].ToString(), (float)fontData.Value["size"], Enum.Parse<System.Drawing.FontStyle>(fontData.Value["style"].ToString())) };
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
                                break;
                            //case PropertyType.OBS_PROPERTY_GROUP:
                        }

                        control.Dock = DockStyle.Fill;
                        label.Visible = property.Visible;
                        control.Visible = property.Visible;

                        tblSettingsContainer.Invoke((MethodInvoker)delegate
                        {
                            tblSettingsContainer.Controls.Add(label);
                            tblSettingsContainer.Controls.Add(control);
                        });
                        
                    }

                    tblSettingsContainer.Invoke((MethodInvoker)delegate
                    {
                        tblSettingsContainer.ResumeLayout(true);
                    });
                }
            });
        }

        public override bool OnActionSave()
        {
            var selectedSourceOption = (SourceOption)((KeyValuePair<string, SourceOption>)ddlItem.SelectedItem).Value;

            var config = new SetSourcePropertiesActionConfig
            {
                Source = selectedSourceOption.Source,
                //FormData = JsonConvert.DeserializeObject<Dictionary<string, object>>(txtFormData.Text)
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = string.Format("Update source {0} properties", selectedSourceOption.Source.Name); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
