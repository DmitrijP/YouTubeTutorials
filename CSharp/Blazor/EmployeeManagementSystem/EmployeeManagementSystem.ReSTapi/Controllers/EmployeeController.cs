using EmployeeManagementSystem.Data.Shared.Interfaces;
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

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeQueries employeeQueries)
        {
            _logger = logger;
            _employeeQueries = employeeQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var e = await _employeeQueries.SelectAllEmployee();
            return Ok(e);            
        }
    }
}
