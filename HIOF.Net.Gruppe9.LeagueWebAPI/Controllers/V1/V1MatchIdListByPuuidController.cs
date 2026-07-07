using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1
{
    [Route("api/1.0/MatchIdListByPuuid")]
    [ApiController]
    public class V1MatchIdListByPuuidController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1MatchIdListByPuuidController(IHttpClientFactory httpClientFactory  )
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/MatchIdListByPuuid/{puuid}
        [HttpGet("{puuid}")]
        public async Task<IActionResult> GetMatchIdListByPuuid(string puuid)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage response = await client.GetAsync($"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids");
            if (response == null)
            {
                return new BadRequestObjectResult($"Could not find matchId");
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                List<string> matchIdStrings = JsonSerializer.Deserialize<List<string>>(jsonContent);
                List<V1MatchIdByPuuidDto> matchIdList = matchIdStrings.Select(id => new V1MatchIdByPuuidDto { MatchId = id }).ToList();

                return new OkObjectResult(matchIdList);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "puuid");
            }
        }
    }
}