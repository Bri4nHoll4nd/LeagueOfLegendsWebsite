using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1
{
    [Route("api/1.0/ClashTeam")]
    [ApiController]

    //
    // The Dto for this Controller is in Models/V1/V1ClashTournamentDto.cs
    //
    public class V1ClashTournamentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1ClashTournamentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/GetPlayers/{summonerName}
        [HttpGet("Player/{summonerName}")]
        public async Task<IActionResult> GetClashPlayer(string summonerName)
        {

            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage summonerResponse = await client.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}");
            V1SummonerDto? summoner = await summonerResponse.Content.ReadAsAsync<V1SummonerDto>();
            if (summoner == null)
            {
                return new BadRequestObjectResult($"Could not find summoner");
            }
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/clash/v1/players/by-summoner/{summoner.Id}");
            List<V1PClashPlayerDto> clashTeamPlayer = null;
            if (summonerResponse.IsSuccessStatusCode)
            {
                clashTeamPlayer = await response.Content.ReadAsAsync<List<V1PClashPlayerDto>>();
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(summonerResponse, "ClashPlayer");
            }

            return new OkObjectResult(clashTeamPlayer);
        }

        // GET: api/1.0/GetClashTeam/{teamId}
        [HttpGet("Team/{teamId}")]
        public async Task<IActionResult> GetClashTeam(string teamId)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage teamResponse = await client.GetAsync($"https://euw1.api.riotgames.com/lol/clash/v1/teams/{teamId}");
            V1ClashTeamDto team = await teamResponse.Content.ReadAsAsync<V1ClashTeamDto>();
            if (team == null)
            {
                return new BadRequestObjectResult($"Could not find team");
            }
            if (teamResponse.IsSuccessStatusCode)
            {
                return new OkObjectResult(team);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(teamResponse, "Team");
            }

        }

        //GET: api/1.0/GetClashTorunament/{tournamentId}
        [HttpGet("Tournament/{tournamentId}")]

        public async Task<IActionResult> GetTournament(string tournamentId)
        {
            HttpClient client = (_httpClientFactory.CreateClient("httpclient"));
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage tournamentResponse = await client.GetAsync($"https://euw1.api.riotgames.com/lol/clash/v1/tournaments/{tournamentId}");
            V1ClashTournamentInfoDto tournament = await tournamentResponse.Content.ReadAsAsync<V1ClashTournamentInfoDto>();
            if (tournament == null)
            {
                return new BadRequestObjectResult($"Could not find the tournament");
            }
            if (tournamentResponse.IsSuccessStatusCode)
            {
                return new OkObjectResult(tournament);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(tournamentResponse, "Tournament");
            }

        }
    }
}
