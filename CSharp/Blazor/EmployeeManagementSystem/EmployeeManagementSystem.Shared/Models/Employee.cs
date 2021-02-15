using System;
namespace EmployeeManagementSystem.Shared.Models
{

    public class Employee
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string TemporaryPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastChange { get; set; }
        public DateTime? LastExport { get; set; }
    }
}
