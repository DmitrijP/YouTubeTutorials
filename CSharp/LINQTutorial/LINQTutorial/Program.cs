using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTutorial
{
    class Program
    {
        static readonly string[] FirstNames 
            = new string[] { "Dmitrij", "Denis", "Paul", "Andrej", "Jack", "Luke" };
        static readonly string[] LastNames 
            = new string[] { "Patuk", "Bauer", "Skywalker", "Calrissian", "Muller", "Wolf" };
        static readonly string[] Cities 
            = new string[] { "Dresden", "Berlin", "Heidelberg", "Karlsruhe", "Mannheim", "Bruchsal" };

        static void Main(string[] args)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var idEnumerable = Enumerable.Range(1, 10000000);
            var employeeArray = from x in idEnumerable
                                select GenerateEmployee(x, random);
            var result = employeeArray.ToList();
        }

        private static EmployeeModel GenerateEmployee(int id, Random random)
        {
            return new EmployeeModel
            {
                Id = id,
                FirstName = FirstNames[random.Next(0, 5)],
                LastName = LastNames[random.Next(0, 5)],
                City = Cities[random.Next(0, 5)],
                BirthDay = DateTime.Now - TimeSpan.FromDays(365 * random.Next(18, 70))
            };
        }

        private static IEnumerable<EmployeeModel> SearchWithYield(IEnumerable<EmployeeModel> inputList)
        {
            foreach (var item in inputList)
                if (FindUser(item))
                    yield return item;
                
        }

        private static void PrintEmployeeArray(IEnumerable<EmployeeModel> a)
        {
            foreach (var item in a)
              Console.WriteLine(item.ToString());
        }

        private static bool FindUser(EmployeeModel model)
        {
            return model.LastName == "Patuk";
        }
    }
}
