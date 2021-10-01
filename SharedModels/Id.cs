using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Id
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }

        [JsonPropertyName("value")] 
        public string Value { get; set; }
    }
}