using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1ChampionMasteryDto
    {
        // Example: 1339
        [JsonPropertyName("championPointsUntilNextLevel")]
        public long ChampionPointsUntilNextLevel { get; set; }

        // Example: false 
        [JsonPropertyName("chestGranted")]
        public bool ChestGranted { get; set; }

        // Example: 12
        [JsonPropertyName("championId")]
        public long ChampionId { get; set; }

        // Example: 1628996362000
        [JsonPropertyName("lastPlayTime")]
        public long LastPlayTime { get; set; }

        // Example: 1
        [JsonPropertyName("championLevel")]
        public int ChampionLevel { get; set; }

        // Example: "p2sW-Fxfb7zNd3FG6bOIsvB5Dd8rx48_YhahSC3JIUMOZ4M"
        [JsonPropertyName("summonerId")]
        public string? SummonerId { get; set; }

        // Example: 461
        [JsonPropertyName("championPoints")]
        public int ChampionPoints { get; set; }

        // Example: 461
        [JsonPropertyName("championPointsSinceLastLevel")]
        public long ChampionPointsSinceLastLevel { get; set; }

        // Example: 0
        [JsonPropertyName("tokensEarned")]
        public int TokensEarned { get; set; }

    }
}
