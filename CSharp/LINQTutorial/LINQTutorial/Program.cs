using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeArray = GenerateEmployeeArray(30);

            var linqResult = from x in employeeArray 
                             where FindUser(x)
                             orderby x.BirthDay descending
                             select x;
            PrintEmployeeArray(linqResult);

            //var result = SearchWithForLoop(employeeArray);
            //PrintEmployeeArray(result);
            
            Console.ReadLine();
        }

        private static bool FindUser(EmployeeModel model)
        {
            return model.LastName == "Patuk";
        }

        private static string GenerateNewCityName()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return Cities[random.Next(0, 5)];
        }

        private static List<EmployeeModel> SearchWithForLoop(EmployeeModel[] employeeArray)
        {
            var result = new List<EmployeeModel>();
            for (int i = 0; i < employeeArray.Length; i++)
                if (employeeArray[i].LastName == "Patuk")
                    result.Add(employeeArray[i]);
            return result;
        }

        static void PrintEmployeeArray(IEnumerable<EmployeeModel> a)
        {
            foreach (var item in a)
                Console.WriteLine(item.ToString());
        }

        static EmployeeModel[] GenerateEmployeeArray(int count)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var arr = new EmployeeModel[count];
            for (int i = 0; i < count; i++)
            {
                arr[i] = new EmployeeModel
                {
                    Id = i,
                    FirstName = FirstNames[random.Next(0, 5)],
                    LastName = LastNames[random.Next(0, 5)],
                    City = Cities[random.Next(0, 5)],
                    BirthDay = DateTime.Now - TimeSpan.FromDays(365 * random.Next(18, 70))
                };
            }
            return arr;
        }

        static string[] FirstNames = new string[] { "Dmitrij", "Denis", "Paul", "Andrej", "Jack", "Luke" };
        static string[] LastNames = new string[] { "Patuk", "Bauer", "Skywalker", "Calrissian", "Muller", "Wolf" };
        static string[] Cities = new string[] { "Dresden", "Berlin", "Heidelberg", "Karlsruhe", "Mannheim", "Bruchsal" };
    }

    class EmployeeModel
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
