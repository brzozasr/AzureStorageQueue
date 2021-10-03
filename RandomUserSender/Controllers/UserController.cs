using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUserSender.Services;
using SharedModels;

namespace RandomUserSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUsersQueue _usersQueue;

        public UserController(ILogger<UserController> logger, IUsersQueue usersQueue)
        {
            _logger = logger;
            _usersQueue = usersQueue;
        }

        [HttpGet("Send/To/Cloude/Queue")]
        public async Task<IActionResult> SendUser()
        {
            User user = null;

            if (_usersQueue.QueueWithUsers.Count > 0)
            {
                user = _usersQueue.QueueWithUsers.Dequeue();
                _logger.LogInformation("User was sent to the Azure Queue");
            }
           
            return Ok(user);
        }
        
        
    }
}
