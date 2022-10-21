using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01Trello.Models
{
    public class TrelloList
    {
        [JsonProperty("id")]
        public string ListId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool IsClosed { get; set; }
    }
}
