using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class PerformanceService : BaseService
    {
        public async Task<PerformanceState> GetModelAsync() => await MakeCallAsync<PerformanceState>(this.GetType().Name);
    }
}
