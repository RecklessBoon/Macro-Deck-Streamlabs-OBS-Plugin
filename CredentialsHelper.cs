using Newtonsoft.Json;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{
    class CredentialsHelper
    {
        public static void UpsertCredential(string key, string value)
        {
            var oldCreds = PluginCredentials.GetPluginCredentials(PluginCache.Plugin);
            var creds = new Dictionary<string, string>();
            if (oldCreds.Count == 1)
            {
                creds = oldCreds.First();
            }

            creds[key] = value;
            PluginCredentials.SetCredentials(PluginCache.Plugin, creds);
        }

        public static void UpsertCredential(string key, object value)
        {
            UpsertCredential(key, JsonConvert.SerializeObject(value));
        }

        public static void RemoveCredential(string key)
        {
            var oldCreds = PluginCredentials.GetPluginCredentials(PluginCache.Plugin);
            if (oldCreds.Count == 1)
            {
                var creds = oldCreds.First();
                creds.Remove(key);
                PluginCredentials.SetCredentials(PluginCache.Plugin, creds);
            }
        }
    }
}
