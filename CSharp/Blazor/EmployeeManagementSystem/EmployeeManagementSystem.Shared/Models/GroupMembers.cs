using System.Collections.Generic;

namespace EmployeeManagementSystem.Shared.Models
{
    public class GroupMembers
    {
        public Group Group { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
