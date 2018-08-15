using AutoFixture.Idioms;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Controllers;
using EmployeeManager.UnitTests.AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManager.UnitTests.Api.Controllers
{
    [Collection("UseAutoMapper")]
    public class EmployeeControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void EmployeeController_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(EmployeeController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Create_When_There_Are_No_Notifications_Should_Create_Employee(
            EmployeeCreateModel employeeCreateModel,
            EmployeeController sut)
        {
            sut.NotificationStore.HasNotifications().Returns(false);

            var actual = await sut.Create(employeeCreateModel);

            actual.Should().BeOfType<CreatedResult>();

            actual.As<CreatedResult>()
                .Value
                .Should()
                .BeOfType<EmployeeModel>()
                .Subject
                .Should()
                .BeEquivalentTo(employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public async Task Create_When_There_Are_Notifications_Should_Return_BadRequest(
            EmployeeCreateModel employeeCreateModel,
            EmployeeController sut)
        {
            sut.NotificationStore.HasNotifications().Returns(true);

            var actual = await sut.Create(employeeCreateModel);

            actual.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}