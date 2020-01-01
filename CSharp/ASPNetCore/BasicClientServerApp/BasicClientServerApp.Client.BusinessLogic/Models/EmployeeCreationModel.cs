using System;

namespace BasicClientServerApp.Client.BusinessLogic.Models
{
    public class EmployeeCreationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
