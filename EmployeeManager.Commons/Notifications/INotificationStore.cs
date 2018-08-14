using System.Collections.Generic;

namespace EmployeeManager.Commons.Notifications
{
    public interface INotificationStore
    {
        ICollection<Notification> Notifications { get; }

        bool HasNotifications();

        void AddNotification(Notification notification);
    }
}