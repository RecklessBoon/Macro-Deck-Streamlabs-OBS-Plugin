using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using SuchByte.MacroDeck.Variables;
using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    public class Client: IDisposable
    {
        public event EventHandler<EventArgs> Started;
        public event EventHandler<EventArgs> Disposed;

        protected NamedPipeClientStream _pipe;
        public NamedPipeClientStream Pipe { get => _pipe; }

        protected RPCConnection _connection;
        public RPCConnection Connection { get => _connection; }

        protected MessageDispatcher _dispatcher;
        public MessageDispatcher Dispatcher { get => _dispatcher; }

        protected bool _isDisposed = false;
        public bool IsDisposed { get => _isDisposed; }

        protected bool _isStarted = false;
        public bool IsStarted { get => _isStarted; }

        protected CancellationTokenSource cts;
        protected TaskCompletionSource<bool> _completionSource = default;

        public Client()
        {
            _pipe = new NamedPipeClientStream(".", "slobs", PipeDirection.InOut, PipeOptions.Asynchronous);
            _connection = new RPCConnection(Pipe);
            _dispatcher = new MessageDispatcher(Connection);
            Connection.Disposed += Connection_OnDisposed;
            Dispatcher.Disposed += Dispatcher_OnDisposed;
            Dispatcher.ErrorDispatched += Dispatcher_OnErrorDispatched;
        }

        public async Task<bool> StartAsync()
        {
            _pipe = new NamedPipeClientStream(".", "slobs", PipeDirection.InOut, PipeOptions.Asynchronous);
            await _pipe.ConnectAsync();
            _ = Task.Run(() =>
            {
                while (_pipe.IsConnected)
                {
                    Thread.Sleep(5000);
                };

                Dispose();
            });

            _connection = new RPCConnection(Pipe);
            _dispatcher = new MessageDispatcher(Connection);
            Connection.Disposed += Connection_OnDisposed;
            Dispatcher.Disposed += Dispatcher_OnDisposed;
            Dispatcher.ErrorDispatched += Dispatcher_OnErrorDispatched;
            Connection.Start();
            if (Connection.IsStarted)
            {
                _isStarted = true;
                VariableManager.SetValue("slobs_connected", true, VariableType.Bool, PluginCache.Plugin, new string[0]);
                Started?.Invoke(this, EventArgs.Empty);
            }

            return (_pipe?.IsConnected ?? false) && (_connection?.IsStarted ?? false);
        }

        private void Dispatcher_OnErrorDispatched(object sender, MessageDispatchedArgs e)
        {
            AppLogger.Error("Invalid response received: {0}", e.Response);
        }

        private void Dispatcher_OnDisposed(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                Dispose();
            }
        }

        protected void Connection_OnDisposed(object sender, EventArgs args)
        {
            if (!IsDisposed)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                _isStarted = false;
                _isDisposed = true;
                VariableManager.SetValue("slobs_connected", false, VariableType.Bool, PluginCache.Plugin, true);
                Pipe?.Dispose();
                Connection?.Dispose();
                Dispatcher?.Dispose();
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
