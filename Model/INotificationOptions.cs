using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.RPC;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model
{
    public interface INotificationOptions
    {
        JsonRpcRequest Action { get; set; }
        string Code { get; set; }
        object Data { get; set; }
        double LifeTime { get; set; }
        string Message { get; set; }
        bool PlaySound { get; set; }
        bool ShowTime { get; set; }
        ENotificationSubType SubType { get; set; }
        ENotificationType Type { get; set; }
        bool Unread { get; set; }
    }
}