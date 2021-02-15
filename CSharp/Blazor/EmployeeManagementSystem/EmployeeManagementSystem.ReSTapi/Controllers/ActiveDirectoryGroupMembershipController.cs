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
    public class ActiveDirectoryGroupMembershipController : ControllerBase
    {
        private readonly IGroupManager _groupManager;
        private readonly IGroupQueries _gQueries;
        private readonly IEmployeeQueries _eQueries;
        private readonly IGroupMembershipQueries _gmQueries;
        private readonly IGroupMembershipCommands _gmCommands;
        private readonly GroupMembershipMapper _gmMapper;
        private readonly EmployeeMapper _eMapper;
        private readonly GroupMapper _gMapper;

        public ActiveDirectoryGroupMembershipController(
            IGroupManager groupManager,
            IGroupQueries gQueries, IEmployeeQueries eQueries,
            IGroupMembershipQueries gmQueries, IGroupMembershipCommands gmCommands, 
            GroupMembershipMapper gmMapper, EmployeeMapper eMapper, GroupMapper gMapper)
        {
            _groupManager = groupManager;
            _gQueries = gQueries;
            _eQueries = eQueries;
            _gmQueries = gmQueries;
            _gmCommands = gmCommands;
            _gmMapper = gmMapper;
            _eMapper = eMapper;
            _gMapper = gMapper;
        }
        
        [HttpPost]
        [Route("add-member")]
        public async Task<IActionResult> CreateMembership(int group, int employee)
        {
            var gm = await _gmQueries.SelectSecurityGroupMembershipByEmployeeAndGroup(_gmMapper.GenerateFromEmployeeGroupId(employee, group));
            if (gm.LastExport.HasValue)
            {
                return Ok();
            }
            else
            {
                var e = await _eQueries.SelectEmployee(_eMapper.GenerateIdOnly(employee));
                var g = await _gQueries.SelectGroup(_gMapper.GenerateIdOnly(group));
                _groupManager.AddToGroup(e.Username, g.Name);
                await _gmCommands.SetExportedDate(gm);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("remove-member")]
        public async Task<IActionResult> DeleteMembership(int group, int employee)
        {
            var gm = await _gmQueries.SelectSecurityGroupMembershipByEmployeeAndGroup(_gmMapper.GenerateFromEmployeeGroupId(employee, group));
            if (gm.LastExport.HasValue)
            {
                return Ok();
            }
            else
            {
                var e = await _eQueries.SelectEmployee(_eMapper.GenerateIdOnly(employee));
                var g = await _gQueries.SelectGroup(_gMapper.GenerateIdOnly(group));
                _groupManager.RemoveFromGroup(e.Username, g.Name);
                await _gmCommands.DeleteMembership(gm);
            }
            return Ok();
        }
    }
}
