using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories
{
    public class EmployeeReader : IEmployeeReader
    {
        public EmployeeManagerContext Context { get; }

        public DbSet<Employee> Employees => Context.Employees;

        public EmployeeReader(EmployeeManagerContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Maybe<Employee>> Read(int employeeId)
        {
            var employee = await Employees.FindAsync(employeeId);

            return employee;
        }
    }
}