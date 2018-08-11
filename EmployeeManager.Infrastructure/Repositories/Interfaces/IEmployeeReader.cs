using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories.Interfaces
{
    public interface IEmployeeReader
    {
        Task<Maybe<Employee>> Read(int employeeId);

        Task<IReadOnlyCollection<Employee>> Read(int pageIndex, int pageSize);
    }
}