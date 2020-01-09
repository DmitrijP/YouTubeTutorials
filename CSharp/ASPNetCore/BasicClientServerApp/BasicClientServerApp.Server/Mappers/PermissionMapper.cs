using System.Linq;
using System.Collections.Generic;
using BasicClientServerApp.Server.Models.Permission;
using BasicClientServerApp.Server.Entities.Permission;

namespace BasicClientServerApp.Server.Mappers
{
    public class PermissionMapper
    {
        public PermissionModel Map(PermissionEntity e) 
            => new PermissionModel { Id = e.Id, Name = e.Name };

        public IEnumerable<PermissionModel> Map(IEnumerable<PermissionEntity> e)
            => from x in e select new PermissionModel { Id = x.Id, Name = x.Name };
    }
}
