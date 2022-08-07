using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
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
            _ = Task.Run(async () =>
            {
                if (!selectedSource.Key.Equals(String.Empty))
                {
                    var source = await PluginCache.SourcesService.GetSourceAsync(selectedSource.Value.Source.Id);
                    var settings = await source.GetSettingsAsync();
                    txtFormData.Invoke((MethodInvoker)delegate
                    {
                        txtFormData.Text = JsonConvert.SerializeObject(settings, Formatting.Indented);
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
                FormData = JsonConvert.DeserializeObject<Dictionary<string, object>>(txtFormData.Text)
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = string.Format("Update source {0} properties", selectedSourceOption.Source.Name); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
