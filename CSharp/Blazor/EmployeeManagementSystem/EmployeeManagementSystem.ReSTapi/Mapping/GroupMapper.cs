using ModelNS = EmployeeManagementSystem.Shared.Models;
using EntityNS = EmployeeManagementSystem.Data.Shared.Entities;
using System;

namespace EmployeeManagementSystem.ReSTapi.Mapping
{
    public class GroupMapper
    {
        public EntityNS.Group Map(ModelNS.Group e)
            => new EntityNS.Group
            {
                Id = e.Id,
                Name = e.Name,
                Deleted = e.Deleted,
                Description = e.Description,
                LastChange = e.LastChange,
                LastExport = e.LastExport,
            };

        internal ModelNS.Group Map(EntityNS.Group e)
            => new ModelNS.Group
            {
                Id = e.Id,
                Name = e.Name,
                Deleted = e.Deleted,
                Description = e.Description,
                LastChange = e.LastChange,
                LastExport = e.LastExport,
            };

        internal EntityNS.Group GenerateIdOnly(int id)
           => new EntityNS.Group
           {
               Id = id
           };

        internal EntityNS.Group GenerateWithExportDate(int id)
           => new EntityNS.Group
           {
               Id = id,
               LastExport = DateTime.Now
           };
    }
}
