using System.IO;
using Microsoft.Extensions.Configuration;

namespace RandomUserSender.Extensions
{
    public class AppSettingsExtension
    {
        public static string GetAppSettings(string appSettingsJsonValue)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
            return configuration[appSettingsJsonValue];
        }
    }
}
