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
    public class SetReplayBufferStateActionConfig
    {
        public enum SetReplayBufferActionType
        {
            None, // Not in UI
            Start,
            Stop,
            Toggle
        }

        private SetReplayBufferActionType _actionType = SetReplayBufferActionType.None;
        public SetReplayBufferActionType ActionType { get => _actionType; set => _actionType = value; }
    }

    public class SetReplayBufferStateAction : PluginAction
    {
        public override string BindableVariable => "slobs_replay_buffer";

        // The name of the action
        public override string Name => "Set Replay Buffer State";

        // A short description what the action can do
        public override string Description => "Start/Stop Replay Buffer";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetReplayBufferStateActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.Client.IsDisposed || !PluginCache.Client.IsStarted ||
                PluginCache.StreamingService.GetType() != typeof(StreamingService)) return;

            var config = JsonConvert.DeserializeObject<SetReplayBufferStateActionConfig>(Configuration);
            switch (config.ActionType) {
                case SetReplayBufferStateActionConfig.SetReplayBufferActionType.Start:
                    _ = PluginCache.StreamingService.StartReplayBufferAsync();
                    break;
                case SetReplayBufferStateActionConfig.SetReplayBufferActionType.Stop:
                    _ = PluginCache.StreamingService.StopReplayBufferAsync();
                    break;
                case SetReplayBufferStateActionConfig.SetReplayBufferActionType.Toggle:
                    if (PluginCache.ReplayBufferState == EReplayBufferState.RUNNING)
                    {
                        _ = PluginCache.StreamingService.StopReplayBufferAsync();
                    } else if (PluginCache.ReplayBufferState == EReplayBufferState.OFFLINE)
                    {
                        _ = PluginCache.StreamingService.StartReplayBufferAsync();
                    }
                    break;
                case SetReplayBufferStateActionConfig.SetReplayBufferActionType.None:
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
