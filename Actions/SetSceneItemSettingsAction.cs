using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{
    public enum LockedType
    {
        Unchanged,
        Locked,
        Unlocked,
        Toggle
    }

    public enum VisibleType
    {
        Unchanged,
        Visible,
        Hidden,
        Toggle
    }

    public class SetSceneItemSettingsActionConfig
    {
        protected string sceneId = null;
        public string SceneId { get => sceneId; set => sceneId = value; }

        protected string itemId = null;
        public string ItemId { get => itemId; set => itemId = value; }

        protected SettingsType settings = new SettingsType()
        {
            Locked = LockedType.Unchanged,
            RecordingVisible = VisibleType.Unchanged,
            StreamVisible = VisibleType.Unchanged,
            Visible = VisibleType.Unchanged
        };
        public SettingsType Settings { get => settings; set => settings = value; }

        public class SettingsType
        {
            public LockedType? Locked { get; set; }
            public VisibleType? RecordingVisible { get; set; }
            public VisibleType? StreamVisible { get; set; }
            public VisibleType? Visible { get; set; }
        }
    }

    public class SetSceneItemSettingsAction : PluginAction
    {
        // The name of the action
        public override string Name => "Update Source";

        // A short description what the action can do
        public override string Description => "Set source settings";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetSceneItemSettingsActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.ScenesService.GetType() != typeof(ScenesService)) return;

            var config = JsonConvert.DeserializeObject<SetSceneItemSettingsActionConfig>(Configuration);
            _ = Task.Run(async () =>
            {
                var scene = await PluginCache.ScenesService.GetSceneAsync(config.SceneId);
                var item = await scene.GetItemAsync(config.ItemId);
                var settings = new SceneItemSettings()
                {
                    Locked = item.Locked,
                    RecordingVisible = item.RecordingVisible,
                    StreamVisible = item.StreamVisible,
                    Visible = item.Visible
                };

                switch (config.Settings.Locked)
                {
                    case LockedType.Toggle:
                        settings.Locked = !settings.Locked;
                        break;
                    case LockedType.Locked:
                        settings.Locked = true;
                        break;
                    case LockedType.Unlocked:
                        settings.Locked = false;
                        break;
                }

                switch (config.Settings.RecordingVisible)
                {
                    case VisibleType.Toggle:
                        settings.RecordingVisible = !settings.RecordingVisible;
                        break;
                    case VisibleType.Visible:
                        settings.RecordingVisible = true;
                        break;
                    case VisibleType.Hidden:
                        settings.RecordingVisible = false;
                        break;
                }

                switch (config.Settings.StreamVisible)
                {
                    case VisibleType.Toggle:
                        settings.StreamVisible = !settings.StreamVisible;
                        break;
                    case VisibleType.Visible:
                        settings.StreamVisible = true;
                        break;
                    case VisibleType.Hidden:
                        settings.StreamVisible = false;
                        break;
                }

                switch (config.Settings.Visible)
                {
                    case VisibleType.Toggle:
                        settings.Visible = !settings.Visible;
                        break;
                    case VisibleType.Visible:
                        settings.Visible = true;
                        break;
                    case VisibleType.Hidden:
                        settings.Visible = false;
                        break;
                }

                _ = item.SetSettingsAsync(settings);
            });
        }

        // Optional; Gets called when the action button gets deleted
        public override void OnActionButtonDelete()
        {

        }

        // Optional; Gets called when the action button is loaded
        public override void OnActionButtonLoaded()
        {

        }
    }
}
