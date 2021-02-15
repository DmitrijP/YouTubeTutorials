using EmployeeManagementSystem.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
        Task CreateEmployee(Employee e);
        Task GenerateADProfile(Employee e);
    }
}
