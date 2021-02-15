using System.Collections.Generic;
using EntityNS = EmployeeManagementSystem.Data.Shared.Entities;
using ModelNS = EmployeeManagementSystem.Shared.Models;

namespace EmployeeManagementSystem.ReSTapi.Mapping
{
    public class GroupMembershipMapper
    {
        internal EntityNS.GroupMembership GenerateIdOnly(int id)
           => new EntityNS.GroupMembership
           {
               Id = id
           };

        internal EntityNS.GroupMembership GenerateFromEmployeeGroupId(int employee, int group)
        => new EntityNS.GroupMembership
        {
            GroupId = group,
            EmployeeId = employee
        };

        internal ModelNS.GroupMember Map(ModelNS.Employee e, ModelNS.Group g)
            => new ModelNS.GroupMember
            {
                Group = g,
                Employee = e
            };


        internal ModelNS.GroupMembers Map(ModelNS.Group g, IEnumerable<ModelNS.Employee> e)
            => new ModelNS.GroupMembers
            {
                Group = g,
                Employees = e
            };

        internal ModelNS.MemberOfGroups Map(ModelNS.Employee e, IEnumerable<ModelNS.Group> g)
            => new ModelNS.MemberOfGroups
            {
                Groups = g,
                Employee = e
            };
    }
}
