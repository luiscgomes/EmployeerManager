using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var employee = await Employees
                .FindAsync(employeeId);

            return employee;
        }

        public async Task<bool> Any(string email)
        {
            return await Employees.AnyAsync(x => x.Email == email);
        }

        public async Task<IReadOnlyCollection<Employee>> Read(int pageIndex, int pageSize)
        {
            var employees = await Employees
                .AsNoTracking()
                .Skip(GetPageSkip(pageIndex, pageSize))
                .Take(pageSize)
                .Where(x => x.DeletedAt == null)
                .ToListAsync();

            return employees;
        }

        private static int GetPageSkip(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }
    }
}