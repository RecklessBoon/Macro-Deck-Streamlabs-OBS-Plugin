using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class MessageDispatchedArgs : EventArgs
    {
        public MessageType Type { get; set; }
        public JsonRpcResponse Response { get; set; }
    }

    public class MessageDispatcher
    {
        public event EventHandler<MessageDispatchedArgs> OnMessageDispatched;
        public event EventHandler<MessageDispatchedArgs> OnEventDispatched;

        protected int _requestCount = 0;
        protected RPCConnection _connection;
        public RPCConnection Connection { get => _connection; }

        protected JoinableTaskContext _joinableTaskContext;
        protected JoinableTaskFactory _joinableTaskFactory;

        public MessageDispatcher(RPCConnection connection)
        {
            _joinableTaskContext = new JoinableTaskContext();
            _joinableTaskFactory = new JoinableTaskFactory(_joinableTaskContext);
            _connection = connection;
            _connection.OnMessageReceived += DispatchMessage;
        }

        public void Notify(JsonRpcRequest request) => _joinableTaskFactory.Run(async () => await NotifyAsync(request));

        public async Task NotifyAsync(JsonRpcRequest request)
        {
            request.Id ??= (_requestCount++).ToString();
            await _connection.NotifyAsync(request);
        }

        public JsonRpcResponse Write(JsonRpcRequest request) => _joinableTaskFactory.Run(async () => await WriteAsync(request));

        public async Task<JsonRpcResponse> WriteAsync(JsonRpcRequest request)
        {
            request.Id ??= (_requestCount++).ToString();
            return await _connection.WriteAsync(request);
        }

        protected Dictionary<string, List<EventHandler<MessageDispatchedArgs>>> handlers = new Dictionary<string, List<EventHandler<MessageDispatchedArgs>>>();

        public void Subscribe(string event_service, string event_name, EventHandler callback)
        {
            var emitter = String.Format("{0}.{1}", event_service, event_name);
            if (!handlers.ContainsKey(emitter))
            {
                PluginCache.Dispatcher.Notify(new JsonRpcRequest
                {
                    Method = event_name,
                    Params = new
                    {
                        resource = event_service
                    }
                });

                handlers.Add(emitter, new List<EventHandler<MessageDispatchedArgs>>());
            }

            EventHandler<MessageDispatchedArgs> handler = delegate (object sender, MessageDispatchedArgs e)
            {
                var result = e.Response.Result.ToObject<RPCResult>();
                if (result.ResourceId == emitter)
                {
                    callback?.Invoke(this, EventArgs.Empty);
                }
            };
            handlers[emitter].Add(handler);
            PluginCache.Dispatcher.OnEventDispatched -= handler;
            PluginCache.Dispatcher.OnEventDispatched += handler;
        }

        public void Subscribe<T>(string event_service, string event_name, EventHandler<T> callback)
        {
            var emitter = String.Format("{0}.{1}", event_service, event_name);
            if (!handlers.ContainsKey(emitter))
            {
                PluginCache.Dispatcher.Notify(new JsonRpcRequest
                {
                    Method = event_name,
                    Params = new
                    {
                        resource = event_service
                    }
                });

                handlers.Add(emitter, new List<EventHandler<MessageDispatchedArgs>>());
            }

            EventHandler<MessageDispatchedArgs> handler = delegate (object sender, MessageDispatchedArgs e)
            {
                var result = e.Response.Result.ToObject<RPCResult>();
                if (result.ResourceId == emitter)
                {
                    var obj = result.Data.ToObject<T>();
                    callback?.Invoke(this, obj);
                }
            };
            handlers[emitter].Add(handler);
            PluginCache.Dispatcher.OnEventDispatched -= handler;
            PluginCache.Dispatcher.OnEventDispatched += handler;
        }

        public void Unsubscribe(string event_service, string event_name)
        {
            var emitter = String.Format("{0}.{1}", event_service, event_name);

            var handler_list = handlers[emitter];
            foreach (var handler in handler_list)
            {
                PluginCache.Dispatcher.OnEventDispatched -= handler;
            }

            handlers.Remove(emitter);
            PluginCache.Dispatcher.Notify(new JsonRpcRequest
            {
                Method = "unsubscribe",
                Params = new
                {
                    resource = emitter
                }
            });
        }

        private void DispatchMessage(object sender, MessageReceivedArgs e)
        {
            if (e.Message.Id == null)
            {
                OnEventDispatched?.Invoke(this, new MessageDispatchedArgs { Type = MessageType.EventDispatch, Response = e.Message });
            }
            else
            {
                OnMessageDispatched?.Invoke(this, new MessageDispatchedArgs { Type = MessageType.RequestResponse, Response = e.Message });
            }
        }
    }
}
