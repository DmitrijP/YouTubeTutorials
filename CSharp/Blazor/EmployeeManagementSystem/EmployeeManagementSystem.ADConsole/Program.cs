using EmployeeManagementSystem.ADLibs;
using EmployeeManagementSystem.Data.SQLiteLayer;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ADConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SelectUsers();            
        }

        public static async Task SelectUsers()
        {
            var userManager = new UserManager("AdminUser", "AdminuserPassword", "Domain");
            var queries = new EmployeeQueries("ConnecitonStringToDB");
            var e = await queries.SelectAllEmployee();
            foreach (var employee in e)
            {
                var name = await queries.SelectName(new Data.Shared.Entities.Name { Id = employee.NameId });
                var login = await queries.SelectLogin(new Data.Shared.Entities.Login { Uuid = employee.LoginUuid});
                userManager.CreateUser("OU=MySubOu,OU=MyMainOu,DC=xyz,DC=local", login.Username, login.Password + login.Salt, name.First, name.Last, employee.Email, employee.Cell);
            }
        }
    }
}
