using Newtonsoft.Json;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public class NotificationModel : INotificationModel
    {
        [JsonProperty("action")]
        public JsonRpcRequest Action { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("date")]
        public double Date { get; set; }

        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("lifetime")]
        public double LifeTime { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("playSound")]
        public bool PlaySound { get; set; }

        [JsonProperty("showTime")]
        public bool ShowTime { get; set; }

        [JsonProperty("subType")]
        public ENotificationSubType SubType { get; set; }

        [JsonProperty("type")]
        public ENotificationType Type { get; set; }

        [JsonProperty("unread")]
        public bool Unread { get; set; }
    }
}
