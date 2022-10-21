using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ex01Trello.Models
{
    public class TrelloBoard
    {
        [JsonProperty("id")]
        public string BoardId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("starred")]
        public bool IsFavorite { get; set; }
    }
}
