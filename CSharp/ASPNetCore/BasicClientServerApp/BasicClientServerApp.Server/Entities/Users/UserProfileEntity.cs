using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicClientServerApp.Server.Entities.Users
{
    public class UserProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime Birthday { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public string Roles { get; set; }
    }
}
