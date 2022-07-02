using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class SceneCollectionsService : BaseService
    {
        public event EventHandler<SceneCollectionsManifestEntry> CollectionSwitched { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
    }
}
