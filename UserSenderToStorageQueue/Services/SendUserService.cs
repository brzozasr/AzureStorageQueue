using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SharedModels;

namespace UserSenderToStorageQueue.Services
{
    class SendUserService : ISendUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ISendUserService> _logger;

        public SendUserService(HttpClient httpClient, ILogger<ISendUserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<User> SendAsync()
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

                        if (user != null && user.ResultUsers.Any())
                        {
                            var userName = string.Empty;
                            var userSurname = string.Empty;

                            foreach (var userResult in user.ResultUsers)
                            {
                                userName = userResult.Name.First;
                                userSurname = userResult.Name.Last;
                                break;
                            }

                            _logger.LogInformation($"The user {userName} {userSurname} was sent to the Azure Queue");
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                       
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
