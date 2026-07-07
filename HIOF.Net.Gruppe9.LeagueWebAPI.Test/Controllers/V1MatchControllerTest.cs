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
    public class V1MatchControllerTest
    {
        private V1MatchController function;
        Mock<IHttpClientFactory> clientFactory;

        [SetUp]
        public void InitializeTest()
        {
            var clientHandler = new HttpRequestMock((request, token) =>
            {
                HttpResponseMessage response = null;
                if (request.RequestUri.ToString().Contains("lol/match/v5/matches") && request.RequestUri.ToString().Contains("test match id 1"))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(new V1MatchDto() { Info = new InfoDto() { Gameld = 1 } }), Encoding.UTF8, "application/json");
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
            function = new V1MatchController(clientFactory.Object);
        }

        [Test]
        public async Task GetMatch_PassValidMatchId_MustReturnsMatchObject()
        {
            // Arrange
            string matchId = "test match id 1";

            // Act
            var response = await function.GetMatch(matchId);

            // Assert
            Assert.IsTrue(response is OkObjectResult);
            var responseData = (V1MatchDto)((OkObjectResult)response).Value;
            Assert.IsNotNull(responseData);
            Assert.AreEqual(1, responseData.Info.Gameld);
        }

        [Test]
        public async Task GetMatch_PassUnvalidMatchId_MustReturnBadRequestObjectResult()
        {
            // Arrange
            string matchId = "test match id 2";

            // Act
            var response = await function.GetMatch(matchId);

            // Assert
            Assert.IsTrue(response is BadRequestObjectResult);
        }
    }
}
