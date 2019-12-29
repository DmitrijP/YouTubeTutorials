using Microsoft.AspNetCore.Mvc;

namespace BasicClientServerApp.Server.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        [Route("{action}")]
        public string GiveMyLocation()
        {
            return "You are in Mannheim!";
        }
    }
}
