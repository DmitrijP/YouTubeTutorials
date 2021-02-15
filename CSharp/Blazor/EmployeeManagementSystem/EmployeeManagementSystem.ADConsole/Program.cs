using EmployeeManagementSystem.ADLibs;
using EmployeeManagementSystem.Data.SQLiteLayer.Queries;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ADConsole
{
    class Program
    {
        private static string BasePath = "OU=YoutubeTutorials,DC=patuk,DC=local";
        private static string UsersPath = "OU=MyCompanyUsers," + BasePath;
        private static string GroupsPath = "OU=MyCompanyGroups," + BasePath;

        static async Task Main(string[] args)
        {
            await GetUser();            
        }

        public static async Task SelectUsers()
        {
            var userManager = new UserManager(Credentials.USR, Credentials.PWD, Credentials.Domain, UsersPath);
            var queries = new EmployeeQueries(Credentials.ConnectionString);
            var employeeList = await queries.SelectAllEmployee();
            foreach (var e in employeeList)
            {
                userManager.Create(e.Username, e.TemporaryPassword, e.FirstName, e.LastName, e.Email, e.Phone);
            }
        }

        public static async Task GetUser()
        {
            try
            {
                var userManager = new UserManager(Credentials.USR, Credentials.PWD, Credentials.Domain, BasePath);
                var user = userManager.GetDetails("bluedog411");
                int i = 1;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public static async Task AddUserToGroup()
        {
            try
            {
                var userManager = new GroupManager(Credentials.USR, Credentials.PWD, Credentials.Domain, BasePath, BasePath);
                userManager.RemoveFromGroup("bluedog411", "Employee");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
