using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Streamlabs_OBS_Plugin.Services
{
    public class ScenesService
    {
        public event EventHandler<Scene> OnSceneSwitched
        {
            add
            {
                PluginCache.Dispatcher.Subscribe<Scene>("ScenesService", "sceneSwitched", value);
            }
            remove 
            {
                PluginCache.Dispatcher.Unsubscribe("ScenesService", "sceneSwitched");
            }
        }
    }
}
