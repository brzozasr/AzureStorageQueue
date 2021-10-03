using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace RandomUserSender.Services
{
    public class QueueClientService : IQueueClientService
    {
        private readonly QueueClient _queueClient;

        public QueueClientService(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task<SendReceipt> SendMessageAsync(string message, 
            TimeSpan? visibilityTimeout = null, 
            TimeSpan? timeToLive = null,
            CancellationToken cancellationToken = default)
        {
            return await _queueClient.SendMessageAsync(message, visibilityTimeout, timeToLive, cancellationToken);
        }

        public async Task<QueueMessage> ReceiveMessageAsync(TimeSpan? visibilityTimeout = null, 
            CancellationToken cancellationToken = default)
        {
            return await _queueClient.ReceiveMessageAsync(visibilityTimeout, cancellationToken);
        }
    }
}
