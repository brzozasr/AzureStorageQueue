using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Street
    {
        [JsonPropertyName("number")] 
        public int Number { get; set; }

        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}