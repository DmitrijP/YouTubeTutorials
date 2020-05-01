using System.Collections.Generic;
using MyFirstWebsite.Web.Entitties;

namespace MyFirstWebsite.Web.Models.Home
{
    public class IndexModel
    {
        public string HelloWorld { get; set; }
        public IEnumerable<EmployeeEntity> EmployeeList { get; set; }
    }
}
