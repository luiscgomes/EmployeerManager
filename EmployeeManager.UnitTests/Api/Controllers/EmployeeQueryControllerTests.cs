using AutoFixture.Idioms;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Controllers;
using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using EmployeeManager.UnitTests.AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManager.UnitTests.Api.Controllers
{
    [Collection("UseAutoMapper")]
    public class EmployeeQueryControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void EmployeeQueryController_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(EmployeeQueryController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Get_When_Employee_Does_Not_Exist_Should_Return_Not_Found(
            int employeeId,
            EmployeeQueryController sut)
        {
            sut.EmployeeReader.Read(employeeId).Returns(Maybe<Employee>.None);

            var actual = await sut.Get(employeeId);

            actual.Should().BeOfType<NotFoundResult>();
        }

        [Theory, AutoNSubstituteData]
        public async Task Get_When_Employee_Exists_Should_Return(
            Employee employee,
            EmployeeQueryController sut)
        {
            sut.EmployeeReader.Read(employee.Id).Returns(employee);

            var actual = await sut.Get(employee.Id);

            actual.As<OkObjectResult>()
                .Value
                .Should()
                .BeOfType<EmployeeModel>()
                .Subject
                .Should()
                .BeEquivalentTo(employee, options => options.ExcludingMissingMembers());
        }

        [Theory, AutoNSubstituteData]
        public async Task Get_Should_Return_Employee_List(
            PageModel pageModel,
            IReadOnlyCollection<Employee> employees,
            EmployeeQueryController sut)
        {
            sut.EmployeeReader.Read(pageModel.Page, pageModel.PageSize).Returns(employees);

            var actual = await sut.Get(pageModel);

            actual.As<OkObjectResult>()
                .Value
                .Should()
                .BeOfType<List<EmployeeModel>>()
                .Subject
                .Should()
                .BeEquivalentTo(employees, options => options.ExcludingMissingMembers());
        }
    }
}