using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using EmployeeManagementSystem.ReSTapi.Mapping;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ReSTapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {

        private readonly IGroupQueries _queries;
        private readonly IGroupCommands _commands;
        private readonly GroupMapper _mapper;

        public GroupController(IGroupQueries queries, IGroupCommands commands, GroupMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var e = await _queries.SelectAllGroups();
            return Ok(e);
        }

        [HttpGet]
        [Route("get-one")]
        public async Task<IActionResult> Get(int id)
        {
            var e = await _queries.SelectGroup(_mapper.GenerateIdOnly(id));
            return Ok(_mapper.Map(e));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]Group group)
        {
            var e = await _commands.InsertGroup(_mapper.Map(group));
            return Ok(_mapper.Map(e));
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]Group group)
        {
            var toQuery = _mapper.Map(group);
            await _commands.UpdateGroup(toQuery);
            var e = await _queries.SelectGroup(toQuery);
            return Ok(_mapper.Map(e));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commands.SetDeletedDate(_mapper.GenerateIdOnly(id));
            return Ok();
        }
    }
}
