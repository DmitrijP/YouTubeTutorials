using System;
using System.Linq;
using System.Collections.Generic;
using BasicClientServerApp.Server.Entities.Employee;

namespace BasicClientServerApp.Server.Stores
{
    public class EmployeeStore
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
                    Position = positions[randoTron.Next(0, 5)]

                });
            }
        }

        public EmployeeEntity Insert(EmployeeEntity employeeEntity)
        {
            var lastId = _employeeStore.Max(x => x.Id) + 1;
            employeeEntity.Id = lastId;
            _employeeStore.Add(employeeEntity);
            return _employeeStore.Where(x => x.Id == lastId).FirstOrDefault();
        }
        public IEnumerable<EmployeeEntity> GetAll()
        {
            return _employeeStore;
        }

        public bool Delete(int id)
        {
            EmployeeEntity entity = _employeeStore.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return false;
            else
                _employeeStore.Remove(entity);
            return true;
        }

        public IEnumerable<EmployeeEntity> Find(string userName)
        {
            return _employeeStore.Where(x => x.UserName.Contains(userName));
        }
        public EmployeeEntity Find(int id)
        {
            return _employeeStore.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
