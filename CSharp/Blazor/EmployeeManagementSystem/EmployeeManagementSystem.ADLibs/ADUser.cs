using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ADLibs
{
    public class ADUser
    {
        public DateTime? LastLogon { get; internal set; }
        public DateTime? LastBadPasswordAttempt { get; internal set; }
        public SecurityIdentifier Sid { get; internal set; }
        public int BadLogonCount { get; internal set; }
        public DateTime? AccountLockoutTime { get; internal set; }
        public string Name { get; internal set; }
    }
}
