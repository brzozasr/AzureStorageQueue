using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class User
    {
        [JsonPropertyName("results")] 
        public IEnumerable<ResultUser> ResultUsers { get; set; }

        [JsonPropertyName("info")] 
        public Info Info { get; set; }
    }
}