using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Shared.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public Country Country { get; set; }
        public Gender Gender { get; set; }
        public Profession Profession { get; set; }
    }
}
