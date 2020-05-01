namespace MVVMTutorials.WPFui.Models
{
    class EmployeeModel : ModelBase 
    {
        private int _employeeNumber;
        public int EmployeeNumber { get { return _employeeNumber; } set {  _employeeNumber = value; Changed(); } }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int UserName { get; set; }
    }
}
