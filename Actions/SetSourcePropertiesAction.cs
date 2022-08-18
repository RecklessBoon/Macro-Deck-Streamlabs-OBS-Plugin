using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class SetSourcePropertiesActionConfig
    {
        public Source Source { get; set; }

        public JArray FormData { get; set; }
    }

    public class SetSourcePropertiesAction : PluginAction
    {
        // The name of the action
        public override string Name => "Update Source Properties";

        // A short description what the action can do
        public override string Description => "Set source properties";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetSourcePropertiesActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.SourcesService.GetType() != typeof(SourcesService)) return;

            var config = JsonConvert.DeserializeObject<SetSourcePropertiesActionConfig>(Configuration);
            if (config.Source == default(Source) || config.FormData.Equals(string.Empty)) return;

            _ = config.Source.SetPropertiesFormDataAsync(config.FormData);
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
