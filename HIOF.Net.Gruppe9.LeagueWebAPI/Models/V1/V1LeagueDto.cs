using System;
using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1LeagueDto
    {
        [JsonPropertyName("leagueId")]
        public string LeagueId { get; set; }


        [JsonPropertyName("summonerId")]
        public string SummonerId { get; set; }


        [JsonPropertyName("summonerName")]
        public string SummonerName { get; set; }


        [JsonPropertyName("queueType")]
        public string QueueType { get; set; }


        [JsonPropertyName("tier")]
        public string Tier { get; set; }


        [JsonPropertyName("rank")]
        public string? Rank { get; set; }


        [JsonPropertyName("leaguePoints")]
        public int LeaguePoints { get; set; }


        [JsonPropertyName("wins")]
        public int Wins { get; set; }


        [JsonPropertyName("losses")]
        public int Losses { get; set; }


        [JsonPropertyName("hotStreak")]
        public bool HotStreak { get; set; }


        [JsonPropertyName("veteran")]
        public bool Veteran { get; set; }

        [JsonPropertyName("freshBlood")]
        public bool FreshBlood { get; set; }

        [JsonPropertyName("inactive")]
        public bool Inactive { get; set; }

        [JsonPropertyName("miniSeries")]
        public MiniSeriesDTO MiniSeries { get; set; }

    }
    public class MiniSeriesDTO
    {
        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("progress")]
        public string Progress { get; set; }

        [JsonPropertyName("target")]
        public int Target { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }
    }
}
