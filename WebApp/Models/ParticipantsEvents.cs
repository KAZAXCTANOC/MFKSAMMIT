using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ParticipantsEvents
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("idevent")]
        public int Idevent { get; set; }

        [JsonPropertyName("idvisitor")]
        public int Idvisitor { get; set; }
    }
}
