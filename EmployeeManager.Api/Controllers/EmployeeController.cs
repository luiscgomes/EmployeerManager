using AutoMapper;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Repositories;
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

        public EmployeeController(
            IEmployeeWriter employeeWriter,
            IEmployeeReader employeeReader)
        {
            EmployeeReader = employeeReader ?? throw new ArgumentNullException(nameof(employeeReader));
            EmployeeWriter = employeeWriter ?? throw new ArgumentNullException(nameof(employeeWriter));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateModel employeeCreateModel)
        {
            var employee = Mapper.Map<Employee>(employeeCreateModel);

            await EmployeeWriter.Write(employee);

            var employeeModel = Mapper.Map<EmployeeModel>(employee);

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