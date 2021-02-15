using EmployeeManagementSystem.Data.SQLiteLayer.Commands;
using EmployeeManagementSystem.Data.SQLiteLayer.Creation;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Bootstrapper.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
            { 
                Console.WriteLine("Please supply the connection string as the only argument. Press enter to quit");
                Console.ReadKey();
                return;
            }
            var connectionString = args[0];
            Console.WriteLine($"Initializing Database!  {connectionString}");
            var source = new EmployeeSource(new System.Net.Http.HttpClient() { BaseAddress = new Uri("https://randomuser.me") });
            var tb = new TableBuilder(connectionString);
            var ec = new EmployeeCommands(connectionString);
            var bootstrapper = new DBBootstrapper(tb, ec, source);
            await bootstrapper.Initialize();
            Console.WriteLine($"Initialized Database!  {connectionString}");
        }
    }
}
