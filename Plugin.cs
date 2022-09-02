using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Dialog;
using Streamlabs_OBS_Plugin.Services;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class Plugin : MacroDeckPlugin
    {
        public event EventHandler<EventArgs> OnInit;

        public override bool CanConfigure => false;

        public Plugin()
        {
            PluginCache.Plugin = this;
            SuchByte.MacroDeck.MacroDeck.OnMainWindowLoad += MainWindowHelper.MacroDeck_OnMainWindowLoad;
            MainWindowHelper.StatusButtonClicked += (o, e) =>
            {
                try
                {
                    _ = Task.Run(() =>
                    {
                        var RPCClient = PluginCache.Client;
                        if (RPCClient != null && !RPCClient.IsDisposed)
                        {
                            StopClient();
                        }
                        else if (RPCClient == null || RPCClient.IsDisposed || !RPCClient.IsStarted)
                        {
                            InitClient();
                            StartClient();
                        }
                    });
                }
                catch (Exception ex)
                {
                    AppLogger.Error("Unexpected Exception:\n{0}", ex);
                }
            };
        }

        private void InitVariables()
        {
            VariableManager.SetValue("slobs_connected", false, VariableType.Bool, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_replay_buffer_state", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_replay_buffer", false, VariableType.Bool, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_recording_status", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_recording", false, VariableType.Bool, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_streaming_status", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_streaming", false, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_active_scene_name", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_active_scene_id", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_active_scene_collection_name", string.Empty, VariableType.String, PluginCache.Plugin, false);
            VariableManager.SetValue("slobs_active_scene_collection_id", string.Empty, VariableType.String, PluginCache.Plugin, true);
        }

        public override void Enable()
        {
            InitVariables();

            // Do plugin initialization here
            this.Actions = new List<PluginAction> {
                new ToggleConnectionAction(),
                new SwitchCollectionAction(),
                new SwitchSceneAction(),
                new SetStreamingStateAction(),
                new SetRecordingStateAction(),
                new SetReplayBufferStateAction(),
                new SaveReplayAction(),
                new SetAudioSourceMuteAction(),
                new SetAudioSourceVolumeAction(),
                new SetSceneItemSettingsAction(),
                new SetSourcePropertiesAction()
            };

            InitClient();
            StartClient();
        }

        public override void OpenConfigurator()
        {
            using (var configurator = new Configurator(new Configuration()))
            {
                configurator.ShowDialog();
            }
        }

        public Client InitClient()
        {
            var client = PluginCache.Client;
            if (client == null || client.IsDisposed)
            {
                client = new Client();
                PluginCache.Client = client;
                PluginCache.ScenesService = new ScenesService();
                PluginCache.SceneCollectionsService = new SceneCollectionsService();
                PluginCache.StreamingService = new StreamingService();
                PluginCache.AudioService = new AudioService();
                PluginCache.SourcesService = new SourcesService();
                OnInit?.Invoke(this, EventArgs.Empty);
            }

            return client;
        }

        public void StartClient()
        {
            var client = PluginCache.Client;

            client.Started += (object sender, EventArgs e) =>
            {
                MainWindowHelper.CancelConnectingStatusLoop();
                MainWindowHelper.UpdateStatusButton(client.IsStarted);
                VariableManager.SetValue("slobs_connected", client.IsStarted, VariableType.Bool, PluginCache.Plugin, true);
                WireListeners();
                _ = Task.Run(async () =>
                {
                    var collection = await PluginCache.SceneCollectionsService.ActiveCollectionAsync();
                    OnCollectionSwitched(this, collection);
                    PluginCache.CollectionSchemas = await PluginCache.SceneCollectionsService.FetchSceneCollectionsSchemaAsync();
                    PluginCache.AudioSources = await PluginCache.AudioService.GetSourcesAsync();
                });
            };
            client.Disposed += (object sender, EventArgs e) =>
            {
                VariableManager.SetValue("slobs_connected", client.IsStarted, VariableType.Bool, PluginCache.Plugin, true);
                ClipListeners();
                MainWindowHelper.UpdateStatusButton(client.IsStarted);
            };

            MainWindowHelper.StartStatusLoopAsync();
            _ = client.StartAsync();
        }

        public void StopClient()
        {
            PluginCache.Client.Dispose();
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
            VariableManager.SetValue("slobs_streaming", (state != EStreamingState.OFFLINE), VariableType.String, PluginCache.Plugin, null);
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
            _ = Task.Run(async () =>
            {
                var scene = await PluginCache.ScenesService.ActiveSceneAsync();
                OnSceneSwitched(this, scene);
            });
        }
    }

    public static class PluginCache
    {
        public static Plugin Plugin { get; set; }
        public static Client Client { get; set; }

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
