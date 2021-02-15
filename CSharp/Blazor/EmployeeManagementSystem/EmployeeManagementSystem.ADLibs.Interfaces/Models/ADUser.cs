using System;
namespace EmployeeManagementSystem.ADLibs.Interfaces.Models
{
    public class ADUser
    {
        public DateTime? LastLogon { get; set; }
        public DateTime? LastBadPasswordAttempt { get; set; }
        public string Sid { get; set; }
        public int BadLogonCount { get; set; }
        public DateTime? AccountLockoutTime { get; set; }
        public string Name { get; set; }
        public bool? Enabled { get; set; }
    }
}
