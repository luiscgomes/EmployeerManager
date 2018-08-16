using AutoFixture.Idioms;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories.EmployeeWriters;
using EmployeeManager.IntegrationTests.AutoFixture;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManager.IntegrationTests.Repositories
{
    public class EmployeeWriterTests
    {
        [Theory, AutoNSubstituteData]
        public void EmployeeWriter_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(EmployeeWriter).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Write_Should_Save_Employee(Employee employee)
        {
            var options = DbContextOptionsFactory.Create(nameof(Write_Should_Save_Employee));

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeWriter(context);

                await sut.Write(employee);
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var employeeStoraged = await context.Employees.FindAsync(employee.Id);

                employeeStoraged.Should().NotBeNull();
            }
        }

        [Theory, AutoNSubstituteData]
        public async Task Remove_Should_Delete_Employee(Employee employee)
        {
            var options = DbContextOptionsFactory.Create(nameof(Remove_Should_Delete_Employee));

            using (var context = new EmployeeManagerContext(options))
            {
                context.Add(employee);
                await context.SaveChangesAsync();
            }

            using (var context = new EmployeeManagerContext(options))
            {
                var sut = new EmployeeWriter(context);

                await sut.Remove(employee);
                employee.DeletedAt.Should().NotBeNull();
            }
        }
    }
}