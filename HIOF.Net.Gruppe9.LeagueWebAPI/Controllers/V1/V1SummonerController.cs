using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers
{
    [Route("api/1.0/Summoner")]
    [ApiController]
    public class V1SummonerController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1SummonerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/Summoner/{summonerName}
        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetSummoner(string summonerName)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}");
            if (response.IsSuccessStatusCode)
            {
                V1SummonerDto? summoner = await response.Content.ReadAsAsync<V1SummonerDto>();
                return new OkObjectResult(summoner);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "Summoner");
            }
        }
    }
}
