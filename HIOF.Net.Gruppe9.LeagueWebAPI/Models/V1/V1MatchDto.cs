using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1MatchDto
    {
        [JsonPropertyName("info")]
        public InfoDto Info { get; set; }
    }
    public class InfoDto
    {
        [JsonPropertyName("gameld")]
        public long Gameld { get; set; }

        [JsonPropertyName("gameMode")]
        public string GameMode { get; set; }

        [JsonPropertyName("gameName")]
        public String GameName { get; set; }

        [JsonPropertyName("gameType")]
        public String GameType { get; set; }

        [JsonPropertyName("gameVersion")]
        public String GameVersion { get; set; }

        [JsonPropertyName("gameDuration")]
        public long GameDuration { get; set; }

        [JsonPropertyName("gameStartTimestamp")]
        public long GameStartTimestamp { get; set; }

        [JsonPropertyName("gameEndTimestamp")]
        public long GameEndTimestamp { get; set; }

        [JsonPropertyName("platformId")]
        public string PlatformId { get; set; }

        [JsonPropertyName("queueid")]
        public int QueueID { get; set; }

        [JsonPropertyName("participants")]
        public List<ParticipantDto> Participants { get; set; }
    }

    public class ParticipantDto
    {
        [JsonPropertyName("champLevel")]
        public int ChampLevel { get; set; }

        [JsonPropertyName("championId")]
        public int ChampionId { get; set; }

        [JsonPropertyName("championName")]
        public string ChampionName { get; set; }

        [JsonPropertyName("win")]
        public Boolean Win { get; set; }


    }
}
