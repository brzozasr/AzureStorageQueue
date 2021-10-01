using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Coordinates
    {
        [JsonPropertyName("latitude")] 
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")] 
        public string Longitude { get; set; }
    }
}