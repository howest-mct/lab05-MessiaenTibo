using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01Trello.Models
{
    public class TrelloCard
    {
        [JsonProperty("id")]
        public string CardId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool IsClosed { get; set; }
    }
}
