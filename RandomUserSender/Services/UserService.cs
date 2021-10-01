using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using RandomUserSender.Extensions;
using SharedModels;

namespace RandomUserSender.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserServiceAsync()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.GetAsync(
                AppSettingsExtension.GetAppSettings("RandomUserMe:BaseApiUrl")))
            {
                var content = await response.Content.ReadAsStreamAsync();

                if (content.Length > 0)
                {
                    var user = await JsonSerializer.DeserializeAsync<User>(content);
                    return user;
                }
            }

            return null;
        }
    }
}
