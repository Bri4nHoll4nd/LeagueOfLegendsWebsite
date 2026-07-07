using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers;
using HIOF.Net.Gruppe9.LeagueWebAPI.Controllers.V1;
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
    public class V1PlayersControllerTest
    {
        private V1PlayersController function;
        Mock<IHttpClientFactory> clientFactory;

        [SetUp]
        public void InitializeTest()
        {
            var clientHandler = new HttpRequestMock((request, token) =>
            {
                HttpResponseMessage response = null;
                if (request.RequestUri.ToString().Contains("lol/clash/v1/players/by-summoner") && request.RequestUri.ToString().Contains("test summoner id 1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new List<V1PlayersDto>() { new V1PlayersDto() { Role = "Striker" } }), Encoding.UTF8, "application/json");
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
            function = new V1PlayersController(clientFactory.Object);
        }

        [Test]
        public async Task GetPlayers_PassValidSummonerId_MustReturnValidPlayerObject()
        {
            // Arrange
            string summonerId = "test summoner id 1";

            // Act
            var response = await function.GetPlayers(summonerId);

            // Arrange
            Assert.IsTrue(response is OkObjectResult);
            var responseData = (List<V1PlayersDto>)((OkObjectResult)response).Value;
            Assert.IsNotNull(responseData);
            Assert.AreEqual("Striker", responseData.FirstOrDefault().Role);
        }

        [Test]
        public async Task GetPlayers_PassUnvalidSummonerId_MustReturnBadRequestObjectResult()
        {
            // Arrange
            string summonerName = "test summoner id 2";

            // Act
            var response = await function.GetPlayers(summonerName);

            // Arrange
            Assert.IsTrue(response is BadRequestObjectResult);
        }
    }
}
