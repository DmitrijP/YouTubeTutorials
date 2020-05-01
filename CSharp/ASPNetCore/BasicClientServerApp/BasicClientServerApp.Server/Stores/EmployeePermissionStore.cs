using System;
using System.Linq;
using System.Collections.Generic;
using BasicClientServerApp.Server.Exceptions;
using BasicClientServerApp.Server.Entities.Permission;

namespace BasicClientServerApp.Server.Stores
{
    public class EmployeePermissionStore
    {

        List<EmployeePermissionEntity> _employeePermissionTable;
        List<PermissionEntity> _permissionTable;

        public EmployeePermissionStore()
        {
            _permissionTable = new List<PermissionEntity>
           {
               new PermissionEntity
               {
                   Id = 1,
                   Name = "User"
               },
               new PermissionEntity
               {
                   Id = 2,
                   Name = "Advanced User"
               },
               new PermissionEntity
               {
                   Id = 3,
                   Name = "Administrator"
               },
               new PermissionEntity
               {
                   Id = 4,
                   Name = "Super Administrator"
               }
           };
            var randoTron = new Random((int)DateTime.Now.Ticks / DateTime.Now.Millisecond);

            foreach (var employee in new EmployeeStore().GetAll())
            {
                //var randomPermission = _permissionTable[randoTron.Next(0, _permissionTable.Count - 1)].Id;
                //_employeePermissionTable.Add(
                //    new EmployeePermissionEntity
                //    {
                //        EmployeeId = employee.Id,
                //        PermissionId = randomPermission
                //    }
                //    );
            }
        }

        public PermissionEntity GetPermission(int id)
        {
            return _permissionTable.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PermissionEntity> GetEmployeePermissions(int employee)
        {
            var employeePermissions =  _employeePermissionTable.Where(x => x.EmployeeId == employee);
            return _permissionTable.Where(x => employeePermissions.Any(y => y.PermissionId == x.Id));
        }

        public IEnumerable<PermissionEntity> AllPermissions()
        {
            return _permissionTable;
        }

        public void DeletePermissionForEmployee(int permission, int employee)
        {
            _employeePermissionTable.RemoveAll(x =>  x.EmployeeId == employee && x.PermissionId == permission);
        }

        public void AddPermissionForEmployee(int permission, int employee)
        {
            if (!_permissionTable.Any(x => x.Id == permission))
                throw new PermissionDoesNotExistException($"Permission with id {permission} not found!");
            DeletePermissionForEmployee(permission, employee);
            _employeePermissionTable.Add(new EmployeePermissionEntity { EmployeeId = employee, PermissionId = permission });
        }

    }
}
