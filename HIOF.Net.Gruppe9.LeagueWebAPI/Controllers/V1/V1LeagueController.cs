using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers
{
    [Route("api/1.0/League")]
    [ApiController]
    public class V1LeagueController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1LeagueController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/league/{summonerName}
        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetLeague(string summonerName)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return BadRequest($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage summonerResponse = await client.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}");
            V1SummonerDto? summoner = await summonerResponse.Content.ReadAsAsync<V1SummonerDto>();
            if (summoner == null)
            {
                return new BadRequestObjectResult($"Could not found summoner");
            }
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/league/v4/entries/by-summoner/{summoner.Id}");
            if (response.IsSuccessStatusCode)
            {
                List<V1LeagueDto>? leagueList = await response.Content.ReadAsAsync<List<V1LeagueDto>>();
                return new OkObjectResult(leagueList);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "League");
            }
        }
    }
}
