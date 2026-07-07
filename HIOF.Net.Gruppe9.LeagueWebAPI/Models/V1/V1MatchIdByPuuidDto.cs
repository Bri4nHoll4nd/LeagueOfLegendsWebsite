using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1MatchIdByPuuidDto
    {
        [JsonPropertyName("MatchId")]
        public string? MatchId { get; set; }
    }
}