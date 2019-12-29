using System.Linq;
using BasicClientServerApp.Server.Entities.Employee;
using BasicClientServerApp.Server.Models.Employee;
using System.Collections.Generic;

namespace BasicClientServerApp.Server.Mappers
{
    public class EmployeeMapper
    {
        public EmployeeQueryModel Map(EmployeeEntity entity)
        {
            return new EmployeeQueryModel
            {
                Id = entity.Id,
                CompanyName = entity.CompanyName,
                Name = entity.FirstName + " " + entity.LastName,
                UserName = entity.UserName
            };
        }
        public IEnumerable<EmployeeQueryModel> Map(IEnumerable<EmployeeEntity> entityList)
        {
            return from x in entityList select Map(x);
        }

        public EmployeeEntity Map(EmployeeCreationModel model)
        {
            string userName = model.LastName + model.FirstName[0];
            if (string.IsNullOrEmpty(model.FirstName))
            {
                userName = "Nicht Bekannt";
            }
            return new EmployeeEntity
            {
                CompanyName = model.CompanyName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday= model.Birthday,
                UserName = userName
            };
        }

    }
}
