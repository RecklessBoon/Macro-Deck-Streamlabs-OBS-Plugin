using Newtonsoft.Json;
using SuchByte.MacroDeck.Plugins;
using System.Linq;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin
{

    public class Configuration
    {

        const string VAR_CLIENT_SECRET = "client_secret";


        [JsonIgnore]
        public string ClientSecret { get; set; }

        public Configuration()
        {
            Reload();
        }

        public bool IsFullySet
        {
            get
            {
                return ClientSecret != null;
            }
        }

        public void Save()
        {
            SaveCredentials();
            SaveConfig();
            LoadConfig();
        }

        public void Reload()
        {
            LoadCredentials();
            LoadConfig();
        }

        protected void LoadCredentials()
        {
            var creds = PluginCredentials.GetPluginCredentials(PluginCache.Plugin);
            var data = creds.Count > 0 ? creds.First() : null;
            if (data != null && data.ContainsKey(VAR_CLIENT_SECRET))
            {
                ClientSecret = data[VAR_CLIENT_SECRET];
            }
        }

        protected void SaveCredentials()
        {
            CredentialsHelper.UpsertCredential(VAR_CLIENT_SECRET, ClientSecret);
        }

        protected void LoadConfig()
        {
            var json = PluginConfiguration.GetValue(PluginCache.Plugin, "config");
            if (json != null)
            {
                try
                {
                    var config = JsonConvert.DeserializeObject<Configuration>(json);
                }
                catch { }
            }
        }

        protected void SaveConfig()
        {
            var json = JsonConvert.SerializeObject(this);
            PluginConfiguration.SetValue(PluginCache.Plugin, "config", json);
        }

    }
}