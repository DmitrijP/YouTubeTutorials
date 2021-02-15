using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using EmployeeManagementSystem.ReSTapi.Mapping;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ReSTapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeQueries _employeeQueries;
        private readonly IEmployeeCommands _employeeCommands;
        private readonly EmployeeMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeQueries employeeQueries, IEmployeeCommands employeeCommands, EmployeeMapper mapper)
        {
            _logger = logger;
            _employeeQueries = employeeQueries;
            _employeeCommands = employeeCommands;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var e = await _employeeQueries.SelectAllEmployee();
            return Ok(e);
        }

        [HttpGet]
        [Route("get-one")]
        public async Task<IActionResult> Get(int id)
        {
            var e = await _employeeQueries.SelectEmployee(_mapper.GenerateIdOnly(id));
            return Ok(_mapper.Map(e));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            var e = await _employeeCommands.InsertEmployee(_mapper.Map(employee));
            return Ok(_mapper.Map(e));
        }


        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            var toQuery = _mapper.Map(employee);
            await _employeeCommands.UpdateEmployee(toQuery);
            var e = await _employeeQueries.SelectEmployee(toQuery);
            return Ok(_mapper.Map(e));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeCommands.SetDeletedDate(_mapper.GenerateIdOnly(id));
            return Ok();
        }
    }
}
