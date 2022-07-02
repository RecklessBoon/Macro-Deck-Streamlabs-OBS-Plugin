using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Dialog;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class Plugin : MacroDeckPlugin
    {
        public override bool CanConfigure => false;

        public Plugin()
        {
            PluginCache.Plugin = this;
        }

        public override void Enable()
        {
            // Do plugin initialization here
            _ = InitClientAsync();

            this.Actions = new List<PluginAction> {
                new FirstAction()
            };
        }

        public override void OpenConfigurator()
        {
            using (var configurator = new Configurator(new Configuration()))
            {
                configurator.ShowDialog();
            }
        }

        protected async Task InitClientAsync()
        {
            if (PluginCache.Connection == null)
            {
                var pipe = new NamedPipeClientStream(".", "slobs", PipeDirection.InOut, PipeOptions.Asynchronous);
                await pipe.ConnectAsync();
                PluginCache.Pipe = pipe;
                var connection = new RPCConnection(pipe);
                var dispatcher = new MessageDispatcher(connection);
                connection.OnDisposed += OnConnectionDisposed;
                PluginCache.Connection = connection;
                PluginCache.Dispatcher = new MessageDispatcher(connection);
                PluginCache.ScenesService = new ScenesService();
                PluginCache.SceneCollectionsService = new SceneCollectionsService();
                connection.Start();
                WireListeners();
                _ = Task.Run(async () => {
                    var scenes = await PluginCache.ScenesService.GetScenesAsync();
                    List<SceneItemFolder> folders = new List<SceneItemFolder>(await scenes[0].GetFoldersAsync());
                    Console.WriteLine("Folders: {0}", String.Join(", ", folders));
                });
            }
        }

        protected void WireListeners()
        {
            PluginCache.ScenesService.SceneSwitched += OnSceneSwitched;
            PluginCache.SceneCollectionsService.CollectionSwitched += OnCollectionSwitched;
        }

        protected void ClipListeners()
        {
            PluginCache.ScenesService.SceneSwitched -= OnSceneSwitched;
            PluginCache.SceneCollectionsService.CollectionSwitched -= OnCollectionSwitched;
        }

        protected void OnSceneSwitched(object sender, ISceneModel scene)
        {
            VariableManager.SetValue("slobs_active_scene_name", scene.Name, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_active_scene_id", scene.Id, VariableType.String, PluginCache.Plugin, null);
        }

        protected void OnCollectionSwitched(object sender, SceneCollectionsManifestEntry collection)
        {
            VariableManager.SetValue("slobs_active_scene_collection_name", collection.Name, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_active_scene_collection_id", collection.Id, VariableType.String, PluginCache.Plugin, null);
        }

        protected void OnConnectionDisposed(object sender, EventArgs args)
        {
            if (!PluginCache.Pipe.IsConnected)
            {
                PluginCache.Pipe.Dispose();
            }

            ClipListeners();
        }
    }

    public static class PluginCache
    {
        public static Plugin Plugin { get; set; }
        public static NamedPipeClientStream Pipe { get; set; }
        public static RPCConnection Connection { get; set; }
        public static MessageDispatcher Dispatcher { get; set; }

        public static ScenesService ScenesService { get; set; }
        public static SceneCollectionsService SceneCollectionsService { get; set; }
    }
}
