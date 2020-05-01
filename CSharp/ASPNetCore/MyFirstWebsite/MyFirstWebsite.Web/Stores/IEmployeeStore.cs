using System.Collections.Generic;
using System.Threading.Tasks;
using MyFirstWebsite.Web.Entitties;

namespace MyFirstWebsite.Web.Stores
{
    public interface IEmployeeStore
    {
        Task<bool> Delete(int id);
        Task<EmployeeEntity> Find(int id);
        Task<IEnumerable<EmployeeEntity>> GetAll();
        Task<IEnumerable<EmployeeEntity>> Find(string userName);
        Task<EmployeeEntity> Insert(EmployeeEntity employeeEntity);
    }
}
