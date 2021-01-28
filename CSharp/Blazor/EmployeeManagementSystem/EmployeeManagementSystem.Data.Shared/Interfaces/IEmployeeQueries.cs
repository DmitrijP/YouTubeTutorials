using EmployeeManagementSystem.Data.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces
{
    public interface IEmployeeQueries
    {
        Task<Employee> SelectEmployee(Employee entity);
        Task<Login> SelectLogin(Login entity);
        Task<Picture> SelectPicture(Picture entity);
        Task<Name> SelectName(Name entity);
        Task<Location> SelectLocation(Location entity);
        Task<Street> SelectStreet(Street entity);
        Task<IEnumerable<Employee>> SelectAllEmployee();
        Task<IEnumerable<Login>> SelectAllLogin();
        Task<IEnumerable<Name>> SelectAllName();
    }
}
