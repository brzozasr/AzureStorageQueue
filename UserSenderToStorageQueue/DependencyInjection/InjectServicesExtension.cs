using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            services.AddSingleton<ISendUserService, SendUserService>(serviceProvider => 
                new SendUserService(new HttpClient
                {
                    BaseAddress = new Uri(Helper.GetAppSettingsJsonValue("UserApi:BaseUri"))
                }, serviceProvider.GetRequiredService<ILogger<ISendUserService>>()));

            // Above add injected services
            return services.BuildServiceProvider();
        }
    }
}
