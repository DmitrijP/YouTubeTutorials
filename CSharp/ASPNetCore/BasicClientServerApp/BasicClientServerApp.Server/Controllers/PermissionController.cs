using Microsoft.AspNetCore.Mvc;
using BasicClientServerApp.Server.Stores;
using BasicClientServerApp.Server.Mappers;
using BasicClientServerApp.Server.Exceptions;
using BasicClientServerApp.Server.Models.Permission;

namespace BasicClientServerApp.Server.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class PermissionController : ControllerBase
    {
        private readonly EmployeePermissionStore _employeePermissionStore;
        private readonly PermissionMapper _permissionMapper;

        public PermissionController(EmployeePermissionStore employeePermissionStore, PermissionMapper permissionMapper)
        {
            this._permissionMapper = permissionMapper;
            this._employeePermissionStore = employeePermissionStore;
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult All()
        {
            var permissionList = _employeePermissionStore.AllPermissions();
            return Ok(_permissionMapper.Map(permissionList));
        }

        [HttpGet]
        [Route("{action}/{employee:int}")]
        public IActionResult EmployeePermissions(int employee)
        {
            var permissionList = _employeePermissionStore.GetEmployeePermissions(employee);
            return Ok( _permissionMapper.Map(permissionList));
        }

        [HttpDelete]
        [Route("{action}/{employee:int}/{permission:int}")]
        public IActionResult EmployeePermission(int employee, int permission)
        {
            _employeePermissionStore.DeletePermissionForEmployee(permission, employee);
            return Ok();
        }

        [HttpPost]
        [Route("{action}")]
        public IActionResult SetPermission(EmployeePermissionModel model)
        {
            try
            {
                _employeePermissionStore.AddPermissionForEmployee(model.Permission, model.Employee);
                return Ok();
            }
            catch (PermissionDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
