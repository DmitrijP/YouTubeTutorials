using EmployeeManagementSystem.Data.Bootstrapper.Interfaces;
using EmployeeManagementSystem.Data.Shared.Interfaces;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Bootstrapper
{
    public class DBBootstrapper : IDBBootstrapper
    {
        private readonly IDBTableBuilder tableBuilder;
        private readonly IEmployeeCommands commands;
        private readonly IEmployeeSource source;

        public DBBootstrapper(IDBTableBuilder factory, IEmployeeCommands commands, IEmployeeSource source)
        {
            this.tableBuilder = factory;
            this.commands = commands;
            this.source = source;
        }

        public async Task Initialize()
        {
            await tableBuilder.CreateTables();
            var sourceList = await source.SelectEmployeeList();
            foreach (var sourceEmployee in sourceList)
            {
                var street = await commands.InsertStreet(new Data.Shared.Entities.Street
                {
                    Name = sourceEmployee.Location.Street.Name,
                    Number = sourceEmployee.Location.Street.Number
                });
                var location = await commands.InsertLocation(new Shared.Entities.Location
                {
                    StreetId = street?.Id,
                    State = sourceEmployee.Location.State,
                    City = sourceEmployee.Location.City,
                    Country = sourceEmployee.Location.Country,
                    Postcode = sourceEmployee.Location.Postcode
                });
                var name = await commands.InsertName(new Shared.Entities.Name
                {
                    First = sourceEmployee.Name.First,
                    Last = sourceEmployee.Name.Last,
                    Title = sourceEmployee.Name.Title
                });
                var login = await commands.InsertLogin(new Shared.Entities.Login
                {
                    Salt = sourceEmployee.Login.Salt,
                    SHA256 = sourceEmployee.Login.SHA256,
                    Password = sourceEmployee.Login.Password,
                    Username = sourceEmployee.Login.Username,
                    Uuid = sourceEmployee.Login.Uuid
                });
                var picture = await commands.InsertPicture(new Shared.Entities.Picture
                {
                    Large = sourceEmployee.Picture.Large,
                    Medium = sourceEmployee.Picture.Medium,
                    Thumbnail = sourceEmployee.Picture.Thumbnail
                });
                var employee = await commands.InsertEmployee(new Shared.Entities.Employee
                {
                    Cell = sourceEmployee.Cell,
                    Email = sourceEmployee.Email,
                    Gender = sourceEmployee.Gender,
                    Nat = sourceEmployee.Nat,
                    Phone = sourceEmployee.Phone,
                    LocationId = location?.Id,
                    NameId = name?.Id,
                    PictureId = picture?.Id,
                    LoginUuid = login?.Uuid,
                });

            }
        }
    }
}
