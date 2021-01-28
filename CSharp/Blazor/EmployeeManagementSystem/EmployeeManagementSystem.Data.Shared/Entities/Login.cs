namespace EmployeeManagementSystem.Data.Shared.Entities
{
    public class Login
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string SHA256 { get; set; }
    }
}
