using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SetAudioSourceVolumeActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetAudioSourceVolumeAction _macroDeckAction;

        protected SetAudioSourceVolumeActionConfig _config;

        protected RadioButton checkedButton = null;

        public SetAudioSourceVolumeActionConfigurator(SetAudioSourceVolumeAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetAudioSourceVolumeActionConfig>(action.Configuration) : null;

            var sources = new Dictionary<string, string>();
            foreach (var source in PluginCache.AudioSources)
            {
                sources.Add(source.SourceId, string.Format("{0}", source.Name));
            }
            cbxAudioSource.DataSource = sources.ToList();
            cbxAudioSource.DisplayMember = "Value";
            cbxAudioSource.ValueMember = "Key";

            if (_config != null)
            {
                cbxAudioSource.SelectedValue = !_config.SourceId.Equals(string.Empty) ? _config.SourceId : null;
                sldDeflection.Value = _config.Deflection != null ? (int)(_config.Deflection * 100) : 0;
            }
        }

        public override bool OnActionSave()
        {
            var selectedSource = (KeyValuePair<string, string>)cbxAudioSource.SelectedItem;
            var config = new SetAudioSourceVolumeActionConfig
            {
                SourceId = selectedSource.Key,
                Deflection = (sldDeflection.Value / 100.0)
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = String.Format("Set {0} to {1}%", selectedSource.Value, sldDeflection.Value); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
