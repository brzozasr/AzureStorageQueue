using System;
using System.Text.Json.Serialization;

namespace SharedModels
{
    public partial class Registered
    {
        [JsonPropertyName("date")] 
        public DateTime Date { get; set; }

        [JsonPropertyName("age")] 
        public int Age { get; set; }
    }
}