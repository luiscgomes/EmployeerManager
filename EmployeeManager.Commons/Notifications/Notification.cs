using System;

namespace EmployeeManager.Commons.Notifications
{
    public class Notification
    {
        public string Message { get; }

        public Notification(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty or null", nameof(message));

            Message = message;
        }
    }
}