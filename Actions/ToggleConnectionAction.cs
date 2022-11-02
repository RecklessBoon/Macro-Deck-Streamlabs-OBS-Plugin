using Newtonsoft.Json;
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
    public class ToggleConnectionAction : PluginAction
    {
        public override string BindableVariable => "slobs_connected";

        // The name of the action
        public override string Name => "Toggle Connection";

        // A short description what the action can do
        public override string Description => "Connect/Disconnect with the Streamlabs OBS Desktop client";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => false;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return null;
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var client = PluginCache.Client;

            if (client == null || client.IsDisposed)
            {
                PluginCache.Plugin.InitClient();
                PluginCache.Plugin.StartClient();
            } else if (client != null && !client.IsStarted)
            {
                PluginCache.Plugin.StartClient();
            }
            else
            {
                PluginCache.Plugin.StopClient();
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
