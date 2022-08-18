using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS;
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

        private Source _DuplicateSource = null;
        public Source DuplicateSource { 
            get => _DuplicateSource;
            set
            {
                if (_DuplicateSource != null)
                {
                    _ = PluginCache.SourcesService.RemoveSourceAsync(_DuplicateSource.Id);
                }
                _DuplicateSource = value;
            }
        }

        public const string DuplicateSourceName = "SLOBS<->MDPLUGIN DUP SOURCE";

        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetSourcePropertiesAction _macroDeckAction;

        protected SetSourcePropertiesActionConfig _config;

        protected Source SelectedSource { get; set; }

        public SetSourcePropertiesActionConfigurator(SetSourcePropertiesAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;

            this._macroDeckAction = action;
            try
            {
                this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetSourcePropertiesActionConfig>(action.Configuration) : null;
            }
            catch (Exception) { }

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

            ddlItem.SelectedValueChanged += OnSourceChanged;
            if (_config != null && _config.Source != default(Source))
            {
                ddlItem.SelectedValue = _config.Source.Id;
            }
        }

        private void OnSourceChanged(object sender, EventArgs e)
        {
            var selectedSource = (KeyValuePair<string, SourceOption>)ddlItem?.SelectedItem;
            SelectedSource = selectedSource.Value.Source;
            DuplicateSource = null;
            
            _ = Task.Run(async () =>
            {
                if (!selectedSource.Key.Equals(String.Empty))
                {
                    var formData = await SelectedSource.GetPropertiesFormDataAsync();
                    PopulateFormData(formData);
                }
            });
        }

        private bool FormDataInit = false;
        private void PopulateFormData(JArray formData)
        {
            if (!FormDataInit && _config?.FormData != null)
            {
                foreach (var token in formData)
                {
                    var config_token = _config.FormData.Where(x => x["name"].ToString().Equals(token["name"].ToString())).First();
                    token["value"] = config_token["value"];
                }
            }
            frmProperties.Value = formData;
            frmProperties.RefreshControls();
            frmProperties.OnFormDataChanged += (object sender, FormDataChangedEventArgs e) =>
            {
                _ = Task.Run(async () =>
                {
                    if (DuplicateSource == null)
                    {
                        var dup = await SelectedSource.DuplicateAsync();
                        _ = dup.SetNameAsync(DuplicateSourceName);
                        dup.Name = DuplicateSourceName;
                        DuplicateSource = dup;
                    }

                    await DuplicateSource.SetPropertiesFormDataAsync(e.FormData);
                    var newFormData = await DuplicateSource.GetPropertiesFormDataAsync();     
                    frmProperties.Value = newFormData;
                    frmProperties.RefreshControls();
                    FormDataInit = true;
                });
            };
        }

        public override bool OnActionSave()
        {
            var selectedSourceOption = (SourceOption)((KeyValuePair<string, SourceOption>)ddlItem.SelectedItem).Value;

            var config = new SetSourcePropertiesActionConfig
            {
                Source = selectedSourceOption.Source,
                FormData = frmProperties.Value
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = string.Format("Update source {0} properties", selectedSourceOption.Source.Name); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
