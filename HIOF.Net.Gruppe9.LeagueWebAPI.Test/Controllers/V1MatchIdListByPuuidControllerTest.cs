using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Test.Controllers
{
    [TestFixture]
    public class V1MatchIdListByPuuidControllerTest
    {
        private V1MatchIdListByPuuidController function;
        Mock<IHttpClientFactory> clientFactory;

        [SetUp]
        public void InitializeTest()
        {
            var clientHandler = new HttpRequestMock((request, token) =>
            {
                HttpResponseMessage response = null;
                if (request.RequestUri.ToString().Contains("lol/match/v5/matches/by-puuid/") &&
                    request.RequestUri.ToString().Contains("JzwEKanEu6FkV_fRlsi9UsNOWdTKzxsG79Y_6d8Lkwx3Nn3EZkCdqtODsVNZhfUkRpDuCRtEEa1A5g")) // < -- puuid
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    List<string> matchIdStrings = new List<string> { "EUW1_6325997112" };
                    response.Content = new StringContent(JsonSerializer.Serialize(matchIdStrings), Encoding.UTF8, "application/json");
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest);
                    response.Content = null;
                }
                return Task.FromResult(response);
            });

            var client = new HttpClient(clientHandler);
            client.BaseAddress = new System.Uri("https://euw1.api.riotgames.com");
            clientFactory = new Moq.Mock<System.Net.Http.IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient("httpclient")).Returns(client);
            function = new V1MatchIdListByPuuidController(clientFactory.Object);
        }

        [Test]
        public async Task GetMatchIdListByPuuid_PassValidPuuid_MustReturnsListOfMatchObjects()
        {
            // Arrange
            string puuid = "JzwEKanEu6FkV_fRlsi9UsNOWdTKzxsG79Y_6d8Lkwx3Nn3EZkCdqtODsVNZhfUkRpDuCRtEEa1A5g";

            // Act
            var response = await function.GetMatchIdListByPuuid(puuid);
            Console.WriteLine(response);

            // Assert
            Assert.IsTrue(response is OkObjectResult);
            var okResult = response as OkObjectResult;
            Assert.IsNotNull(okResult);
            var responseData = okResult.Value as List<V1MatchIdByPuuidDto>;
            Assert.IsNotNull(responseData);
            Assert.AreEqual("EUW1_6325997112", responseData[0].MatchId);
        }

        [Test]
        public async Task GetMatchIdListByPuuid_PassUnvalidPuuid_MustReturnBadRequestObjectResult()
        {
            // Arrange
            string summonerName = "puuid 2";

            // Act
            var response = await function.GetMatchIdListByPuuid(summonerName);

            // Assert
            Assert.IsTrue(response is BadRequestObjectResult);
        }
    }
}
