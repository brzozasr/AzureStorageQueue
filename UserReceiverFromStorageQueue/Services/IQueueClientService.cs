using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues.Models;

namespace UserReceiverFromStorageQueue.Services
{
    internal interface IQueueClientService
    {
        Task<QueueMessage> ReceiveMessageAsync(TimeSpan? visibilityTimeout = null,
            CancellationToken cancellationToken = default);

        Task<Response> DeleteMessageAsync(string messageId, string popReceipt,
            CancellationToken cancellationToken = default);
    }
}