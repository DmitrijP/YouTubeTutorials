using EmployeeManagementSystem.Data.Shared.Entities;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces
{
    public interface IEmployeeCommands
    {
        Task<Employee> InsertEmployee(Employee entity);
        Task<Login> InsertLogin(Login entity);
        Task<Picture> InsertPicture(Picture entity);
        Task<Name> InsertName(Name entity);
        Task<Location> InsertLocation(Location entity);
        Task<Street> InsertStreet(Street entity);
    }
}
