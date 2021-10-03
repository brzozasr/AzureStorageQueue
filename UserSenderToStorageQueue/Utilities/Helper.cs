using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UserSenderToStorageQueue.Utilities
{
    class Helper
    {
        public static string GetAppSettingsJsonValue(string appSettingsJsonValue)
        {
            var pathCurrentDir = Directory.GetCurrentDirectory();
            var pathCombine = Path.GetFullPath(Path.Combine(pathCurrentDir, @"..\..\..\"));
            var fileName = "appsettings.json";

            var usedPath = File.Exists($"{pathCurrentDir}\\{fileName}") ? pathCurrentDir : pathCombine;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(usedPath)
                .AddJsonFile(fileName, true)
                .Build();
            return configuration[appSettingsJsonValue];
        }
    }
}
