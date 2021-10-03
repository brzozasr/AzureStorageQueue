using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserSenderToStorageQueue.DependencyInjection;
using UserSenderToStorageQueue.Services;

namespace UserSenderToStorageQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configuration DI
            var serviceProvider = DI
                .BuildServiceProvider()
                .InjectServices();
            
            // Console logging configuration
            var logger = serviceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();
            
            // Get the Queue Client
            var sendUserService = serviceProvider
                .GetRequiredService<ISendUserService>();

            Console.WriteLine("Press CTRL + C to exit...");

            while (true)
            {
                logger.LogInformation("Sending a user to the Azure Queue");
                var user = await sendUserService.SendAsync();

                if (user == null)
                {
                    logger.LogInformation("The user was not sent due to it is equal null");
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
