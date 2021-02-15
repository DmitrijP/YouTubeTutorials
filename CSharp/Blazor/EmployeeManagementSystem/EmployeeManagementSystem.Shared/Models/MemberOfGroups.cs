using System.Collections.Generic;

namespace EmployeeManagementSystem.Shared.Models
{
    public class MemberOfGroups
    {
        public Employee Employee { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
