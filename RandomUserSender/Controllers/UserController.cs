using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomUserSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Send/To/Cloude/Queue")]
        public async Task<IActionResult> SendUser()
        {
            _logger.LogInformation("User was sent to the Azure Queue");
            return Ok(new {Test = "test"});
        }
        
        
    }
}
