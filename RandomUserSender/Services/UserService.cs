using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RandomUserSender.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IUserService> _logger;

        public UserService(HttpClient httpClient, ILogger<IUserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<User> GetUserServiceAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await _httpClient.GetAsync(_httpClient.BaseAddress))
                {
                    var content = await response.Content.ReadAsStreamAsync();

                    if (content.Length > 0)
                    {
                        var user = await JsonSerializer.DeserializeAsync<User>(content);
                        _logger.LogInformation("Deserialization of the user successful");
                        return user;
                    }
                }
               
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
