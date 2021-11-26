using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class VisitorFromEvent
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("idevent")]
        public int idevent { get; set; }

        [JsonPropertyName("idvisitor")]
        public int idvisitor { get; set; }
    }
}
