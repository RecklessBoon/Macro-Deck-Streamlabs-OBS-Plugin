using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class NotificationsSettings : INotificationsSettings
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("playSound")]
        public bool PlaySound { get; set; }
    }
}
