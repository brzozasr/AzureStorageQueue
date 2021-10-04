using System.IO;
using Microsoft.Extensions.Configuration;

namespace UserReceiverFromStorageQueue.Utilities
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

        public static void CutString(ref string stringToCut, int maxStringLength, 
            int lengthAfterCut, string suffix = null)
        {
            if (stringToCut.Length > maxStringLength)
            {
                stringToCut = $"{stringToCut.Substring(0, lengthAfterCut).TrimEnd()}{suffix}";
            }
            
        }
    }
}
