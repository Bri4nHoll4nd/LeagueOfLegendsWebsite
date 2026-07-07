using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Models.V1;
using Microsoft.AspNetCore.Mvc;
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
    public class V1LeagueControllerTest
    {
        private V1LeagueController function;
        Mock<IHttpClientFactory> clientFactory;

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
                else if (request.RequestUri.ToString().Contains("lol/league/v4/entries/by-summoner") && request.RequestUri.ToString().Contains("test summoner id 1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new List<V1LeagueDto>() { new V1LeagueDto() { Wins = 5, FreshBlood = true, Losses = 2 } }), Encoding.UTF8, "application/json");
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
            function = new V1LeagueController(clientFactory.Object);
        }

        [Test]
        public async Task GetLeague_PassValidSummonerName_MustReturnsListOfLeagues()
        {
            // Arrange
            string summonerName = "test summoner name 1";

            // Act
            var response = await function.GetLeague(summonerName);

            // Assert
            Assert.IsTrue(response is OkObjectResult);
            var responseData = (List<V1LeagueDto>)((OkObjectResult)response).Value;
            Assert.IsNotNull(responseData);
            Assert.AreEqual(true, responseData.FirstOrDefault().FreshBlood);
            Assert.AreEqual(5, responseData.FirstOrDefault().Wins);
            Assert.AreEqual(2, responseData.FirstOrDefault().Losses);
        }

        [Test]
        public async Task GetLeague_PassUnvalidSummonerName_MustReturnBadRequestObjectResult()
        {
            // Arrange
            string summonerName = "test summoner name 2";

            // Act
            var response = await function.GetLeague(summonerName);

            // Assert
            Assert.IsTrue(response is BadRequestObjectResult);
        }
    }
}
