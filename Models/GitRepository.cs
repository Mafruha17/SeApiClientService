using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Globalization;

namespace SeApiClientService.Models
{
    public class GitRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("html_url")]
        public Uri Url { get; set; }

        [JsonPropertyName("homepage")]
        public Uri Page { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

    }
}
