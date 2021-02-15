namespace EmployeeManagementSystem.ADLibs.Interfaces.Models
{
    public class ADGroup
    {
        public string Sid { get; set; }
        public string Name { get; set; }
        public string Descripion { get; set; }
        public bool? IsSecurityGroup { get; set; }
        public string GroupScope { get; set; }
    }
}
