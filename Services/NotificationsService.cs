using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Services
{
    public class NotificationsService : BaseService
    {
        public async Task ApplyActionAsync(double notificationId) => await MakeCallAsync(this.GetType().Name, new { notificationId });
        public async Task<NotificationModel[]> GetAllAsync(ENotificationType type) => await MakeCallAsync<NotificationModel[]>(this.GetType().Name, new { type });
        public async Task<NotificationModel> GetAllAsync(double id) => await MakeCallAsync<NotificationModel>(this.GetType().Name, new { id });
        public async Task<NotificationModel[]> GetReadAsync(ENotificationType type) => await MakeCallAsync<NotificationModel[]>(this.GetType().Name, new { type });
        public async Task<NotificationsSettings> GetSettingsAsync() => await MakeCallAsync<NotificationsSettings>(this.GetType().Name);
        public async Task<NotificationModel[]> GetUnreadAsync(ENotificationType type) => await MakeCallAsync<NotificationModel[]>(this.GetType().Name, new { type });
        public async Task MarkAllAsReadAsync(double id) => await MakeCallAsync(this.GetType().Name, new { id });
        public async Task<NotificationModel> PushAsync(NotificationOptions notifyInfo) => await MakeCallAsync<NotificationModel>(this.GetType().Name, new { notifyInfo });
        public async Task RestoreDefaultsAsync() => await MakeCallAsync(this.GetType().Name);
        public async Task SetSettingsAsync(NotificationsSettings patch) => await MakeCallAsync(this.GetType().Name, new { patch });
        public async Task ShowNotificationsAsync() => await MakeCallAsync(this.GetType().Name);
    }
}
