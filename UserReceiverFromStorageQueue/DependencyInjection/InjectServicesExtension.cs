using System;
using System.Security.Cryptography.X509Certificates;
using Azure.Storage.Queues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using UserReceiverFromStorageQueue.Services;
using UserReceiverFromStorageQueue.Utilities;

namespace UserReceiverFromStorageQueue.DependencyInjection
{
    static class InjectServicesExtension
    {
        public static IServiceProvider InjectServices(this IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddSerilog(new LoggerConfiguration().WriteTo.File("logs.txt").CreateLogger())
                    .AddConsole();
            });
            
            services.AddSingleton<IQueueClientService, QueueClientService>(serviceProvider => 
                new QueueClientService(new QueueClient(
                    Helper.GetAppSettingsJsonValue("StorageQueue:ConnectionString"),
                    Helper.GetAppSettingsJsonValue("StorageQueue:QueueName"))));

            // Above add injected services
            return services.BuildServiceProvider();
        }
    }
}
