using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues.Models;

namespace UserSenderToStorageQueue.Services
{
    internal interface IQueueClientService
    {
        Task<QueueMessage> ReceiveMessageAsync(TimeSpan? visibilityTimeout = null,
            CancellationToken cancellationToken = default);
    }
}