using EmployeeManagementSystem.Data.Shared.Entities;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Commands
{
    public interface IGroupCommands
    {
        Task<Group> InsertGroup(Group entity);
        Task UpdateGroup(Group entity);
        Task SetExportedDate(Group entity);
        Task SetDeletedDate(Group entity);
        Task Delete(Group entity);
    }
}
