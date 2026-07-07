using HIOF.Net.Gruppe9.LeagueWebAPI.BackgroundJob.Models.V1;
using HIOF.Net.Gruppe9.LeagueWebAPI.Helpers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1
{
    [Route("api/1.0/ChampionMastery")]
    [ApiController]
    public class V1ChampionMasteryController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IMemoryCache _cache;

        public V1ChampionMasteryController(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        // GET: api/1.0/ChampionMastery/{summonerName}/{championId}
        [HttpGet("{summonerName}/{championId}")]
        public async Task<IActionResult> GetChampionMastery(string summonerName, long championId, bool useCache = true)
        {
            if (useCache)
            {
                if (_cache.TryGetValue(championId, out V1ChampionMasteryDto championFromCache))
                {
                    return new OkObjectResult(championFromCache);
                }
            }

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
                return new BadRequestObjectResult($"Could not found summoner");
            }
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{summoner.Id}/by-champion/{championId}");
            if (response.IsSuccessStatusCode)
            {
                V1ChampionMasteryDto? championMastery = await response.Content.ReadAsAsync<V1ChampionMasteryDto>();

                if (useCache)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                    _cache.Set(championId, championMastery, cacheEntryOptions);
                }

                return new OkObjectResult(championMastery);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "ChampionMastery");
            }
        }

        // GET: api/1.0/ChampionMastery/{summonerName}
        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetChampionMasteryList(string summonerName)
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
            HttpResponseMessage response = await client.GetAsync($"https://euw1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{summoner.Id}");
            if (response.IsSuccessStatusCode)
            {
                List<V1ChampionMasteryDto>? championMasteryList = await response.Content.ReadAsAsync<List<V1ChampionMasteryDto>>();
                return new OkObjectResult(championMasteryList);
            }
            else
            {
                return HttpResponseMessageHelper.GetResponseCode(response, "ChampionMastery");
            }
        }
    }
}
