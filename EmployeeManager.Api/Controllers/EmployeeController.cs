using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Mappers.Extensions;
using EmployeeManager.Commons.Notifications;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeManager.Api.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        public IEmployeeWriter EmployeeWriter { get; }

        public IEmployeeReader EmployeeReader { get; }

        public INotificationStore NotificationStore { get; }

        public EmployeeController(
            IEmployeeWriter employeeWriter,
            IEmployeeReader employeeReader,
            INotificationStore notificationStore)
        {
            NotificationStore = notificationStore ?? throw new ArgumentNullException(nameof(notificationStore));
            EmployeeReader = employeeReader ?? throw new ArgumentNullException(nameof(employeeReader));
            EmployeeWriter = employeeWriter ?? throw new ArgumentNullException(nameof(employeeWriter));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateModel employeeCreateModel)
        {
            var employee = employeeCreateModel.MapTo<Employee>();

            await EmployeeWriter.Write(employee);

            if (NotificationStore.HasNotifications())
            {
                var badRequestModel = new BadRequestModel(NotificationStore.Notifications);

                return BadRequest(badRequestModel);
            }

            var employeeModel = employee.MapTo<EmployeeModel>();

            return Created(string.Empty, employeeModel);
        }

        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> Remove(int employeeId)
        {
            var employee = await EmployeeReader.Read(employeeId);

            if (employee.HasNoValue)
                return NotFound();

            await EmployeeWriter.Remove(employee.Value);

            return Ok();
        }
    }
}