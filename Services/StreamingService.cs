using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class StreamingService : BaseService
    {
        public event EventHandler<ERecordingState> RecordingStatusChange { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<EReplayBufferState> ReplayBufferStatusChange { add => AddSubscriber(value); remove => RemoveSubscriber(value); }
        public event EventHandler<EStreamingState> StreamingStatusChange { add => AddSubscriber(value); remove => RemoveSubscriber(value); }

        public async Task<StreamingState> GetModelAsync() => await MakeCallAsync<StreamingState>(this.GetType().Name);
        public async Task SaveReplayAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task StartReplayBufferAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task StopReplayBufferAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task ToggleRecordingAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task ToggleStreamingAsync() => await MakeCallAsync(this.GetType().Name);


    }
}
