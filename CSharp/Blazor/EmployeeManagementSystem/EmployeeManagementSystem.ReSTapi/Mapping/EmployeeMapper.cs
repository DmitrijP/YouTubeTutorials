using ModelNS = EmployeeManagementSystem.Shared.Models;
using EntityNS = EmployeeManagementSystem.Data.Shared.Entities;
using System;

namespace EmployeeManagementSystem.ReSTapi.Mapping
{
    public class EmployeeMapper
    {
        public EntityNS.Employee Map(ModelNS.Employee e)
            => new EntityNS.Employee
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastChange = e.LastChange,
                LastExport = e.LastExport,
                LastName = e.LastName,
                Title = e.Title,
                Username = e.Username,
                Phone = e.Phone,
                TemporaryPassword = e.TemporaryPassword
            };

        internal ModelNS.Employee Map(EntityNS.Employee e)
            => new ModelNS.Employee
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastChange = e.LastChange,
                LastExport = e.LastExport,
                LastName = e.LastName,
                Title = e.Title,
                Username = e.Username,
                Phone = e.Phone,
                TemporaryPassword = e.TemporaryPassword
            };

        internal EntityNS.Employee GenerateIdOnly(int id)
            => new EntityNS.Employee
            {
                Id = id
            };

        internal EntityNS.Employee GenerateWithExportDate(int id)
            => new EntityNS.Employee
            {
                Id = id,
                LastExport = DateTime.Now
            };
    }
}
