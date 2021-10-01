using System.IO;
using Microsoft.Extensions.Configuration;

namespace RandomUserSender.Utilities
{
    public class AppSettings
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
