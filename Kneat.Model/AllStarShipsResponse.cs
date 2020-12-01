using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kneat.Model
{
    public class AllStarShipsResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public IEnumerable<StarShip> Result { get; set; }
    }
}
