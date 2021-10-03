using System.Threading.Tasks;
using SharedModels;

namespace RandomUserSender.Services
{
    public interface IUserService
    {
        Task<User> GetUserServiceAsync();
    }
}