using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager.Commons.Notifications
{
    public class NotificationStore : INotificationStore
    {
        public ICollection<Notification> Notifications { get; }

        public NotificationStore()
        {
            Notifications = new List<Notification>();
        }

        public bool HasNotifications()
        {
            return Notifications.Any();
        }

        public void AddNotification(Notification notification)
        {
            Notifications.Add(notification);
        }
    }
}