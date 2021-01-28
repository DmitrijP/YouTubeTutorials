using EmployeeManagementSystem.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Bootstrapper.Interfaces
{
    public interface IEmployeeSource
    {
        Task<IEnumerable<Employee>> SelectEmployeeList();
    }
}
