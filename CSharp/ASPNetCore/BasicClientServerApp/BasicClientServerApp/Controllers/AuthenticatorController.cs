using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicClientServerApp.Models;
using BasicClientServerApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicClientServerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticatorController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Auth(AuthenticationRequestModel model)
        {
            try
            {
                return Ok(await authenticationService.Authenticate(model));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
