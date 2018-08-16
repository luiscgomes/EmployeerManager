using AutoFixture.Idioms;
using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories;
using EmployeeManager.IntegrationTests.AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManager.IntegrationTests.Repositories
{
    public class EmployeeReaderTests
    {
        [Theory, AutoNSubstituteData]
        public void EmployeeReader_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(EmployeeReader).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Read_When_EmployeeId_Exists_Should_Return_Employee(Employee employee)
        {
            var options = DbContextOptionsFactory.Create(nameof(Read_When_EmployeeId_Exists_Should_Return_Employee));

            using (var context = new EmployeeManagerContext(options))
            {
                context.Add(employee);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeReader(context);

                var actual = await sut.Read(employee.Id);

                actual.Should().NotBeNull();
                actual.Value.Id.Should().Be(employee.Id);
            }
        }

        [Theory, AutoNSubstituteData]
        public async Task Read_When_EmployeeId_Does_Not_Exist_Should_Return_Maybe_None(
            Employee employee,
            int someEmployeeId)
        {
            var options = DbContextOptionsFactory.Create(nameof(Read_When_EmployeeId_Does_Not_Exist_Should_Return_Maybe_None));

            using (var context = new EmployeeManagerContext(options))
            {
                context.Add(employee);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeReader(context);

                var actual = await sut.Read(someEmployeeId);

                actual.Should().Be(Maybe<Employee>.None);
            }
        }

        [Theory, AutoNSubstituteData]
        public async Task Any_When_EmailAddress_Exists_Should_Return_True(
            Employee employee)
        {
            var options = DbContextOptionsFactory.Create(nameof(Any_When_EmailAddress_Exists_Should_Return_True));

            using (var context = new EmployeeManagerContext(options))
            {
                context.Add(employee);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeReader(context);

                var actual = await sut.Any(employee.Email.Address);

                actual.Should().BeTrue();
            }
        }

        [Theory, AutoNSubstituteData]
        public async Task Any_When_EmailAddress_Does_Not_Exist_Should_Return_False(
            Employee employee,
            string someEmployeeEmail)
        {
            var options = DbContextOptionsFactory.Create(nameof(Any_When_EmailAddress_Does_Not_Exist_Should_Return_False));

            using (var context = new EmployeeManagerContext(options))
            {
                context.Add(employee);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeReader(context);

                var actual = await sut.Any(someEmployeeEmail);

                actual.Should().BeFalse();
            }
        }

        [Theory, AutoNSubstituteData]
        public async Task Read_Should_Paginated_Employees(List<Employee> employees)
        {
            const int pageIndex = 2;
            const int pageSize = 1;

            var options = DbContextOptionsFactory.Create(nameof(Any_When_EmailAddress_Does_Not_Exist_Should_Return_False));

            using (var context = new EmployeeManagerContext(options))
            {
                context.AddRange(employees);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeReader(context);

                var actual = await sut.Read(pageIndex, pageSize);

                actual.Count.Should().Be(pageSize);
            }
        }
    }
}