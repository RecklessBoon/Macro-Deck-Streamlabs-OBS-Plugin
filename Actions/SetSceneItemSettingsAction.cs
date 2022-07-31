using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{

    public class SetSceneItemSettingsAction : PluginAction
    {
        // The name of the action
        public override string Name => "Update Source";

        // A short description what the action can do
        public override string Description => "Set source settings";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => false;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return null;
            //return new SetAudioSourceMuteActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.ScenesService.GetType() != typeof(ScenesService)) return;

            var settings = new SceneItemSettings()
            {
                Visible = false
            };

            _ = Task.Run(async () =>
            {
                var scenes = await PluginCache.ScenesService.GetScenesAsync();
                foreach(var scene in scenes)
                {
                    var items = await scene.GetItemsAsync();
                    foreach(var item in items)
                    {
                        var settings = new SceneItemSettings()
                        {
                            Locked = item.Locked,
                            RecordingVisible = item.RecordingVisible,
                            StreamVisible = item.StreamVisible,
                            Transform = item.Transform,
                            Visible = !item.Visible
                        };
                        _ = item.SetSettingsAsync(settings);
                    }
                }
            });

            /*var config = JsonConvert.DeserializeObject<SetAudioSourceMuteActionConfig>(Configuration);
            if (!config.SourceId.Equals(string.Empty))
            {
                _ = Task.Run(async () =>
                {
                    var source = await PluginCache.AudioService.GetSourceAsync(config.SourceId);
                    switch (config.Mute)
                    {
                        case SetAudioSourceMuteActionConfig.MuteType.Mute:
                            await source.SetMutedAsync(true);
                            break;
                        case SetAudioSourceMuteActionConfig.MuteType.Unmute:
                            await source.SetMutedAsync(false);
                            break;
                        case SetAudioSourceMuteActionConfig.MuteType.Toggle:
                        default:
                            if (source.Muted)
                            {
                                await source.SetMutedAsync(false);
                            } 
                            else
                            {
                                await source.SetMutedAsync(true);
                            }
                            break;
                    }
                });
            }*/
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
