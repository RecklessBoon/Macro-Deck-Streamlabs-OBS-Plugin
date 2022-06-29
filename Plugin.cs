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
                await InitVariablesAsync();
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
                        var data = e.Message.Result["data"];
                        VariableManager.SetValue("slobs_active_scene", data["name"].ToString(), VariableType.String, PluginCache.Plugin);
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

        protected async Task InitVariablesAsync()
        {
            var dispatcher = PluginCache.Dispatcher;
            if (dispatcher != null && dispatcher.Connection != null && dispatcher.Connection.IsDisposed == false)
            {
                var response = await dispatcher.SendMessageAsync(new JsonRpcRequest
                {
                    Method = "activeScene",
                    Params = new
                    {
                        resource = "ScenesService"
                    }
                });

                var scene = response.Result.ToObject<Scene>();
                VariableManager.SetValue("slobs_active_scene", scene.Name, VariableType.String, PluginCache.Plugin);
            }
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
