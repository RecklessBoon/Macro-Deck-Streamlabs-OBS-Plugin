using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{
    public class SetRecordingStateActionConfig
    {
        public enum SetRecordingActionType
        {
            None, // Not in UI
            Start,
            Stop,
            Toggle
        }

        private SetRecordingActionType _actionType = SetRecordingActionType.None;
        public SetRecordingActionType ActionType { get => _actionType; set => _actionType = value; }
    }

    public class SetRecordingStateAction : PluginAction
    {
        // The name of the action
        public override string Name => "Set Recording State";

        // A short description what the action can do
        public override string Description => "Start/Stop Recording";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetRecordingStateActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.StreamingService.GetType() != typeof(StreamingService)) return;

            var config = JsonConvert.DeserializeObject<SetRecordingStateActionConfig>(Configuration);
            switch (config.ActionType) {
                case SetRecordingStateActionConfig.SetRecordingActionType.Start:
                    if (PluginCache.RecordingState == ERecordingState.OFFLINE)
                    {
                        _ = PluginCache.StreamingService.ToggleRecordingAsync();
                    }
                    break;
                case SetRecordingStateActionConfig.SetRecordingActionType.Stop:
                    if (PluginCache.RecordingState == ERecordingState.RECORDING)
                    {
                        _ = PluginCache.StreamingService.ToggleRecordingAsync();
                    }
                    break;
                case SetRecordingStateActionConfig.SetRecordingActionType.Toggle:
                    _ = PluginCache.StreamingService.ToggleRecordingAsync();
                    break;
                case SetRecordingStateActionConfig.SetRecordingActionType.None:
                default:
                    break;
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
