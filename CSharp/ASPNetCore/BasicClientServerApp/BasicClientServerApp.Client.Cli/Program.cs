using System;
using System.Text;
using System.Threading.Tasks;
using BasicClientServerApp.Client.BusinessLogic.Models;
using BasicClientServerApp.Client.BusinessLogic.Services;

namespace BasicClientServerApp.Client.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync()
                .GetAwaiter()
                .GetResult();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Starting Client");
            string baseUri = "https://localhost:44308";
            var employeeService = new EmployeeService(baseUri);
            Console.WriteLine("Press enter Command <id, delete, name, all, create, q> to continue");
            var command = Console.ReadLine();
            while (true)
            {
                await ProcessCommandAsync(employeeService, command);
                command = Console.ReadLine();
            }
        }

        private static async Task ProcessCommandAsync(EmployeeService employeeService, string command)
        {
            string result = string.Empty;
            if (command == "id")
            {
                Console.WriteLine("Press enter id to continue");
                var c = Console.ReadLine();
                result = await employeeService.GetEmployeeByIdAsync(int.Parse(c));
            }
            if (command == "delete")
            {
                Console.WriteLine("Press enter id to continue");
                var c = Console.ReadLine();
                result = await employeeService.DeleteEmployeeAsync(int.Parse(c));
            }
            if (command == "name")
            {
                Console.WriteLine("Press enter name to continue");
                var c = Console.ReadLine();
                result = await employeeService.GetEmployeeByNameAsync(c);
            }
            if (command == "all")
            {
                //result = await employeeService.GetAllEmployeeAsync();
            }
            if (command == "create")
            {
                var model = new EmployeeCreationModel();
                Console.WriteLine("Enter First:");
                var firstName = Console.ReadLine();
                model.FirstName = firstName;
                Console.WriteLine("Enter LastName:");
                var lastName = Console.ReadLine();
                model.LastName = lastName;
                Console.WriteLine("Enter CompanyName:");
                var companyName = Console.ReadLine();
                model.CompanyName = companyName;
                Console.WriteLine("Enter BirthDay:");
                var birthDay = Console.ReadLine();
                model.Birthday = DateTime.Parse(birthDay);

                result = await employeeService.CreateEmployeeAsync(model);
            }
            if (command == "q")
            {
                return;
            }
            Console.WriteLine(result);
        }
    }
}
