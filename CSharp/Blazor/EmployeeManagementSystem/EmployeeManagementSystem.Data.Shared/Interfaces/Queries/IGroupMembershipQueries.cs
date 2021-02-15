using EmployeeManagementSystem.Data.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces.Queries
{
    public interface IGroupMembershipQueries
    {
        Task<IEnumerable<Employee>> SelectGroupMembers(Group entity);
        Task<IEnumerable<Group>> SelectGroupsOfEmployee(Employee entity);
        Task<GroupMembership> SelectSecurityGroupMembershipByEmployeeAndGroup(GroupMembership entity);
        Task<GroupMembership> SelectSecurityGroupMembership(GroupMembership entity);
    }
}
