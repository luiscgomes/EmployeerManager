using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories
{
    public interface IEmployeeReader
    {
        Task<Maybe<Employee>> Read(int employeeId);
    }
}