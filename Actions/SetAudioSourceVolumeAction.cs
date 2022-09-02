using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{
    public class SetAudioSourceVolumeActionConfig
    {
        public string SourceId { get; set; }
        public double? Deflection { get; set; }
    }

    public class SetAudioSourceVolumeAction : PluginAction
    {
        // The name of the action
        public override string Name => "Set Audio Source Volume/Deflection";

        // A short description what the action can do
        public override string Description => "Set audio source volume deflection";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetAudioSourceVolumeActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.Client.IsDisposed ||
                !PluginCache.Client.IsStarted ||
                PluginCache.AudioService.GetType() != typeof(AudioService)) return;

            var config = JsonConvert.DeserializeObject<SetAudioSourceVolumeActionConfig>(Configuration);
            if (!config.SourceId.Equals(string.Empty) && config.Deflection != null)
            {
                _ = Task.Run(async () =>
                {
                    var source = await PluginCache.AudioService.GetSourceAsync(config.SourceId);
                    if (source == null) return;
                    await source.SetDeflectionAsync(config.Deflection ?? 0);
                });
            }
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
