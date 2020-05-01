using System;
using System.Linq;
using System.Threading.Tasks;
using BasicClientServerApp.Entities;

namespace BasicClientServerApp.DataStores
{
    public class UserStore : IUserStore
    {
        UserProfileEntity[] myUserDataBase = new UserProfileEntity[]
        {
            new UserProfileEntity
            {
                Id = 1,
                Birthday = DateTime.Now,
                City = "Bruchsal",
                CompanyName = "CompanyA",
                FirstName = "Dmitrij",
                LastName = "Patuk",
                Password = "SuperPassword",
                Position = "IT",
                UserName = "PatukD",
                Roles = "UserAdministrator:UserManager:RSXG-BCSApp-Read-Test"
            }  ,
             new UserProfileEntity
            {
                Id = 2,
                Birthday = DateTime.Now,
                City = "Mannheim",
                CompanyName = "CompanyA",
                FirstName = "Dmitrijasdf ",
                LastName = "Patuk asdf ",
                Password = "SuperPasswordasdf",
                Position = "IT",
                UserName = "PatukDasdf"
            },
              new UserProfileEntity
            {
                Id = 3,
                Birthday = DateTime.Now,
                City = "Mannheim",
                CompanyName = "CompanyA",
                FirstName = "Dmitrij -  Test",
                LastName = "Patuk -- Test",
                Password = "SuperPassword",
                Position = "IT",
                UserName = "PatukD1"
            }
        };


        public async Task<UserProfileEntity> FindUserProfile(string userName)
        {
            await Task.Delay(100);
            return myUserDataBase.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
