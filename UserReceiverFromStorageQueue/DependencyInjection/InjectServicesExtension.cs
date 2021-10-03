using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserSenderToStorageQueue.Services;
using UserSenderToStorageQueue.Utilities;

namespace UserSenderToStorageQueue.DependencyInjection
{
    static class InjectServicesExtension
    {
        public static IServiceProvider InjectServices(this IServiceCollection services)
        {
            services.AddLogging(config => config.AddConsole());
            services.AddSingleton<IQueueClientService, QueueClientService>(serviceProvider => 
                new QueueClientService(new QueueClient(
                    Helper.GetAppSettingsJsonValue("StorageQueue:ConnectionString"),
                    Helper.GetAppSettingsJsonValue("StorageQueue:QueueName"))));

            // Above add injected services
            return services.BuildServiceProvider();
        }
    }
}
