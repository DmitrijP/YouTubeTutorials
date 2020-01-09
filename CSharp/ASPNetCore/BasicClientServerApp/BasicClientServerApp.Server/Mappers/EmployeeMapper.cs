using System.Linq;
using System.Collections.Generic;
using BasicClientServerApp.Server.Models.Employee;
using BasicClientServerApp.Server.Entities.Employee;

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
                FirstName = entity.FirstName ,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Birthday = entity.Birthday,
                City = entity.City,
                Position = entity.Position
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
                UserName = userName,
                City =   model.City,
                Position = model.Position
            };
        }

    }
}
