using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class SceneCollectionsService
    {
        public event EventHandler<SceneCollectionsManifestEntry> OnCollectionSwitched
        {
            add
            {
                PluginCache.Dispatcher.Subscribe<SceneCollectionsManifestEntry>("SceneCollectionsService", "collectionSwitched", value);
            }
            remove
            {
                PluginCache.Dispatcher.Unsubscribe("ScenesService", "sceneSwitched");
            }
        }
    }
}
