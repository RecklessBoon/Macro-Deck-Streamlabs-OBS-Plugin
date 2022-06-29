using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Dialog;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
                connection.OnMessageReceived += OnMessageReceived;
                connection.OnDisposed += OnConnectionDisposed;
                PluginCache.Connection = connection;
                PluginCache.Dispatcher = new MessageDispatcher(connection);
                WireListeners();
                connection.Start();
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedArgs e)
        {
            var result = e.Message.Result;
            switch (result["resourceId"].ToString())
            {
                case "ScenesService.sceneSwitched":
                    if (result["_type"].ToString() == "EVENT")
                    {
                        var scene = result["data"].ToObject<Scene>();
                        VariableManager.SetValue("slobs_active_scene_name", scene.Name, VariableType.String, PluginCache.Plugin);
                        VariableManager.SetValue("slobs_active_scene_id", scene.Id, VariableType.String, PluginCache.Plugin);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void WireListeners()
        {
            _ = PluginCache.Dispatcher.SendMessageAsync(new JsonRpcRequest
            {
                Method = "sceneSwitched",
                Params = new
                {
                    resource = "ScenesService"
                }
            });
        }

        protected void ClipListeners()
        {
            _ = PluginCache.Dispatcher.SendMessageAsync(new JsonRpcRequest
            {
                Method = "unsubscribe",
                Params = new
                {
                    resource = "ScneseService.sceneSwitched"
                }
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
    }
}
