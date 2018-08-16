using AutoFixture.Idioms;
using EmployeeManager.Commons.Notifications;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Repositories.EmployeeWriters;
using EmployeeManager.UnitTests.AutoFixture;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManager.UnitTests.Infrastructure
{
    public class EmployeeWriterWithEmailAlreadyExistsValidationTests
    {
        [Theory, AutoNSubstituteData]
        public void EmployeeWriterWithEmailAlreadyExistsValidation_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(EmployeeWriterWithEmailAlreadyExistsValidation).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Write_When_Email_Already_Exists_Should_Not_Write(
            Employee employee,
            EmployeeWriterWithEmailAlreadyExistsValidation sut)
        {
            sut.EmployeeReader.Any(employee.Email.Address).Returns(true);

            await sut.Write(employee);

            sut.NotificationStore.Received().AddNotification(Arg.Is<Notification>(x => x.Message == "Email already registred"));
            await sut.EmployeeWriter.DidNotReceive().Write(employee);
        }

        [Theory, AutoNSubstituteData]
        public async Task Write_When_Email_Does_Not_Exist_Should_Write(
            Employee employee,
            EmployeeWriterWithEmailAlreadyExistsValidation sut)
        {
            sut.EmployeeReader.Any(employee.Email.Address).Returns(false);

            await sut.Write(employee);

            await sut.EmployeeWriter.Received().Write(employee);
            sut.NotificationStore.DidNotReceive().AddNotification(Arg.Any<Notification>());
        }
    }
}