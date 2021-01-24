using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Shared.Models
{
    public class Name
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "First name too long (16 character limit).")]
        public string First { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Last name too long (16 character limit).")]
        public string Last { get; set; }
    }
}
