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
            var randoTron = new Random();
            var companyNames = new string[]
            { "CompA", "CompC", "CompB"};
            var firstNames = new string[]
            { "Savannah", "Pearl", "Ava", "Scarlet", "Mary", "Gordon" };
            var lastNames = new string[]
            { "Hunt", "Cook", "Mcdonald", "Hunter", "Hayes", "Freeman", "Williams" };
            for (int i = 0; i < 20; i++)
            {
                var firstName = firstNames[randoTron.Next(0, 5)];
                var lastName = lastNames[randoTron.Next(0, 7)];
                _employeeStore.Add(new EmployeeEntity
                {
                    Birthday = DateTime.Now + TimeSpan.FromDays(randoTron.Next(0, 1000)),
                    Id = i,
                    CompanyName = companyNames[randoTron.Next(0, 2)],
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = $"{lastName}{firstName[0]}"
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
