using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using UserSenderToStorageQueue.Services;
using UserSenderToStorageQueue.Utilities;

namespace UserSenderToStorageQueue.DependencyInjection
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
