using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class EmptyEvents
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
