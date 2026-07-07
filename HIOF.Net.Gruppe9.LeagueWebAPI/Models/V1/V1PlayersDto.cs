using System;
using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1PlayersDto
    {
        [JsonPropertyName("summonerId")]
        public string SummonerId { get; set; }


        [JsonPropertyName("teamId")]
        public string TeamId { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

    }
}
