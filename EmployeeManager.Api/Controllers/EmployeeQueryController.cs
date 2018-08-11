using AutoMapper;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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

            var employeeModel = Mapper.Map<EmployeeModel>(employee.Value);

            return Ok(employeeModel);
        }

        [HttpGet]
        public async Task<IActionResult> Get(PaginationModel paginationModel)
        {
            var employees = await EmployeeReader.Read(paginationModel.Page, paginationModel.PageSize);

            return Ok(employees);
        }
    }
}