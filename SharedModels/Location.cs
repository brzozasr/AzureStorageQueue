using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Location
    {
        [JsonPropertyName("street")] 
        public Street Street { get; set; }

        [JsonPropertyName("city")] 
        public string City { get; set; }

        [JsonPropertyName("state")] 
        public string State { get; set; }

        [JsonPropertyName("country")] 
        public string Country { get; set; }

        [JsonPropertyName("postcode")] 
        public string Postcode { get; set; }

        [JsonPropertyName("coordinates")] 
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("timezone")]
        public Timezone Timezone { get; set; }
    }
}