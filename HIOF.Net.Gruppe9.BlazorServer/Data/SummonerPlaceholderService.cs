namespace HIOF.Net.Gruppe9.BlazorServer.Data
{
    public class SummonerPlaceholderService
    {
        private static readonly string[] Summoners = new[]
        {
        "Summoner1", "Summoner2", "Summoner3", "Summoner4", "Summoner5"
    };

        public Task<List<SummonerPlaceholder>> GetSummonersAsync()
        {
            var summoners = new List<SummonerPlaceholder>();
            for(int i = 0; i<Summoners.Length; i++)
            {
                summoners.Add(new SummonerPlaceholder { Name = Summoners[i] });
            }
            return Task.FromResult<List<SummonerPlaceholder>>(summoners);
        }
    }
}