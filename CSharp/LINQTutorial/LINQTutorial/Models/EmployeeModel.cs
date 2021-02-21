using System;

namespace LINQTutorial
{
    internal class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime BirthDay { get; set; }
        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {City} {BirthDay}";
        }
    }
}
