using EmployeeManagementSystem.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> Get(int id);
        Task<Employee> Update(Employee e);
        Task<Employee> Create(Employee e);
        Task Delete(int id);
    }
}
