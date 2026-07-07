using System.Text.Json.Serialization;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1
{
    public class V1ClashTournamentDto
    {
    }

    public class V1PClashPlayerDto
    { 
        // ClashTeamPlayers (PlayerDto)
        // /clash/v1/players/by-summoner/{summonerId}
        [JsonPropertyName("summonerId")]
        public string? SummonerId { get; set; }

        [JsonPropertyName("teamId")]
        public string? TeamId { get; set; }

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

    }
    // ClashTeam
    //clash/v1/teams/{teamId}
    public class V1ClashTeamDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("tournamentId")]
        public int tournamentId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("iconId")]
        public int iconId { get; set; }

        [JsonPropertyName("tier")]
        public int tier { get; set; }

        [JsonPropertyName("captain")]
        public string? captain { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? abbreviation { get; set; }

        public List<V1ClashTeamPlayerDto>? teamMembers { get; set; }

    }

    // ClashPlayer
    public class V1ClashTeamPlayerDto
    {
        [JsonPropertyName("summonerId")]
        public string? SummonerId { get; set; }

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }

    //clash/v1/tournaments/{tournamentId}
    public class V1ClashTournamentInfoDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("themeId")]
        public int ThemeId { get; set; }

        [JsonPropertyName("namekey")]
        public string? NameKey { get; set; }

        [JsonPropertyName("nameKeySecondary")]
        public string? NameKeySecondary { get; set; }

        [JsonPropertyName("schedule")]
        public List<V1ClashTournamentPhaseDto>? Schedule { get; set; }
    }

    public class V1ClashTournamentPhaseDto
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
