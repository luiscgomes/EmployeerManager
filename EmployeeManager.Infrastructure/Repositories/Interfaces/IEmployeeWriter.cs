using EmployeeManager.Domain.Entities;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories.Interfaces
{
    public interface IEmployeeWriter
    {
        Task Write(Employee employee);

        Task Remove(Employee employee);
    }
}