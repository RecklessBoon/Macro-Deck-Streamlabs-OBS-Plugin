using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;
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

        public JsonRpcResponse SendMessage(JsonRpcRequest request)
        {
            return _joinableTaskFactory.Run(async () => await SendMessageAsync(request));
        }

        public async Task<JsonRpcResponse> SendMessageAsync(JsonRpcRequest request)
        {
            request.Id ??= (_requestCount++).ToString();
            return await _connection.WriteAsync(request);
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
