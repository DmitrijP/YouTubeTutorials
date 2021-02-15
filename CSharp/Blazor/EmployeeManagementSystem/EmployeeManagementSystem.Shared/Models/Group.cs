using System;
namespace EmployeeManagementSystem.Shared.Models
{
    public class Group
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? LastChange { get; set; }
        public DateTime? LastExport { get; set; }
    }
}
