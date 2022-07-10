using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System.Collections.Generic;
using System.Linq;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SwitchSceneActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SwitchSceneAction _macroDeckAction;

        protected SwitchSceneActionConfig _config;

        public SwitchSceneActionConfigurator(SwitchSceneAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SwitchSceneActionConfig>(action.Configuration) : null;

            var scenes = new Dictionary<string, string>();
            foreach (var schema in PluginCache.CollectionSchemas)
            {
                foreach (var scene in schema.Scenes)
                {
                    scenes.Add(scene.Id, string.Format("[{0}] {1}", schema.Name, scene.Name));
                }
            }
            cbxScene.DataSource = scenes.ToList();
            cbxScene.DisplayMember = "Value";
            cbxScene.ValueMember = "Key";

            if (_config != null && !_config.SceneId.Equals(string.Empty))
            {
                cbxScene.SelectedValue = _config.SceneId;
            }
        }

        public override bool OnActionSave()
        {
            var selectedScene = (KeyValuePair<string, string>)cbxScene.SelectedItem;
            var config = new SwitchSceneActionConfig
            {
                SceneId = selectedScene.Key
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = "Switch to "+selectedScene.Value; // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
