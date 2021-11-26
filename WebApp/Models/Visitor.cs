using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Visitor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("adress")]
        public string Adress { get; set; }
    }
}
