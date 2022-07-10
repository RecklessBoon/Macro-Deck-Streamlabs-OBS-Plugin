using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System.Collections.Generic;
using System.Linq;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SwitchCollectionActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SwitchCollectionAction _macroDeckAction;

        protected SwitchCollectionActionConfig _config;

        public SwitchCollectionActionConfigurator(SwitchCollectionAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SwitchCollectionActionConfig>(action.Configuration) : null;

            var collections = new Dictionary<string, string>();
            foreach (var schema in PluginCache.CollectionSchemas)
            {
                collections.Add(schema.Id, string.Format("{0}", schema.Name));
            }
            cbxCollection.DataSource = collections.ToList();
            cbxCollection.DisplayMember = "Value";
            cbxCollection.ValueMember = "Key";

            if (_config != null && !_config.CollectionId.Equals(string.Empty))
            {
                cbxCollection.SelectedValue = _config.CollectionId;
            }
        }

        public override bool OnActionSave()
        {
            var selectedCollection = (KeyValuePair<string, string>)cbxCollection.SelectedItem;
            var config = new SwitchCollectionActionConfig
            {
                CollectionId = selectedCollection.Key
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = "Switch to "+selectedCollection.Value; // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
