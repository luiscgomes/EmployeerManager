using EmployeeManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManager.IntegrationTests
{
    internal static class DbContextOptionsFactory
    {
        internal static DbContextOptions Create(string databaseName = null) =>
            new DbContextOptionsBuilder<EmployeeManagerContext>()
                .UseInMemoryDatabase(databaseName: databaseName ?? Guid.NewGuid().ToString())
                .Options;
    }
}