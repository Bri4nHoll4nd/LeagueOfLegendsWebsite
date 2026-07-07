using HIOF.Net.Gruppe9.LeagueWebAPI.BackgroundJob.Models.V1;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.BackgroundJob
{
    public class BackgroundJob : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BackgroundJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                DateTime scheduledTime = new DateTime(now.Year, now.Month, now.Day, 23, 59, 0);
                TimeSpan delay = scheduledTime - now;

                if (delay < TimeSpan.Zero)
                {
                    delay += TimeSpan.FromDays(1);
                }

                await Task.Delay(delay, stoppingToken);

                HttpClient client = _httpClientFactory.CreateClient("httpclient");
                if (client == null)
                {
                    return;
                }

                client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-54de1d46-1dec-443b-bff6-daacd0c0ac86");
                // calling the versions api to get the latest version
                HttpResponseMessage response = await client.GetAsync($"https://ddragon.leagueoflegends.com/api/versions.json");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    // storing versions in a list
                    var versionsList = JsonConvert.DeserializeObject<List<string>>(responseJson);
                    if (versionsList != null)
                    {
                        // getting latest version from list
                        var latestVersion = versionsList.FirstOrDefault();

                        var getVersion = await client.GetAsync("https://localhost/V1/Version/GetVersion");
                        var responseVersion = await getVersion.Content.ReadAsStringAsync();
                        var version = (string)JsonNode.Parse(responseVersion)["value"]["name"];

                        if (version != latestVersion)
                        {
                            await client.PatchAsync($"https://localhost/V1/Version/Update/{latestVersion}", null);
                            version = latestVersion;

                            // calling the champions.json and passing the latest version
                            response = await client.GetAsync($"http://ddragon.leagueoflegends.com/cdn/{version}/data/en_US/champion.json");
                            if (response.IsSuccessStatusCode)
                            {
                                responseJson = await response.Content.ReadAsStringAsync();
                                var championData = JsonConvert.DeserializeObject<dynamic>(responseJson);
                                foreach (var item in championData.SelectTokens("data.*"))
                                {
                                    try
                                    {
                                        V1PostChampion champ = new V1PostChampion();
                                        champ.Version = item["version"];
                                        champ.RiotId = item["id"];
                                        champ.RiotKey = item["key"];
                                        champ.Name = item["name"];
                                        champ.Title = item["title"];
                                        champ.Blurb = item["blurb"];
                                        champ.Tag1 = item["tags"][0];
                                        if (item["tags"].Count == 2)
                                        {
                                            champ.Tag2 = item["tags"][1];
                                        }
                                        champ.Partype = item["partype"];
                                        champ.Full = item["image"]["full"];
                                        champ.Sprite = item["image"]["sprite"];
                                        champ.Group = item["image"]["group"];
                                        champ.X = item["image"]["x"];
                                        champ.Y = item["image"]["y"];
                                        champ.Width = item["image"]["w"];
                                        champ.Height = item["image"]["h"];
                                        champ.Attack = item["info"]["attack"];
                                        champ.Defence = item["info"]["defense"];
                                        champ.Magic = item["info"]["magic"];
                                        champ.Difficulty = item["info"]["difficulty"];
                                        champ.Hp = item["stats"]["hp"];
                                        champ.HpPerLevel = item["stats"]["hpperlevel"];
                                        champ.Mp = item["stats"]["mp"];
                                        champ.MpPerLevel = item["stats"]["mpperlevel"];
                                        champ.MoveSpeed = item["stats"]["movespeed"];
                                        champ.Armour = item["stats"]["armor"];
                                        champ.ArmourPerLevel = item["stats"]["armorperlevel"];
                                        champ.SpellBlock = item["stats"]["spellblock"];
                                        champ.SpellBlockPerLevel = item["stats"]["spellblockperlevel"];
                                        champ.AttackRange = item["stats"]["attackrange"];
                                        champ.HpRegen = item["stats"]["hpregen"];
                                        champ.HpRegenPerLevel = item["stats"]["hpregenperlevel"];
                                        champ.MpRegen = item["stats"]["mpregen"];
                                        champ.MpRegenPerLevel = item["stats"]["mpregenperlevel"];
                                        champ.Crit = item["stats"]["crit"];
                                        champ.CritPerLevel = item["stats"]["critperlevel"];
                                        champ.AttackDamage = item["stats"]["attackdamage"];
                                        champ.AttackDamagePerLevel = item["stats"]["attackdamageperlevel"];
                                        champ.AttackSpeedPerLevel = item["stats"]["attackspeedperlevel"];
                                        champ.AttackSpeed = item["stats"]["attackspeed"];

                                        var content = JsonConvert.SerializeObject(champ);
                                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                                        var byteContent = new ByteArrayContent(buffer);
                                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                                        await client.PatchAsync($"https://localhost:7214/V1/Champion/Update/{champ.Name}", byteContent);
                                    }
                                    catch (Exception e)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
