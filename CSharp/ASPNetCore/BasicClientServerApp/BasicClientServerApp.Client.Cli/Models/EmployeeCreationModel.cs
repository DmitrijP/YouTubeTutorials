using System;

namespace BasicClientServerApp.Client.Cli.Models
{
    class EmployeeCreationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
