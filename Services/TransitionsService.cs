using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class TransitionsService : BaseService
    {
        public event EventHandler<bool> StudioModeChanged { add => AddSubscriber(value); remove => RemoveSubscriber(value); }

        public async Task DisableStudioModeAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task EnableStudioModeAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task ExecuteStudioModeTransitionAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task<TransitionsService> GetModelAsync() => await MakeCallAsync<TransitionsService>(this.GetType().Name);
    }
}
