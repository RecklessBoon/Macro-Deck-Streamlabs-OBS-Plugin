using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Streamlabs_OBS_Plugin.Services
{
    public class ScenesService
    {
        public static Task<Scene[]> GetScenesAsync(JsonRpc client)
        {
            return client.InvokeWithParameterObjectAsync<Scene[]>("getScenes", new { resource = "ScenesService" });
        }
    }
}
