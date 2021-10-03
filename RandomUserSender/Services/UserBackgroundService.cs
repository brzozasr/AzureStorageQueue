using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomUserSender.Utilities;
using SharedModels;

namespace RandomUserSender.Services
{
    public class UserBackgroundService : BackgroundService
    {
        private readonly ILogger<UserBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsersQueue _usersQueue;

        public UserBackgroundService(ILogger<UserBackgroundService> logger, IServiceProvider serviceProvider,
            IUsersQueue usersQueue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _usersQueue = usersQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var queueCount = 100;
                    User user = null;

                    if (_usersQueue.QueueWithUsers.Count < queueCount)
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                        user = await userService.GetUserServiceAsync();
                    }
                    

                    if (user is not null && _usersQueue.QueueWithUsers.Count < queueCount)
                    {
                        _usersQueue.QueueWithUsers.Enqueue(user);
                        _logger.LogInformation($"Number of users in the queue is {_usersQueue.QueueWithUsers.Count}");
                    }
                    else if (_usersQueue.QueueWithUsers.Count >= queueCount)
                    {
                        _logger.LogInformation($"The queue is full, the max size of the queue is established to {queueCount}");
                    }
                    else if (user is null)
                    {
                        _logger.LogInformation("The user object is null");
                    }

                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }

        }
    }
}
