using System;

namespace EmployeeManagementSystem.Data.Shared.Entities
{
    public class GroupMembership
    {
        public int? Id { get; set; }
        public int? GroupId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? LastExport { get; set; }
    }
}
