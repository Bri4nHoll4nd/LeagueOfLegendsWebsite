using HIOF.Net.Gruppe9.LeaugeVersionDB.Data;
using HIOF.Net.Gruppe9.LeaugeVersionDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIOF.Net.Gruppe9.LeaugeVersionDB.Controllers.V1
{
    [ApiController]
    [Route("V1/Version")]
    public class V1VersionController : ControllerBase
    {
        private readonly ILogger<V1VersionController> _logger;

        public V1VersionController(ILogger<V1VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Get")]
        public V1Result<V1Version>? GetVersion()
        {
            _logger.LogDebug("GetVersion was called");

            var dbContext = new VersionDBContext();

            try
            {
                var responseVersions = dbContext.Versions.First();

                var result = new V1Result<V1Version>(new V1Version
                {
                    Name = responseVersions.Name
                });

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing save in GetVersion: {0}", e.Message);
                var failedResult = new V1Result<V1Version>(null);
                failedResult.Errors = new List<string>
                {
                    "Couldn't get version, something went wrong"
                };
                return failedResult;
            }
        }

        [HttpPost("Create")]
        public async Task<V1Result<V1Version>?> CreateVersion(string name)
        {
            _logger.LogDebug("CreateVersion was called");

            var dbContext = new VersionDBContext();

            try
            {
                var version = new Data.Version
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };

                dbContext.Versions.Add(version);
                await dbContext.SaveChangesAsync();

                var result = new V1Result<V1Version>(new V1Version
                {
                    Id = version.Id,
                    Name = name
                });

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing save in CreateVersion: {0}", e.Message);
                var failedResult = new V1Result<V1Version>(null);
                failedResult.Errors = new List<string>
                {
                    "Couldn't create version, something went wrong"
                };
                return failedResult;
            }
        }

        [HttpPatch("Update/{name}")]
        public void PatchVersion(string name)
        {
            _logger.LogDebug("PatchVersion was called");

            var db = new VersionDBContext();

            try
            {
                var version = db.Versions.First();

                if (version != null) 
                {
                    version.Name = name;

                    db.SaveChanges();
                } else if (version == null)
                {
                    CreateVersion(name);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception when executing save in PatchVersion: {0}", e.Message);
            }

            
        }
    }
}