using EmployeeManager.Commons.Notifications;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories.EmployeeWriters
{
    public class EmployeeWriterWithEmailAlreadyExistsValidation : IEmployeeWriter
    {
        public IEmployeeWriter EmployeeWriter { get; }

        public IEmployeeReader EmployeeReader { get; }

        public INotificationStore NotificationStore { get; }

        public EmployeeWriterWithEmailAlreadyExistsValidation(
            IEmployeeWriter employeeWriter,
            IEmployeeReader employeeReader,
            INotificationStore notificationStore)
        {
            NotificationStore = notificationStore ?? throw new ArgumentNullException(nameof(notificationStore));
            EmployeeWriter = employeeWriter ?? throw new ArgumentNullException(nameof(employeeWriter));
            EmployeeReader = employeeReader ?? throw new ArgumentNullException(nameof(employeeReader));
        }

        public async Task Write(Employee employee)
        {
            var emailAlreadyExists = await EmployeeReader.Any(employee.Email.Address);

            if (emailAlreadyExists)
            {
                var notification = new Notification("Email already registred");
                NotificationStore.AddNotification(notification);

                return;
            }

            await EmployeeWriter.Write(employee);
        }

        public async Task Remove(Employee employee)
        {
            await EmployeeWriter.Remove(employee);
        }
    }
}