using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1
{
    [Route("api/1.0/Players")]
    [ApiController]
    public class V1PlayersController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public V1PlayersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/1.0/GetPlayers/{summonerId}
        [HttpGet("{summonerId}")]
        public async Task<IActionResult> GetPlayers(string summonerId)
        {
            HttpClient client = _httpClientFactory.CreateClient("httpclient");
            if (client == null)
            {
                return new BadRequestObjectResult($"Can not create http client");
            }

            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/clash/v1/players/by-summoner/{summonerId}");
            List<V1PlayersDto> players = null;
            
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<List<V1PlayersDto>>();
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "Players");
            }

            return new OkObjectResult(players);
        }

    }
}
