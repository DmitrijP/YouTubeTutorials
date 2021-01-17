using EmployeeManagementSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.Shared
{
    public class EmployeeFactory
    {
        public void InitializeEmployeeList()
        {
            CountryList = new List<Country>
            {
                new Country{CountryId = 1, Name = "Germany"},
                new Country{CountryId = 2, Name = "Russia"},
                new Country{CountryId = 3, Name = "USA"},
                new Country{CountryId = 4, Name = "Cyprus"},
            };
            ProfessionList = new List<Profession>
            {
                new Profession{ ProfessionId = 1, Name = "Pie research"},
                new Profession{ ProfessionId = 2, Name = "Software Developer"},
                new Profession{ ProfessionId = 3, Name = "Accountant"},
                new Profession{ ProfessionId = 4, Name = "CIO"},
                new Profession{ ProfessionId = 5, Name = "CEO"},
                new Profession{ ProfessionId = 6, Name = "CTO"}
            };

            EmployeeList = new List<Employee> {
               new Employee {
                   EmployeeId = 1,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   Gender = Gender.Male,
                   JoinDate = GetRandomDate(),
                   FirstName = "Dmitrij",
                   LastName = "Patuk"
               },
               new Employee {
                   EmployeeId = 2,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   Gender = Gender.Other,
                   JoinDate = GetRandomDate(),
                   ExitDate = GetRandomDate(),
                   FirstName = "Joe",
                   LastName = "Pdsakjhfd" },
               new Employee {
                   EmployeeId = 3,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   Gender = Gender.Male,
                   JoinDate = GetRandomDate(),
                   FirstName = "Bill",
                   LastName = "Dsalkdfj" },
               new Employee {
                   EmployeeId = 4,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   ExitDate = GetRandomDate(),
                   Gender = Gender.Female,
                   JoinDate = GetRandomDate(),
                   FirstName = "Samantha",
                   LastName = "Guadnas" },
               new Employee {
                   EmployeeId = 5,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   Gender = Gender.Female,
                   JoinDate = GetRandomDate(),
                   FirstName = "Laura", LastName = "Glraods" },
               new Employee {
                   EmployeeId = 6,
                   Country = GetRandomCountry(),
                   Birthday = GetRandomDate(),
                   Profession = GetRandomJobCategory(),
                   Gender = Gender.Female,
                   JoinDate = GetRandomDate(),
                   FirstName = "Sina",
                   LastName = "Klasdlkf" },
            };
        }


        public Random GetRandomSeed
            => new Random(DateTime.Now.Millisecond);

        public List<Country> CountryList { get; private set; }
        public List<Profession> ProfessionList { get; private set; }
        public List<Employee> EmployeeList { get; private set; }

        private Country GetRandomCountry()
            => CountryList[GetRandomSeed.Next(0, CountryList.Count - 1)];
        private Profession GetRandomJobCategory()
            => ProfessionList[GetRandomSeed.Next(0, ProfessionList.Count - 1)];
        private DateTime GetRandomDate()
            => DateTime.Now - TimeSpan.FromDays(GetRandomSeed.Next(18, 60) * 360);
        private Gender GetRandomGender()
            => (Gender)GetRandomSeed.Next(1, 3);
    }
}
