using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Mappers.Extensions;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Api.Controllers
{
    [Route("employee")]
    public class EmployeeQueryController : Controller
    {
        public IEmployeeReader EmployeeReader { get; }

        public EmployeeQueryController(IEmployeeReader employeeReader)
        {
            EmployeeReader = employeeReader ?? throw new ArgumentNullException(nameof(employeeReader));
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> Get(int employeeId)
        {
            var employee = await EmployeeReader.Read(employeeId);

            if (employee.HasNoValue)
                return NotFound();

            var employeeModel = employee.Value.MapTo<EmployeeModel>();

            return Ok(employeeModel);
        }

        [HttpGet]
        public async Task<IActionResult> Get(PageModel pageModel)
        {
            var employees = await EmployeeReader.Read(pageModel.Page, pageModel.PageSize);

            var employeeModels = employees.MapTo<IReadOnlyCollection<EmployeeModel>>();

            return Ok(employeeModels);
        }
    }
}