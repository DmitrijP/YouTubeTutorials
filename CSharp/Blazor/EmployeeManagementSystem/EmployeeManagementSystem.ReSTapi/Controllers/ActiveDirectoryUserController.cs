using EmployeeManagementSystem.ADLibs.Interfaces;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using EmployeeManagementSystem.ReSTapi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ReSTapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActiveDirectoryUserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IEmployeeQueries _employeeQueries;
        private readonly IEmployeeCommands _employeeCommands;
        private readonly EmployeeMapper _eMapper;

        public ActiveDirectoryUserController(IUserManager userManager, IEmployeeQueries employeeQueries, IEmployeeCommands employeeCommands,  EmployeeMapper eMapper)
        {
            _userManager = userManager;
            _employeeQueries = employeeQueries;
            _employeeCommands = employeeCommands;
            _eMapper = eMapper;
        }

        [HttpGet]
        [Route("get-details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.Create(u.Username, u.TemporaryPassword, u.FirstName, u.LastName, u.Email, u.Phone);
            await _employeeCommands.SetExportedDate(_eMapper.GenerateWithExportDate(id));
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.Delete(u.Username);
            await _employeeCommands.Delete(_eMapper.GenerateIdOnly(id));
            return Ok();
        }

        [HttpPost]
        [Route("disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.Disable(u.Username);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("expire-password")]
        public async Task<IActionResult> ExpirePassword(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.ExpirePassword(u.Username);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("refresh-expired-password")]
        public async Task<IActionResult> RefreshPassword(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.RefreshExpiredPassword(u.Username);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.Enable(u.Username);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("unlock")]
        public async Task<IActionResult> Unlock(int id)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.Unlock(u.Username);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            var u = await _employeeQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            _userManager.ChangePassword(u.Username, password);
            u.TemporaryPassword = password;
            await _employeeCommands.UpdateEmployee(u);
            var e = _userManager.GetDetails(u.Username);
            return Ok(e);
        }
    }
}
