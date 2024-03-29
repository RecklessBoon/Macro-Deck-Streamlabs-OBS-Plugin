﻿using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions
{
    public class SwitchSceneActionConfig
    {
        public string SceneId { get; set; }
    }

    public class SwitchSceneAction : PluginAction
    {
        // The name of the action
        public override string Name => "Switch Scene";

        // A short description what the action can do
        public override string Description => "Switch scene within the current site collection";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SwitchSceneActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginCache.Client.IsDisposed ||
                !PluginCache.Client.IsStarted ||
                PluginCache.ScenesService.GetType() != typeof(ScenesService)) return;

            var config = JsonConvert.DeserializeObject<SwitchSceneActionConfig>(Configuration);
            if (!config.SceneId.Equals(string.Empty) && PluginCache.ActiveScene?.Id != config.SceneId)
            {
                _ = PluginCache.ScenesService.MakeSceneActiveAsync(config.SceneId);
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
