using HIOF.Net.Gruppe9.LeaugeChampionDB.Data;
using HIOF.Net.Gruppe9.LeaugeChampionDB.Model.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Controllers.V1
{
    [ApiController]
    [Route("V1/Champion")]
    public class V1ChampionController : ControllerBase
    {
        private readonly ILogger<V1ChampionController> _logger;

        public V1ChampionController(ILogger<V1ChampionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<V1Result<IEnumerable<V1Champion>>?> GetChampions()
        {
            _logger.LogDebug("GetChampions was called");

            var dbContext = new ChampionDBContext();

            try
            {
                var responseChampions = await dbContext.Champions
                .Select(champion => new V1Champion
                {
                    Id = champion.Id,
                    Version = champion.Version,
                    RiotId = champion.RiotId,
                    RiotKey = champion.RiotKey,
                    Name = champion.Name,
                    Title = champion.Title,
                    Blurb = champion.Blurb,
                    Info = new V1Info
                    {
                        Attack = champion.Info.Attack,
                        Defence = champion.Info.Defence,
                        Magic = champion.Info.Magic,
                        Difficulty = champion.Info.Difficulty
                    },
                    Image = new V1Image
                    {
                        Full = champion.Image.Full,
                        Sprite = champion.Image.Sprite,
                        Group = champion.Image.Group,
                        X = champion.Image.X,
                        Y = champion.Image.Y,
                        Width = champion.Image.Width,
                        Height = champion.Image.Height
                    },
                    Tag1 = champion.Tag1,
                    Tag2 = champion.Tag2,
                    Partype = champion.Partype,
                    Stats = new V1Stats
                    {
                        Hp = champion.Stats.Hp,
                        HpPerLevel = champion.Stats.HpPerLevel,
                        Mp = champion.Stats.Mp,
                        MpPerLevel = champion.Stats.MpPerLevel,
                        Armour = champion.Stats.Armour,
                        ArmourPerLevel = champion.Stats.ArmourPerLevel,
                        SpellBlock = champion.Stats.SpellBlock,
                        SpellBlockPerLevel = champion.Stats.SpellBlockPerLevel,
                        AttackRange = champion.Stats.AttackRange,
                        HpRegen = champion.Stats.HpRegen,
                        HpRegenPerLevel = champion.Stats.HpRegenPerLevel,
                        MpRegen = champion.Stats.MpRegen,
                        MpRegenPerLevel = champion.Stats.MpRegenPerLevel,
                        Crit = champion.Stats.Crit,
                        CritPerLevel = champion.Stats.CritPerLevel,
                        AttackDamage = champion.Stats.AttackDamage,
                        AttackDamagePerLevel = champion.Stats.AttackDamagePerLevel,
                        AttackSpeedPerLevel = champion.Stats.AttackSpeedPerLevel,
                        AttackSpeed = champion.Stats.AttackSpeed
                    }
                })
                .ToListAsync();

                return new V1Result<IEnumerable<V1Champion>>(responseChampions);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing get in GetChampions: {0}", e.Message);
                var failedResult = new V1Result<IEnumerable<V1Champion>>(null);
                failedResult.Errors = new List<string>
                {
                    "Couldn't get champions, something went wrong"
                };
                return failedResult;
            }
        }

        [HttpGet("Get/{name}")]
        public V1Result<V1Champion>? GetChampion(string name)
        {
            _logger.LogDebug("GetChampion was called");

            var dbContext = new ChampionDBContext();

            try
            {
                var champion = dbContext.Champions.Where(champion => champion.Name == name).FirstOrDefault();

                var image = new V1Image
                {
                    Full = champion.Image.Full,
                    Sprite = champion.Image.Sprite,
                    Group = champion.Image.Group,
                    X = champion.Image.X,
                    Y = champion.Image.Y,
                    Width = champion.Image.Width,
                    Height = champion.Image.Height
                };
                var info = new V1Info
                {
                    Attack = champion.Info.Attack,
                    Defence = champion.Info.Defence,
                    Magic = champion.Info.Magic,
                    Difficulty = champion.Info.Difficulty
                };
                var stats = new V1Stats
                {
                    Hp = champion.Stats.Hp,
                    HpPerLevel = champion.Stats.HpPerLevel,
                    Mp = champion.Stats.Mp,
                    MpPerLevel = champion.Stats.MpPerLevel,
                    Armour = champion.Stats.Armour,
                    ArmourPerLevel = champion.Stats.ArmourPerLevel,
                    SpellBlock = champion.Stats.SpellBlock,
                    SpellBlockPerLevel = champion.Stats.SpellBlockPerLevel,
                    AttackRange = champion.Stats.AttackRange,
                    HpRegen = champion.Stats.HpRegen,
                    HpRegenPerLevel = champion.Stats.HpRegenPerLevel,
                    MpRegen = champion.Stats.MpRegen,
                    MpRegenPerLevel = champion.Stats.MpRegenPerLevel,
                    Crit = champion.Stats.Crit,
                    CritPerLevel = champion.Stats.CritPerLevel,
                    AttackDamage = champion.Stats.AttackDamage,
                    AttackDamagePerLevel = champion.Stats.AttackDamagePerLevel,
                    AttackSpeedPerLevel = champion.Stats.AttackSpeedPerLevel,
                    AttackSpeed = champion.Stats.AttackSpeed
                };

                var result = new V1Result<V1Champion>(new V1Champion
                {
                    Id = champion.Id,
                    Version = champion.Version,
                    RiotId = champion.RiotId,
                    RiotKey = champion.RiotKey,
                    Name = champion.Name,
                    Title = champion.Title,
                    Blurb = champion.Blurb,
                    Info = info,
                    Image = image,
                    Tag1 = champion.Tag1,
                    Tag2 = champion.Tag2,
                    Partype = champion.Partype,
                    Stats = stats
                });

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing get in GetChampion: {0}", e.Message);
                var failedResult = new V1Result<V1Champion>(null);
                failedResult.Errors = new List<string>
                {
                    "Couldn't get Champion. Either the name is wrong or the champion does not exixst"
                };
                return failedResult;
            }
        }

        [HttpPost("Create")]
        public async Task<V1Result<V1Champion>> CreateChampion(V1PostChampion postChampion)
        {
            _logger.LogDebug("CreateChampion was called");

            var dbContext = new ChampionDBContext();

            var image = new Data.Image
            {
                Full = postChampion.Full,
                Sprite = postChampion.Sprite,
                Group = postChampion.Group,
                X = postChampion.X,
                Y = postChampion.Y,
                Width = postChampion.Width,
                Height = postChampion.Height
            };
            var info = new Data.Info
            {
                Attack = postChampion.Attack,
                Defence = postChampion.Defence,
                Magic = postChampion.Magic,
                Difficulty = postChampion.Difficulty
            };
            var stats = new Data.Stats
            {
                Hp = postChampion.Hp,
                HpPerLevel = postChampion.HpPerLevel,
                Mp = postChampion.Mp,
                MpPerLevel = postChampion.MpPerLevel,
                Armour = postChampion.Armour,
                ArmourPerLevel = postChampion.ArmourPerLevel,
                SpellBlock = postChampion.SpellBlock,
                SpellBlockPerLevel = postChampion.SpellBlockPerLevel,
                AttackRange = postChampion.AttackRange,
                HpRegen = postChampion.HpRegen,
                HpRegenPerLevel = postChampion.HpRegenPerLevel,
                MpRegen = postChampion.MpRegen,
                MpRegenPerLevel = postChampion.MpRegenPerLevel,
                Crit = postChampion.Crit,
                CritPerLevel = postChampion.CritPerLevel,
                AttackDamage = postChampion.AttackDamage,
                AttackDamagePerLevel = postChampion.AttackDamagePerLevel,
                AttackSpeedPerLevel = postChampion.AttackSpeedPerLevel,
                AttackSpeed = postChampion.AttackSpeed
            };

            var champion = new Data.Champion
            {
                Id = Guid.NewGuid(),
                Version = postChampion.Version,
                RiotId = postChampion.RiotId,
                RiotKey = postChampion.RiotKey,
                Name = postChampion.Name,
                Title = postChampion.Title,
                Blurb = postChampion.Blurb,
                Info = info,
                Image = image,
                Tag1 = postChampion.Tag1,
                Tag2 = postChampion.Tag2,
                Partype = postChampion.Partype,
                Stats = stats
            };

            var result = new V1Result<V1Champion>(new V1Champion
            {
                Id = champion.Id,
                Version = postChampion.Version,
                RiotId = postChampion.RiotId,
                RiotKey = postChampion.RiotKey,
                Name = postChampion.Name,
                Title = postChampion.Title,
                Blurb = postChampion.Blurb,
                Tag1 = postChampion.Tag1,
                Tag2 = postChampion.Tag2,
                Partype = postChampion.Partype
            });

            try
            {
                dbContext.Champions.Add(champion);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing add in CreateChampion: {0}", e.Message);
                var failedResult = new V1Result<V1Champion>(null);
                failedResult.Errors = new List<string>
                {
                    "Couldn't create Champion"
                };
                return failedResult;
            }
            
            return result;
        }

        [HttpPatch("Update/{name}")]
        public void PatchChampion(String name, V1PostChampion postChampion)
        {
            _logger.LogDebug("PatchChampion was called");

            var db = new ChampionDBContext();

            var champion = db.Champions.Where(champion => champion.Name == name).FirstOrDefault();

            if (champion != null)
            {
                var image = new Data.Image
                {
                    Full = postChampion.Full,
                    Sprite = postChampion.Sprite,
                    Group = postChampion.Group,
                    X = postChampion.X,
                    Y = postChampion.Y,
                    Width = postChampion.Width,
                    Height = postChampion.Height
                };
                var info = new Data.Info
                {
                    Attack = postChampion.Attack,
                    Defence = postChampion.Defence,
                    Magic = postChampion.Magic,
                    Difficulty = postChampion.Difficulty
                };
                var stats = new Data.Stats
                {
                    Hp = postChampion.Hp,
                    HpPerLevel = postChampion.HpPerLevel,
                    Mp = postChampion.Mp,
                    MpPerLevel = postChampion.MpPerLevel,
                    Armour = postChampion.Armour,
                    ArmourPerLevel = postChampion.ArmourPerLevel,
                    SpellBlock = postChampion.SpellBlock,
                    SpellBlockPerLevel = postChampion.SpellBlockPerLevel,
                    AttackRange = postChampion.AttackRange,
                    HpRegen = postChampion.HpRegen,
                    HpRegenPerLevel = postChampion.HpRegenPerLevel,
                    MpRegen = postChampion.MpRegen,
                    MpRegenPerLevel = postChampion.MpRegenPerLevel,
                    Crit = postChampion.Crit,
                    CritPerLevel = postChampion.CritPerLevel,
                    AttackDamage = postChampion.AttackDamage,
                    AttackDamagePerLevel = postChampion.AttackDamagePerLevel,
                    AttackSpeedPerLevel = postChampion.AttackSpeedPerLevel,
                    AttackSpeed = postChampion.AttackSpeed
                };

                champion.Version = postChampion.Version;
                champion.RiotId = postChampion.RiotId;
                champion.RiotKey = postChampion.RiotKey;
                champion.Name = postChampion.Name;
                champion.Title = postChampion.Title;
                champion.Blurb = postChampion.Blurb;
                champion.Info = info;
                champion.Image = image;
                champion.Tag1 = postChampion.Tag1;
                champion.Tag2 = postChampion.Tag2;
                champion.Partype = postChampion.Partype;
                champion.Stats = stats;

                try
                {
                   db.SaveChanges();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception when executing save in PatchChampion: {0}", e.Message);
                }
                
            }
            else if (champion == null)
            {
                CreateChampion(postChampion);
            }
        }
    }
}