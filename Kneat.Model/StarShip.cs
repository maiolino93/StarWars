
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kneat.Model
{
    public class StarShip
    {
        [JsonProperty("name")]
        public string  Name { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("cost_in_credits")]
        public string Cost { get; set; }

        [JsonProperty("length")]
        public string Lenght { get; set; }

        [JsonProperty("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [JsonProperty("crew")]
        public string Crew { get; set; }

        [JsonProperty("passengers")]
        public string Passengers { get; set; }

        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }

        [JsonProperty("consumables")]
        public string Consumable { get; set; }

        [JsonProperty("hyperdrive_rating")]
        public string HyperDriveRating { get; set; }

        [JsonProperty("MGLT")]
        public string MGLT { get; set; }

        [JsonProperty("starship_class")]
        public string StarShipCLass { get; set; }

        [JsonProperty("pilots")]
        public IEnumerable<string> Pilots { get; set; }

        [JsonProperty("films")]
        public IEnumerable<string> Films { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("edited")]
        public DateTime Edited { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }









    }
}
