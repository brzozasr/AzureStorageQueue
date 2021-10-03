using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using RandomUserSender.Services;

namespace RandomUserSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUsersQueue _usersQueue;
        private readonly IQueueClientService _queueClientService;

        public UserController(ILogger<UserController> logger, IUsersQueue usersQueue, IQueueClientService queueClientService)
        {
            _logger = logger;
            _usersQueue = usersQueue;
            _queueClientService = queueClientService;
        }

        [HttpGet("Send/To/Cloud/Queue")]
        public async Task<IActionResult> SendUser()
        {
            try
            {
                if (_usersQueue.QueueWithUsers.Count > 0)
                {
                    var user = _usersQueue.QueueWithUsers.Dequeue();
                    var message = JsonSerializer.Serialize(user);
                    await _queueClientService.SendMessageAsync(message);
                    _logger.LogInformation("User was sent to the Azure Queue");

                    return Ok(user);
                }

                _logger.LogWarning("The queue is empty");
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"{e.Message}");
            }
        }
        
        
    }
}
