namespace EmployeeManagementSystem.Data.Shared.Entities
{

    public class Employee
    {
        public string LoginUuid { get; set; }
        public int? NameId { get; set; }
        public int? PictureId { get; set; }
        public int? LocationId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Gender { get; set; }
        public string Nat { get; set; }
    }
}
