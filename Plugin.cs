using Newtonsoft.Json.Converters;
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
                new SwitchCollectionAction(),
                new SwitchSceneAction(),
                new SetStreamingStateAction(),
                new SetRecordingStateAction(),
                new SetReplayBufferStateAction(),
                new SaveReplayAction(),
                new SetAudioSourceMuteAction(),
                new SetSceneItemSettingsAction(),
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
                PluginCache.StreamingService = new StreamingService();
                PluginCache.AudioService = new AudioService();
                PluginCache.SourcesService = new SourcesService();
                connection.Start();
                WireListeners();
                var collection = await PluginCache.SceneCollectionsService.ActiveCollectionAsync();
                OnCollectionSwitched(this, collection);
                PluginCache.CollectionSchemas = await PluginCache.SceneCollectionsService.FetchSceneCollectionsSchemaAsync();
                PluginCache.AudioSources = await PluginCache.AudioService.GetSourcesAsync();
            }
        }

        protected void WireListeners()
        {
            PluginCache.ScenesService.SceneSwitched += OnSceneSwitched;
            PluginCache.SceneCollectionsService.CollectionSwitched += OnCollectionSwitched;
            PluginCache.StreamingService.StreamingStatusChange += OnStreamingStatusChanged;
            PluginCache.StreamingService.RecordingStatusChange += OnRecordingStatusChanged;
            PluginCache.StreamingService.ReplayBufferStatusChange += OnReplayBufferStatusChanged;
        }

        protected void ClipListeners()
        {
            PluginCache.ScenesService.SceneSwitched -= OnSceneSwitched;
            PluginCache.SceneCollectionsService.CollectionSwitched -= OnCollectionSwitched;
            PluginCache.StreamingService.StreamingStatusChange -= OnStreamingStatusChanged;
            PluginCache.StreamingService.RecordingStatusChange -= OnRecordingStatusChanged;
            PluginCache.StreamingService.ReplayBufferStatusChange -= OnReplayBufferStatusChanged;
        }

        private void OnReplayBufferStatusChanged(object sender, EReplayBufferState state)
        {
            PluginCache.ReplayBufferState = state;
            VariableManager.SetValue("slobs_replay_buffer_status", state, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_replay_buffer", (state != EReplayBufferState.OFFLINE), VariableType.Bool, PluginCache.Plugin, null);
        }

        private void OnRecordingStatusChanged(object sender, ERecordingState state)
        {
            PluginCache.RecordingState = state;
            VariableManager.SetValue("slobs_recording_status", state, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_recording", (state != ERecordingState.OFFLINE), VariableType.String, PluginCache.Plugin, null);
        }

        private void OnStreamingStatusChanged(object sender, EStreamingState state)
        {
            PluginCache.StreamingState = state;
            VariableManager.SetValue("slobs_streaming_status", state, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_recording", (state != EStreamingState.OFFLINE), VariableType.String, PluginCache.Plugin, null);
        }

        protected void OnSceneSwitched(object sender, ISceneModel scene)
        {
            PluginCache.ActiveScene = (Scene)scene;
            VariableManager.SetValue("slobs_active_scene_name", scene.Name, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_active_scene_id", scene.Id, VariableType.String, PluginCache.Plugin, null);
        }

        protected void OnCollectionSwitched(object sender, SceneCollectionsManifestEntry collection)
        {
            PluginCache.ActiveCollection = collection;
            VariableManager.SetValue("slobs_active_scene_collection_name", collection.Name, VariableType.String, PluginCache.Plugin, null);
            VariableManager.SetValue("slobs_active_scene_collection_id", collection.Id, VariableType.String, PluginCache.Plugin, null);
            _ = Task.Run(async () => {
                var scene = await PluginCache.ScenesService.ActiveSceneAsync();
                OnSceneSwitched(this, scene);
            });
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
        public static StreamingService StreamingService { get; set; }
        public static AudioService AudioService { get; set; }
        public static SourcesService SourcesService { get; set; }
        public static SceneCollectionSchema[] CollectionSchemas { get; set; }
        public static AudioSource[] AudioSources { get; set; }
        public static SceneCollectionsManifestEntry ActiveCollection { get; set; }
        public static Scene ActiveScene { get; set; }
        public static EReplayBufferState ReplayBufferState { get; set; }
        public static EStreamingState StreamingState { get; set; }
        public static ERecordingState RecordingState { get; set; }
    }
}
