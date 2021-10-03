using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RandomUserSender.Services;

namespace RandomUserSender.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddUserService(this IServiceCollection services, Uri uri)
        {
            return services.AddScoped<IUserService, UserService>(serviceProvide =>
                new UserService(new HttpClient
                {
                    BaseAddress = uri
                }, 
                serviceProvide.GetRequiredService<ILogger<IUserService>>()));
        }

        public static IServiceCollection AddQueueClientSingleton(this IServiceCollection services,
            string connectionString,
            string queueName)
        {
            return services.AddSingleton<IQueueClientService, QueueClientService>(x =>
                new QueueClientService(new QueueClient(connectionString, queueName)));
        }
    }
}
