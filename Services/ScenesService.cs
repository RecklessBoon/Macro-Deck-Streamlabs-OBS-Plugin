using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Streamlabs_OBS_Plugin.Services
{
    public class ScenesService : BaseService
    {
        public async Task<Scene> ActiveSceneAsync() => await MakeCallAsync<Scene>(this.GetType().Name);
        public async Task<string> ActiveSceneIdAsync() => await MakeCallAsync<string>(this.GetType().Name);

        public event EventHandler<SceneItemModel> ItemAdded { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SceneItemModel> ItemRemoved { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<SceneItemModel> ItemUpdated { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<Scene> SceneAdded { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<Scene> SceneRemoved { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<Scene> SceneSwitched { add => AddSubscriber(value); remove => RemoveSubscriber(value); }

        public async Task<Scene> CreateSceneAsync(string name) => await MakeCallAsync<Scene>(this.GetType().Name, name);
        public async Task<Scene> GetSceneAsync(string id) => await MakeCallAsync<Scene>(this.GetType().Name, id);
        public async Task<Scene[]> GetScenesAsync() => await MakeCallAsync<Scene[]>(this.GetType().Name);
        public async Task<bool> MakeSceneActiveAsync(string id) => await MakeCallAsync<bool>(this.GetType().Name, id);
        public async Task<Scene> RemoveSceneAsync(string id) => await MakeCallAsync<Scene>(this.GetType().Name, id);
    }
}
