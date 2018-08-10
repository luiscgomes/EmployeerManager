using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Infrastructure.Data
{
    public class EmployeeManagerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeManagerContext()
        {
        }

        public EmployeeManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}