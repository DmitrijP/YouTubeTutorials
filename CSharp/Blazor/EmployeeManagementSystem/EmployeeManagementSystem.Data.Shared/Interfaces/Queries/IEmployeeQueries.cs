using EmployeeManagementSystem.Data.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Queries
{
    public interface IEmployeeQueries
    {
        Task<Employee> SelectEmployee(Employee entity);
        Task<IEnumerable<Employee>> SelectAllEmployee();
    }
}
