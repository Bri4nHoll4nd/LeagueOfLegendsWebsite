using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1SummonerDto
    {
        // Example: "0lIQbZrd59QQiNp2YXL3jbYS79pe5iuf2WcEJAf3-PUiLkU"
        [JsonPropertyName("accountId")]
        public string? AccountId { get; set; }

        // Example: 4301
        [JsonPropertyName("profileIconId")]
        public int ProfileIconId { get; set; }

        // Example: 1672670733000
        [JsonPropertyName("revisionDate")]
        public long RevisionDate { get; set; }

        // Example: "7marre"
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        // Example: "p2sW-Fxfb7zNd3FG6bOIsvB5Dd8rx48_YhahSC3JIUMOZ4M"
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        // Example: "YSwWZeV6Tj7ioDHhmr0TS2GwUAITmbforqB6Rwwg2J-LHm5hEJOHYZZsRNVyDwxk4EyTE86v8-k0Eg"
        [JsonPropertyName("puuid")]
        public string? Puuid { get; set; }

        // Example: 217
        [JsonPropertyName("summonerLevel")]
        public long SummonerLevel { get; set; }
    }
}
