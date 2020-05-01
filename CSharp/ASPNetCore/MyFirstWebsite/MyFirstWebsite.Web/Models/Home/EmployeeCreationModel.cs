using System;

namespace MyFirstWebsite.Web.Models.Home
{
    public class EmployeeCreationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime Birthday { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
    }
}
