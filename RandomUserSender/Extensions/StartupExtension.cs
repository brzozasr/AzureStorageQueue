using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RandomUserSender.Services;

namespace RandomUserSender.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddUserService(this IServiceCollection services, Uri uri)
        {
            return services.AddScoped<IUserService, UserService>(serviceProvide =>
                new UserService(new HttpClient
                {
                    BaseAddress = uri
                }));
        }
    }
}
