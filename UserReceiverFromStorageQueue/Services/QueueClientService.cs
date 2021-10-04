using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace UserReceiverFromStorageQueue.Services
{
    class QueueClientService : IQueueClientService
    {
        private readonly QueueClient _queueClient;

        public QueueClientService(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task<QueueMessage> ReceiveMessageAsync(TimeSpan? visibilityTimeout = null,
            CancellationToken cancellationToken = default)
        {
            return await _queueClient.ReceiveMessageAsync(visibilityTimeout, cancellationToken);
        }

        public async Task<Response> DeleteMessageAsync(string messageId, string popReceipt,
            CancellationToken cancellationToken = default)
        {
            return await _queueClient.DeleteMessageAsync(messageId, popReceipt, cancellationToken);
        }
    }
}
