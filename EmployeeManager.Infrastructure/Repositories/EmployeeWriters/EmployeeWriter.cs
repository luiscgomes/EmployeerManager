using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories.EmployeeWriters
{
    public class EmployeeWriter : IEmployeeWriter
    {
        public EmployeeManagerContext Context { get; }

        public DbSet<Employee> Employees => Context.Employees;

        public EmployeeWriter(EmployeeManagerContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Write(Employee employee)
        {
            await Employees.AddAsync(employee);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Employee employee)
        {
            employee.SetToDeleted();

            Context.Update(employee);
            await Context.SaveChangesAsync();
        }
    }
}