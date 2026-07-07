using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers
{
    [Route("api/1.0/Tournament")]
    [ApiController]
    public class V1TournamentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1TournamentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/Tournament/{tournamentId}
        [HttpGet("{tournamentId}")]
        public async Task<IActionResult> GetTournment(string tournamentId)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/clash/v1/tournaments/{tournamentId}");
            if (response.IsSuccessStatusCode)
            {
                V1TournamentDto tournament = await response.Content.ReadAsAsync<V1TournamentDto>();
                return new OkObjectResult(tournament);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "Tournment");
            }
        }
    }
}