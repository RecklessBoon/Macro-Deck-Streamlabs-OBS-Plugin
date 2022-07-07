using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class SelectionService : Selection
    {
        public async Task<string> SceneIdAsync() => await BaseService.MakeCallAsync<string>(this.GetType().Name);
    }
}
