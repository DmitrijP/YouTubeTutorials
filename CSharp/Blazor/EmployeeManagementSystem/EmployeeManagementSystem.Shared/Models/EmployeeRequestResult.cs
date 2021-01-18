using System.Collections.Generic;

namespace EmployeeManagementSystem.Shared.Models
{
    public class EmployeeRequestResult
    {
        public IEnumerable<Employee> Results { get; set; }
        public Info Info { get; set; }
    }
}
