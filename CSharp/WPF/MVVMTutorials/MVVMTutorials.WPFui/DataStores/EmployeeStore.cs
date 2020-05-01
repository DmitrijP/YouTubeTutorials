using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVVMTutorials.WPFui.Entities;

namespace MVVMTutorials.WPFui.DataStores
{
    public class EmployeeStore : IEmployeeStore
    {
        private IList<EmployeeEntity> _employeeStore;
        public EmployeeStore()
        {
            _employeeStore = new List<EmployeeEntity>();
            var randoTron = new Random((int)DateTime.Now.Ticks / DateTime.Now.Millisecond);
            var companyNames = new string[]
            { "CompA", "CompB","CompC","CompD"};
            var firstNames = new string[]
            { "Savannah", "Pearl", "Ava", "Scarlet", "Mary", "Gordon", "Donald", "Vader" };
            var lastNames = new string[]
            { "Hunt", "Cook", "Mcdonald", "Hunter", "Hayes", "Freeman", "Williams", "Top" };
            var positions = new string[]
            { "Agent", "TeamLeader", "Leader", "LocationLeader", "Developer"};
            var cities = new string[]
            { "Mannheim", "Karlsruhe", "Frankfurt", "Berlin", "Köln"};
            var permissions = new PermissionEntity[]
            {
                new PermissionEntity { Id = 0, Name = "HiddenStuff", Description = "HiddenStuff user permission"},
                new PermissionEntity { Id = 1, Name = "Read", Description = "Read user permission"},
                new PermissionEntity { Id = 2, Name = "Write", Description = "Write user permission"},
                new PermissionEntity { Id = 3, Name = "Administration", Description = "Administrator user permission"},
                new PermissionEntity { Id = 4, Name = "Developer", Description = "Developer user permission"}
            };
            for (int i = 0; i < 20; i++)
            {
                var firstName = firstNames[randoTron.Next(0, 7)];
                var lastName = lastNames[randoTron.Next(0, 8)];
                _employeeStore.Add(new EmployeeEntity
                {
                    Birthday = DateTime.Now + TimeSpan.FromDays(randoTron.Next(0, 1000)),
                    Id = i,
                    CompanyName = companyNames[randoTron.Next(0, 3)],
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = $"{lastName}{firstName[0]}",
                    City = cities[randoTron.Next(0, 5)],
                    Position = positions[randoTron.Next(0, 5)],
                    Permissions = new [] { 
                        permissions[randoTron.Next(0, 4)], 
                        permissions[randoTron.Next(0, 4)], 
                        permissions[randoTron.Next(0, 4)] }                    
                });
            }
            _employeeStore.Add(new EmployeeEntity
            {
                UserName = "PatukD",
                CompanyName = "tectiqom",
                Birthday = DateTime.Now,
                City = "Mannheim",
                Position = "SW Engineer",
                Id = 21,
                LastName = "Patuk",
                FirstName = "Dmitrij"
            });
        }

        public async Task<EmployeeEntity> Insert(EmployeeEntity employeeEntity)
        {
            var lastId = _employeeStore.Max(x => x.Id) + 1;
            employeeEntity.Id = lastId;
            employeeEntity.UserName = employeeEntity.LastName + employeeEntity.FirstName[0];
            await Task.Delay(100);
            _employeeStore.Add(employeeEntity);
            return _employeeStore.Where(x => x.Id == lastId).FirstOrDefault();
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAll()
        {
            await Task.Delay(100);
            return _employeeStore;
        }

        public async Task<bool> Delete(int id)
        {
            await Task.Delay(100);
            EmployeeEntity entity = _employeeStore.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return false;
            else
                _employeeStore.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<EmployeeEntity>> Find(string userName)
        {
            if (userName == null)
                return new EmployeeEntity[0];
            await Task.Delay(100);
            return _employeeStore.Where(x => x.UserName.Contains(userName));
        }

        public async Task<EmployeeEntity> Find(int id)
        {
            await Task.Delay(100);
            return _employeeStore.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
