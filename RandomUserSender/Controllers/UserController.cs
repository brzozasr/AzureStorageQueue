using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
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
            var connectionString = "";
            var queueName = "";
            var queueClient = new QueueClient(connectionString, queueName);

            User user = null;

            if (_usersQueue.QueueWithUsers.Count > 0)
            {
                user = _usersQueue.QueueWithUsers.Dequeue();
                var message = JsonSerializer.Serialize(user);
                await queueClient.SendMessageAsync(message);
                _logger.LogInformation("User was sent to the Azure Queue");
            }
           
            return Ok(user);
        }
        
        
    }
}
