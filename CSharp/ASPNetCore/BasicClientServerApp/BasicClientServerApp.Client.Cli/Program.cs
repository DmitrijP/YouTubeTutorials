using BasicClientServerApp.Client.Cli.Services;
using System;

namespace BasicClientServerApp.Client.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Client");
            var employeeService = new EmployeeService();
            Console.WriteLine("Press enter to continue");
            var id = Console.ReadLine();
            var result = employeeService.GetEmployee(id);
                if(result.Length > 3)
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(result);
                    Console.WriteLine("Retrying...");
                }

        }
    }
}
