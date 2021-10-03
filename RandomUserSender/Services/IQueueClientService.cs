using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues.Models;

namespace RandomUserSender.Services
{
    public interface IQueueClientService
    {
        Task<SendReceipt> SendMessageAsync(string message, 
            TimeSpan? visibilityTimeout = null, 
            TimeSpan? timeToLive = null,
            CancellationToken cancellationToken = default);

        Task<QueueMessage> ReceiveMessageAsync(TimeSpan? visibilityTimeout = null, 
            CancellationToken cancellationToken = default);
    }
}