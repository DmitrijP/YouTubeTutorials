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
    public class ActiveDirectoryGroupController : ControllerBase
    {
        private readonly IGroupManager _groupManager;
        private readonly IGroupCommands _groupCommands;
        private readonly IGroupQueries _groupQueries;
        private readonly GroupMapper _gMapper;

        public ActiveDirectoryGroupController(IGroupManager groupManager, IGroupCommands groupCommands, IGroupQueries groupQueries,
            GroupMapper gMapper)
        {
            _groupManager = groupManager;
            _groupCommands = groupCommands;
            _groupQueries = groupQueries;
            _gMapper = gMapper;
        }

        [HttpGet]
        [Route("get-details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var u = await _groupQueries.SelectGroup(_gMapper.GenerateIdOnly(id));
            var e = _groupManager.GetDetails(u.Name);
            return Ok(e);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(int id)
        {
            var u = await _groupQueries.SelectGroup(_gMapper.GenerateIdOnly(id));
            _groupManager.Create(u.Name, u.Description);
            await _groupCommands.SetExportedDate(_gMapper.GenerateWithExportDate(id));
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _groupQueries.SelectGroup(_gMapper.GenerateIdOnly(id));
            _groupManager.Delete(u.Name);
            await _groupCommands.Delete(_gMapper.GenerateIdOnly(id));
            return Ok();
        }
    }
}
