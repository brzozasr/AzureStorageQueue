using System.Collections.Generic;
using SharedModels;

namespace RandomUserSender.Services
{
    public partial interface IUsersQueue
    {
        Queue<User> QueueWithUsers { get; set; }
    }
}