namespace MVVMTutorials.WPFui.Models
{
    class SecondModel : ModelBase
    {
        private int _employeeNumber;
        public int EmployeeNumber { get { return _employeeNumber; } set { _employeeNumber = value; Changed(); } }
    }
}
