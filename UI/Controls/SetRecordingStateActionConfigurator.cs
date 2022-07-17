using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SetRecordingStateActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetRecordingStateAction _macroDeckAction;

        protected SetRecordingStateActionConfig _config;

        protected RadioButton checkedButton = null;

        public SetRecordingStateActionConfigurator(SetRecordingStateAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetRecordingStateActionConfig>(action.Configuration) : null;

            foreach (SetRecordingStateActionConfig.SetRecordingActionType opt in (SetRecordingStateActionConfig.SetRecordingActionType[])Enum.GetValues(typeof(SetRecordingStateActionConfig.SetRecordingActionType)))
            {
                if (opt == SetRecordingStateActionConfig.SetRecordingActionType.None) continue;

                var btn = new RadioButton() { Text = opt.ToString() };
                btn.CheckedChanged += (object sender, EventArgs e) =>
                {
                    RadioButton rb = (RadioButton)sender;
                    if (rb.Checked) {
                        checkedButton = rb;
                    }
                };
                if (_config != null && _config.ActionType.Equals(opt))
                {
                    btn.Checked = true;
                    checkedButton = btn;
                }
                pnlActionTypeButtonGroup.Controls.Add(btn);
            }
        }

        public override bool OnActionSave()
        {

            var config = new SetRecordingStateActionConfig
            {
                ActionType = Enum.Parse<SetRecordingStateActionConfig.SetRecordingActionType>(checkedButton.Text)
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = checkedButton.Text; // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
