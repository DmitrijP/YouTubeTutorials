using EmployeeManagementSystem.Data.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Queries
{
    public interface IGroupQueries
    {
        Task<Group> SelectGroup(Group entity);
        Task<IEnumerable<Group>> SelectAllGroups();
    }
}
