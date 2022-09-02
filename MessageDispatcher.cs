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

    public class MessageDispatcher: IDisposable
    {
        public event EventHandler<MessageDispatchedArgs> MessageDispatched;
        public event EventHandler<MessageDispatchedArgs> EventDispatched;
        public event EventHandler<MessageDispatchedArgs> ErrorDispatched;
        public event EventHandler<EventArgs> Disposed;

        protected bool _isDisposed = false;
        public bool IsDisposed { get => _isDisposed; }

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
            _connection.MessageReceived += DispatchMessage;
        }

        public void Notify(JsonRpcRequest request) => _joinableTaskFactory.Run(async () => await NotifyAsync(request));

        public async Task NotifyAsync(JsonRpcRequest request)
        {
            if (IsDisposed || _connection.IsDisposed) return;

            request.Id ??= (_requestCount++).ToString();
            await _connection.NotifyAsync(request);
        }

        public JsonRpcResponse Write(JsonRpcRequest request) => _joinableTaskFactory.Run(async () => await WriteAsync(request));

        public async Task<JsonRpcResponse> WriteAsync(JsonRpcRequest request)
        {
            if (IsDisposed || _connection.IsDisposed) return null;

            request.Id ??= (_requestCount++).ToString();
            return await _connection.WriteAsync(request);
        }

        protected Dictionary<string, List<EventHandler<MessageDispatchedArgs>>> handlers = new Dictionary<string, List<EventHandler<MessageDispatchedArgs>>>();

        public void Subscribe(string event_service, string event_name, EventHandler callback)
        {
            if (IsDisposed || _connection.IsDisposed) return;

            var emitter = String.Format("{0}.{1}", event_service, event_name);
            if (!handlers.ContainsKey(emitter))
            {
                Notify(new JsonRpcRequest
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
            EventDispatched -= handler;
            EventDispatched += handler;
        }

        public void Subscribe<T>(string event_service, string event_name, EventHandler<T> callback)
        {
            if (IsDisposed || _connection.IsDisposed) return;

            var emitter = String.Format("{0}.{1}", event_service, event_name);
            if (!handlers.ContainsKey(emitter))
            {
                Notify(new JsonRpcRequest
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
            EventDispatched -= handler;
            EventDispatched += handler;
        }

        public void Unsubscribe(string event_service, string event_name)
        {
            if (IsDisposed || _connection.IsDisposed) return;

            var emitter = String.Format("{0}.{1}", event_service, event_name);

            var handler_list = handlers[emitter];
            foreach (var handler in handler_list)
            {
                EventDispatched -= handler;
            }

            handlers.Remove(emitter);
            Notify(new JsonRpcRequest
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
            if (IsDisposed) return;

            if (e.Message.Error != null)
            {
                ErrorDispatched?.Invoke(this, new MessageDispatchedArgs { Type = MessageType.ErrorThrown, Response = e.Message });
            }
            else if (e.Message.Id == null)
            {
                EventDispatched?.Invoke(this, new MessageDispatchedArgs { Type = MessageType.EventDispatch, Response = e.Message });
            }
            else
            {
                MessageDispatched?.Invoke(this, new MessageDispatchedArgs { Type = MessageType.RequestResponse, Response = e.Message });
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
            MessageDispatched = default;
            EventDispatched = default;
            ErrorDispatched = default;
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
