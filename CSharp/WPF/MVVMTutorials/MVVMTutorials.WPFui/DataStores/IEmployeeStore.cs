using System.Threading.Tasks;
using System.Collections.Generic;
using MVVMTutorials.WPFui.Entities;

namespace MVVMTutorials.WPFui.DataStores
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
