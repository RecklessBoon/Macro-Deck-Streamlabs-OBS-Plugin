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
    public partial class SetSceneItemSettingsActionConfigurator : ActionConfigControl
    {
        internal class SceneOption
        {
            public SceneCollectionSchema Parent { get; set; }
            public Scene Scene { get; set; }

            public SceneOption(SceneCollectionSchema parent, Scene scene)
            {
                Parent = parent;
                Scene = scene;
            }

            public override string ToString() => string.Format("[{0}] {1}", Parent.Name, Scene.Name);
        }

        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetSceneItemSettingsAction _macroDeckAction;

        protected SetSceneItemSettingsActionConfig _config;

        public SetSceneItemSettingsActionConfigurator(SetSceneItemSettingsAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this._macroDeckAction = action;
            this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetSceneItemSettingsActionConfig>(action.Configuration) : null;

            PopulateScenes();
            // Items populate from scene change
            PopulateLockedDDLs();
            PopulateVisibleDDLs();

            //cbxRecordingVisible.CheckedChanged += CbxRecordingVisible_CheckedChanged;
            //cbxStreamVisible.CheckedChanged += CbxStreamVisible_CheckedChanged;
        }

        /*private void CbxRecordingVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbxStreamVisible.Checked && !cbxRecordingVisible.Checked)
            {
                cbxStreamVisible.Checked = true;
            }
        }

        private void CbxStreamVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbxStreamVisible.Checked && !cbxRecordingVisible.Checked)
            {
                cbxRecordingVisible.Checked = true;
            }
        }*/

        protected void PopulateScenes()
        {
            var scenes = new Dictionary<string, SceneOption>();
            foreach (var schema in PluginCache.CollectionSchemas)
            {
                foreach (var scene in schema.Scenes)
                {
                    scenes.Add(scene.Id, new SceneOption(schema, scene));
                }
            }
            ddlScene.DataSource = scenes.ToList();
            ddlScene.DisplayMember = "Value";
            ddlScene.ValueMember = "Key";

            ddlScene.SelectedIndexChanged += OnSceneChanged;
            if (_config != null && !_config.SceneId.Equals(string.Empty))
            {
                ddlScene.SelectedValue = _config.SceneId;
            }

        }

        private void OnSceneChanged(object sender, System.EventArgs e)
        {
            var ddl = (System.Windows.Forms.ComboBox)sender;
            var selectedSceneOption = (SceneOption)((KeyValuePair<string, SceneOption>)(ddl.SelectedItem)).Value;
            _ = Task.Run(async () =>
            {
                if (selectedSceneOption != null)
                {
                    var items = await selectedSceneOption.Scene.GetItemsAsync();
                    if (items == null)
                    {
                        ddlItem.Invoke((MethodInvoker)delegate { ddlItem.Items.Clear(); });
                        return;
                    }

                    var itemsList = new Dictionary<string, string>();
                    foreach(var item in items)
                    {
                        itemsList.Add(item.Id, string.Format("{0}", item.Name));
                    }
                    ddlItem.Invoke((MethodInvoker)delegate { 
                        ddlItem.DataSource = itemsList.ToList(); 
                        ddlItem.DisplayMember = "Value";
                        ddlItem.ValueMember = "Key";
                    });

                    if (_config != null && !_config.ItemId.Equals(string.Empty))
                    {
                        ddlItem.Invoke((MethodInvoker)delegate { ddlItem.SelectedValue = _config.ItemId; });
                    }
                }
            });
        }

        private void PopulateLockedDDLs()
        {
            var optList = new Dictionary<int, string>();
            foreach (LockedType opt in (LockedType[])Enum.GetValues(typeof(LockedType)))
            {
                optList.Add((int)opt, opt.ToString());
            }
            ddlLocked.DataSource = optList.ToList();
            ddlLocked.DisplayMember = "Value";
            ddlLocked.ValueMember = "Key";

            if (_config != null && _config.Settings != null && _config.Settings.Locked != null)
            {
                ddlLocked.SelectedValue = (int)_config.Settings.Locked;
            }
        }

        private void PopulateVisibleDDLs()
        {
            var optList = new Dictionary<int, string>();
            foreach (VisibleType opt in (VisibleType[])Enum.GetValues(typeof(VisibleType)))
            {
                optList.Add((int)opt, opt.ToString());
            }

            ddlRecordingVisible.DataSource = optList.ToList();
            ddlRecordingVisible.DisplayMember = "Value";
            ddlRecordingVisible.ValueMember = "Key";
            if (_config != null && _config.Settings != null && _config.Settings.RecordingVisible != null)
            {
                ddlRecordingVisible.SelectedValue = (int)_config.Settings.RecordingVisible;
            }

            ddlStreamVisible.DataSource = optList.ToList();
            ddlStreamVisible.DisplayMember = "Value";
            ddlStreamVisible.ValueMember = "Key";
            if (_config != null && _config.Settings != null && _config.Settings.StreamVisible != null)
            {
                ddlStreamVisible.SelectedValue = (int)_config.Settings.StreamVisible;
            }

            ddlVisible.DataSource = optList.ToList();
            ddlVisible.DisplayMember = "Value";
            ddlVisible.ValueMember = "Key";
            if (_config != null && _config.Settings != null && _config.Settings.Visible != null)
            {
                ddlVisible.SelectedValue = (int)_config.Settings.Visible;
            }
        }

        public override bool OnActionSave()
        {
            var selectedSceneOption = (SceneOption)((KeyValuePair<string, SceneOption>)ddlScene.SelectedItem).Value;
            var selectedItem = (KeyValuePair<string, string>)ddlItem.SelectedItem;
            var config = new SetSceneItemSettingsActionConfig
            {
                SceneId = selectedSceneOption.Scene.Id,
                ItemId = selectedItem.Key,
                Settings = new SetSceneItemSettingsActionConfig.SettingsType()
                {
                    Locked = (LockedType)ddlLocked.SelectedValue,
                    RecordingVisible = (VisibleType)ddlRecordingVisible.SelectedValue,
                    StreamVisible = (VisibleType)ddlStreamVisible.SelectedValue,
                    Visible = (VisibleType)ddlVisible.SelectedValue
                }
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = string.Format("Update {0} in scene {1}", selectedItem.Value, selectedSceneOption); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }
    }
}
