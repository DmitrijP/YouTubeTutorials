using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagementSystem.Shared.Models
{

    public class Employee
    {
        public Id Id { get; set; }
        public Login Login { get; set; }
        [ValidateComplexType]
        public Name Name { get; set; }
        public Picture Picture { get; set; }
        [Required]
        public string Email { get; set; }
        public Location Location { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public DateStamp Dob { get; set; }
        public DateStamp Registered { get; set; }
        public string Gender { get; set; }
        public string Nat { get; set; }
    }
}
