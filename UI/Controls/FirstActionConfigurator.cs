using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class FirstActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private FirstAction _macroDeckAction;

        protected FirstActionConfig _config;

        public FirstActionConfigurator(FirstAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<FirstActionConfig>(action.Configuration) : null;

            txtField1.Text = _config.Field1 ?? "";

            // Do other stuff
        }

        public override bool OnActionSave()
        {
            var config = new FirstActionConfig
            {
                Field1 = txtField1.Text // Actually get the UI value and put it here
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = config.Field1; // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
