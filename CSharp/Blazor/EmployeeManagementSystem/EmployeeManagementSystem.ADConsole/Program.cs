using EmployeeManagementSystem.ADLibs;
using EmployeeManagementSystem.Data.SQLiteLayer;
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
            var userManager = new UserManager(Credentials.USR, Credentials.PWD, Credentials.Domain);
            var queries = new EmployeeQueries(Credentials.ConnectionString);
            var e = await queries.SelectAllEmployee();
            foreach (var employee in e)
            {
                var name = await queries.SelectName(new Data.Shared.Entities.Name { Id = employee.NameId });
                var login = await queries.SelectLogin(new Data.Shared.Entities.Login { Uuid = employee.LoginUuid});
                userManager.CreateUser(UsersPath, login.Username, login.Password + login.Salt, name.First, name.Last, employee.Email, employee.Cell);
            }
        }

        public static async Task GetUser()
        {
            try
            {
                var userManager = new UserManager(Credentials.USR, Credentials.PWD, Credentials.Domain);
                var user = userManager.GetUser(BasePath, "bluedog411");
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
                var userManager = new UserManager(Credentials.USR, Credentials.PWD, Credentials.Domain);
                userManager.RemoveUserFromGroup(BasePath, "bluedog411", "Employee");
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
