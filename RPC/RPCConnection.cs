using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC
{
    public class MessageReceivedArgs : EventArgs
    {
        public JsonRpcResponse Message
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<JsonRpcResponse>(RawMessage);
                } catch (JsonSerializationException) 
                {
                    return null;
                }
            }
        }

        public string RawMessage { get; set; }
    }

    public class RPCConnection : IDisposable
    {
        public event EventHandler<MessageReceivedArgs> OnMessageReceived;
        public event EventHandler OnDisposed;

        protected CancellationTokenSource cts;
        protected TaskCompletionSource<bool> _completionSource = default;
        public Task Completion => _completionSource.Task;

        protected JoinableTaskContext _joinableTaskContext = new JoinableTaskContext();
        protected JoinableTaskFactory _joinableTaskFactory;

        protected PipeReader reader;
        protected PipeWriter writer;

        private bool _disposed = false;
        public bool IsDisposed => _disposed;

        private bool _started = false;
        public bool IsStarted => _started;

        public RPCConnection(PipeStream pipe) : this (PipeReader.Create(pipe), PipeWriter.Create(pipe)) { }

        public RPCConnection(PipeReader reader, PipeWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        protected RPCConnection()
        {
            _joinableTaskFactory = new JoinableTaskFactory(_joinableTaskContext);
            this.cts = new CancellationTokenSource();
            _completionSource = new TaskCompletionSource<bool>();
        }

        public void Start()
        {
            _started = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    await ProcessMessagesAsync(reader, writer, cts.Token);
                }
                catch (Exception ex)
                {
                    _completionSource.SetException(ex);
                }
                _completionSource.SetResult(true);

            });
        }

        public void Notify(JsonRpcRequest request) => _joinableTaskFactory.Run(async () => await NotifyAsync(request));

        public async Task NotifyAsync(JsonRpcRequest request) => await WriteMessagesAsync(writer, request.ToString());

        public async Task<JsonRpcResponse> WriteAsync(JsonRpcRequest request)
        {
            var tcs = new TaskCompletionSource<JsonRpcResponse>();
            AppLogger.Trace("Message being sent:\n{0}", request);
            await NotifyAsync(request);
            EventHandler<MessageReceivedArgs> Watcher = null;
            Watcher = (object sender, MessageReceivedArgs args) =>
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<JsonRpcResponse>(args.RawMessage);
                    if (response.Id == request.Id)
                    {
                        tcs.SetResult(response);
                        this.OnMessageReceived -= Watcher;
                    }
                } 
                catch (Exception jse)
                {
                    Console.WriteLine("Weird serialization error: {0}", jse);
                    this.OnMessageReceived -= Watcher;
                }
            };
            this.OnMessageReceived += Watcher;

            _ = Task.Run(async () =>
            {
                await Task.Delay(30000);
                if (!tcs.Task.IsCompleted)
                {
                    this.OnMessageReceived -= Watcher;
                    tcs.SetException(new TimeoutException("No response given within 30 seconds. Abandonning watcher"));
                }
            });

            return await tcs.Task;
        }

        async Task ProcessMessagesAsync(
           PipeReader reader,
            PipeWriter writer,
            CancellationToken ct)
        {
            try
            {
                while (true && !ct.IsCancellationRequested)
                {
                    ReadResult readResult = await reader.ReadAsync();
                    ReadOnlySequence<byte> buffer = readResult.Buffer;

                    try
                    {
                        if (readResult.IsCanceled)
                        {
                            break;
                        }

                        if (TryParseLines(ref buffer, out string response))
                        {
                            if (response != null)
                            {
                                AppLogger.Trace("Message received:\n{0}", response);
                                foreach (var message in response.Split("\r\n"))
                                {
                                    if (message == "") continue;
                                    OnMessageReceived?.Invoke(this, new MessageReceivedArgs { RawMessage = message.Replace("\\n", "\n") });
                                }
                            }
                        }

                        if (readResult.IsCompleted)
                        {
                            if (!buffer.IsEmpty)
                            {
                                throw new InvalidDataException("Incomplete message.");
                            }
                            break;
                        }
                    }
                    finally
                    {
                        reader.AdvanceTo(buffer.Start, buffer.End);
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
            finally
            {
                await reader.CompleteAsync();
                await writer.CompleteAsync();
            }
        }

        static bool TryParseLines(
            ref ReadOnlySequence<byte> buffer,
            out string message)
        {
            SequencePosition? position;
            StringBuilder outputMessage = new StringBuilder();

            while (true)
            {
                position = buffer.PositionOf((byte)'\n');

                if (!position.HasValue)
                    break;

                outputMessage.Append(Encoding.ASCII.GetString(buffer.Slice(buffer.Start, position.Value).ToArray()))
                            .AppendLine();

                buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
            };

            message = outputMessage.ToString();
            return message.Length != 0;
        }

        static ValueTask<FlushResult> WriteMessagesAsync(
            PipeWriter writer,
            string message) =>
            writer.WriteAsync(Encoding.ASCII.GetBytes(String.Format("{0}\n", message)));

        public void Dispose()
        {
            cts.Cancel();

            _disposed = true;
            OnDisposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
