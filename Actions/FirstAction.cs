using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{
    public class FirstActionConfig
    {
        public string Field1 { get; set; }
    }

    public class FirstAction : PluginAction
    {
        // The name of the action
        public override string Name => "My First Action";

        // A short description what the action can do
        public override string Description => "This action does a thing";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new FirstActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            // Do the thing
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
