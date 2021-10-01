using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Timezone
    {
        [JsonPropertyName("offset")] 
        public string Offset { get; set; }

        [JsonPropertyName("description")] 
        public string Description { get; set; }
    }
}