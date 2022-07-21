﻿using Newtonsoft.Json;
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
    public partial class SetAudioSourceMuteActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetAudioSourceMuteAction _macroDeckAction;

        protected SetAudioSourceMuteActionConfig _config;

        protected RadioButton checkedButton = null;

        public SetAudioSourceMuteActionConfigurator(SetAudioSourceMuteAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetAudioSourceMuteActionConfig>(action.Configuration) : null;

            var sources = new Dictionary<string, string>();
            foreach (var source in PluginCache.AudioSources)
            {
                sources.Add(source.SourceId, string.Format("{0}", source.Name));
            }
            cbxAudioSource.DataSource = sources.ToList();
            cbxAudioSource.DisplayMember = "Value";
            cbxAudioSource.ValueMember = "Key";

            if (_config != null && !_config.SourceId.Equals(string.Empty))
            {
                cbxAudioSource.SelectedValue = _config.SourceId;
            }

            foreach (SetAudioSourceMuteActionConfig.MuteType opt in (SetAudioSourceMuteActionConfig.MuteType[])Enum.GetValues(typeof(SetAudioSourceMuteActionConfig.MuteType)))
            {
                var btn = new RadioButton() { Text = opt.ToString() };
                btn.CheckedChanged += (object sender, EventArgs e) =>
                {
                    RadioButton rb = (RadioButton)sender;
                    if (rb.Checked)
                    {
                        checkedButton = rb;
                    }
                };
                if (_config != null && _config.Mute.Equals(opt))
                {
                    btn.Checked = true;
                    checkedButton = btn;
                }
                pnlActionWrapper.Controls.Add(btn);
            }
        }

        public override bool OnActionSave()
        {
            var selectedSource = (KeyValuePair<string, string>)cbxAudioSource.SelectedItem;
            var muteValue = Enum.Parse<SetAudioSourceMuteActionConfig.MuteType>(checkedButton.Text);
            var config = new SetAudioSourceMuteActionConfig
            {
                SourceId = selectedSource.Key,
                Mute = muteValue
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = checkedButton.Text+" "+selectedSource.Value; // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
