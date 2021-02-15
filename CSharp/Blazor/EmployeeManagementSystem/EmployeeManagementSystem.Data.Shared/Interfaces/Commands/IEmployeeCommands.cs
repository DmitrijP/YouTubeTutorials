using EmployeeManagementSystem.Data.Shared.Entities;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Commands
{
    public interface IEmployeeCommands
    {
        Task<Employee> InsertEmployee(Employee entity);
        Task UpdateEmployee(Employee entity);
        Task SetExportedDate(Employee entity);
        Task SetDeletedDate(Employee entity);
        Task Delete(Employee entity);
    }
}
