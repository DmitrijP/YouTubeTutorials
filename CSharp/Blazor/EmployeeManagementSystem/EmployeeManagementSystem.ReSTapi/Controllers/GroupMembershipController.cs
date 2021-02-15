using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using EmployeeManagementSystem.ReSTapi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ReSTapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupMembershipController : ControllerBase
    {

        private readonly IGroupMembershipQueries _queries;
        private readonly IGroupMembershipCommands _commands;
        private readonly IEmployeeQueries _eQueries;
        private readonly IGroupQueries _gQueries;
        private readonly GroupMapper _gMapper;
        private readonly EmployeeMapper _eMapper;
        private readonly GroupMembershipMapper _gmMapper;

        public GroupMembershipController(
            IGroupMembershipQueries queries, IGroupMembershipCommands commands, 
            IEmployeeQueries eQueries, IGroupQueries gQueries,
            GroupMapper gMapper, EmployeeMapper eMapper, GroupMembershipMapper gmMapper)
        {
            _queries = queries;
            _commands = commands;
            _eQueries = eQueries;
            _gQueries = gQueries;
            _gMapper = gMapper;
            _eMapper = eMapper;
            _gmMapper = gmMapper;
        }

        [HttpGet]
        [Route("get-members")]
        public async Task<IActionResult> GetGroupMembers(int id)
        {
            var e = await _queries.SelectGroupMembers(_gMapper.GenerateIdOnly(id));
            var g = await _gQueries.SelectGroup(_gMapper.GenerateIdOnly(id));
            
            return Ok(_gmMapper.Map(_gMapper.Map(g), from x in e select _eMapper.Map(x)));
        }

        [HttpGet]
        [Route("get-groups")]
        public async Task<IActionResult> GetEmployeeGroups(int id)
        {
            var groups = await _queries.SelectGroupsOfEmployee(_eMapper.GenerateIdOnly(id));
            var employee = await _eQueries.SelectEmployee(_eMapper.GenerateIdOnly(id));
            
            return Ok(_gmMapper.Map(_eMapper.Map(employee), from x in groups select _gMapper.Map(x)));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(int groupId, int employeeId)
        {
            await _commands.InsertMembership(_gmMapper.GenerateFromEmployeeGroupId(employeeId, groupId));
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int groupId, int employeeId)
        {
            await _commands.MarkMembershipForDeletion(_gmMapper.GenerateFromEmployeeGroupId(employeeId, groupId));
            return Ok();
        }
    }
}
