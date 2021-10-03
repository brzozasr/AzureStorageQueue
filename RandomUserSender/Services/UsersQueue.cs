using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace RandomUserSender.Services
{
    public class UsersQueue : IUsersQueue
    {
        public Queue<User> QueueWithUsers { get; set; } = new Queue<User>();
    }
}
