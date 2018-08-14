using EmployeeManager.Commons.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager.Api.Contracts
{
    public class BadRequestModel
    {
        public IList<Notification> Errors { get; }

        public BadRequestModel()
        {
            this.Errors = new List<Notification>();
        }

        public BadRequestModel(IEnumerable<Notification> notifications)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            if (notifications.Any() == false)
                throw new ArgumentException("Notifications cannot be an empty collection.", nameof(notifications));

            Errors = notifications.ToList();
        }
    }
}