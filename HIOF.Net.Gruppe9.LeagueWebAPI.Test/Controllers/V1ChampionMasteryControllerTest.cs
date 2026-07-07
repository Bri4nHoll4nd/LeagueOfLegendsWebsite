using HIOF.Net.Gruppe9.LeagueWebAPI.BackgroundJob.Models.V1;
using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Text.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Test.Controllers
{
    [TestFixture]
    public class V1ChampionMasteryControllerTest
    {
        private V1ChampionMasteryController function;
        Mock<IHttpClientFactory> clientFactory;
        Mock<IMemoryCache> _cache;

        [SetUp]
        public void InitializeTest()
        {
            var clientHandler = new HttpRequestMock((request, token) =>
            {
                HttpResponseMessage response = null;
                if (request.RequestUri.ToString().Contains("lol/summoner/v4/summoners/by-name/") && request.RequestUri.ToString().Contains("test summoner name 1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new V1SummonerDto() { Id = "test summoner id 1" }), Encoding.UTF8, "application/json");
                }
                else if (request.RequestUri.ToString().Contains("lol/champion-mastery/") && request.RequestUri.ToString().Contains("test summoner id 1") && request.RequestUri.ToString().Contains("/1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new V1ChampionMasteryDto() { ChampionId = 1, ChampionLevel = 3, ChampionPoints = 15 }), Encoding.UTF8, "application/json");
                }
                else if (request.RequestUri.ToString().Contains("lol/champion-mastery/") && request.RequestUri.ToString().Contains("test summoner id 1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new List<V1ChampionMasteryDto>() { new V1ChampionMasteryDto() { ChampionId = 1, ChampionLevel = 3, ChampionPoints = 15 } }), Encoding.UTF8, "application/json");
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
            _cache = new Mock<IMemoryCache>();
            function = new V1ChampionMasteryController(clientFactory.Object, _cache.Object);
        }

        [Test]
        public async Task GetChampionMastery_PassChampionIdAndSummonerName_MustReturnsValidChamptionObject()
        {
            string summonerName = "test summoner name 1";
            int championId = 1;

            var response = await function.GetChampionMastery(summonerName, championId, false);

            Assert.IsTrue(response is OkObjectResult);
            var responseData = (V1ChampionMasteryDto)((OkObjectResult)response).Value;
            Assert.IsNotNull(responseData);
            Assert.AreEqual(1, responseData.ChampionId);
            Assert.AreEqual(3, responseData.ChampionLevel);
            Assert.AreEqual(15, responseData.ChampionPoints);
        }

        [Test]
        public async Task GetChampionMasteryList_PassValidSummonerName_MustReturnsChamptionMasteryList()
        {
            // Arrange
            string summonerName = "test summoner name 1";

            // Act
            var response = await function.GetChampionMasteryList(summonerName);

            // Assert
            Assert.IsTrue(response is OkObjectResult);
            var responseData = (List<V1ChampionMasteryDto>)((OkObjectResult)response).Value;
            Assert.IsNotNull(responseData);
            Assert.AreEqual(1, responseData.FirstOrDefault().ChampionId);
            Assert.AreEqual(3, responseData.FirstOrDefault().ChampionLevel);
            Assert.AreEqual(15, responseData.FirstOrDefault().ChampionPoints);
        }

        [Test]
        public async Task GetChampionMastery_PassUnvalidChampionIdAndSummonerName_MustReturnBadRequestObjectResult()
        {
            // Arrange
            string summonerName = "test summoner name 2";
            int championId = 2;

            // Act
            var response = await function.GetChampionMastery(summonerName, championId);

            // Assert
            Assert.IsTrue(response is BadRequestObjectResult);
        }
    }
}
