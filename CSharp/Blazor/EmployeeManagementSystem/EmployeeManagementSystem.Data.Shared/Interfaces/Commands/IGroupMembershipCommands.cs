using EmployeeManagementSystem.Data.Shared.Entities;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Commands
{
    public interface IGroupMembershipCommands
    {
        Task InsertMembership(GroupMembership membership);
        Task MarkMembershipForDeletion(GroupMembership membership);
        Task DeleteMembership(GroupMembership membership);
        Task SetExportedDate(GroupMembership entity);
    }
}
