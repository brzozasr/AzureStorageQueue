using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedModels;

namespace RandomUserSender.Services
{
    public class UserService : IUserService
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

            using (var response = await _httpClient.GetAsync(_httpClient.BaseAddress))
            {
                //var content = await response.Content.ReadAsStreamAsync();
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content) && content.Contains("results") && content.Contains("gender"))
                {
                    //var user = await JsonSerializer.DeserializeAsync<User>(content);
                    var user = JsonConvert.DeserializeObject<User>(content);
                    return user;
                }
            }

            return null;
        }
    }
}
