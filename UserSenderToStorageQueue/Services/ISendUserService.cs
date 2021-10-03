using System.Threading.Tasks;
using SharedModels;

namespace UserSenderToStorageQueue.Services
{
    internal interface ISendUserService
    {
        Task<User> SendAsync();
    }
}