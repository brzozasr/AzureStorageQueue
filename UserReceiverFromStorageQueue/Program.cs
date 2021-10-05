using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedModels;
using UserReceiverFromStorageQueue.DependencyInjection;
using UserReceiverFromStorageQueue.Services;
using UserReceiverFromStorageQueue.Utilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UserReceiverFromStorageQueue
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

            // Get Queue Client
            var queueClient = serviceProvider
                .GetRequiredService<IQueueClientService>();

            Console.WriteLine("Press CTRL + C to exit...");

            while (true)
            {
                logger.LogInformation("Receiving a message from the Azure Storage Queue");
                var queueMessage = await queueClient.ReceiveMessageAsync();

                if (queueMessage != null && queueMessage.MessageText.Contains("gender") 
                                         && queueMessage.MessageText.Contains("title"))
                {
                    logger.LogInformation("Deserialization of the user successful");
                    var user = JsonSerializer.Deserialize<User>(queueMessage.MessageText);

                    JToken jt = JToken.Parse(queueMessage.MessageText);
                    string formattedJson = jt.ToString();
                    Helper.CutString(ref formattedJson, 200, 200, "...");

                    logger.LogInformation(formattedJson);

                    await queueClient.DeleteMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt);
                }
                else
                {
                    logger.LogWarning("The user was not received due to it is equal null");
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
