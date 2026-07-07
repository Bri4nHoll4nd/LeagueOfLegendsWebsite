using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1TournamentDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("themeId")]
        public int ThemeId { get; set; }

        [JsonPropertyName("namekey")]
        public string NameKey { get; set; }

        [JsonPropertyName("nameKeySecondary")]
        public string NameKeySecondary { get; set; }

        [JsonPropertyName("schedule")]
        public List<TournamentPhaseDto> Schedule { get; set; }
    }

    public class TournamentPhaseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("registrationTime")]
        public long RegistrationTime { get; set; }

        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        [JsonPropertyName("cancelled")]
        public Boolean Cancelled { get; set; }


    }
}