using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers
{
    [Route("api/1.0/Match")]
    [ApiController]
    public class V1MatchController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1MatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/Match/{matchid}
        [HttpGet("{matchid}")]
        public async Task<IActionResult> GetMatch(string matchid)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage response = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/{matchid}");
            if (response.IsSuccessStatusCode)
            {
                V1MatchDto? match = await response.Content.ReadAsAsync<V1MatchDto>();
                return new OkObjectResult(match);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "Match");
            }
        }
    }
}
