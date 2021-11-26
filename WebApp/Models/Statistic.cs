using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Statistic
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }
    }
}
