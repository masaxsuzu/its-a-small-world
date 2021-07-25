using Microsoft.AspNetCore.Mvc.Testing;

using System.Linq;
using System.Threading.Tasks;
using Netsoft.SmallWorld.Web;
using Netsoft.SmallWorld.Api.Tests.Helpers;
using Xunit;

namespace Netsoft.SmallWorld.Api.Tests
{
    public class TestHubsEndpoint
    : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string _baseUrl;

        public TestHubsEndpoint(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _baseUrl = "/v1/hubs";
        }

        [Theory]
        [InlineData("ブラック・マジシャン", "光の創造神 ホルアクティ")]
        [InlineData("ブラック・マジシャン", "ブラック・マジシャン・ガール")]
        [InlineData("死の４つ星てんとう虫", "ブラック・マジシャン・ガール")]
        public async Task HubsCanBeFoundSomeHow(string from, string to)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var cards = await client.Get<DTOs.Card[]>($"/v1/cards");
            var response = await client.Get<DTOs.Card[]>($"{_baseUrl}/?from={from}&to={to}");

            var src = cards.Where(c => c.name == from).First();
            var dst = cards.Where(c => c.name == to).First();

            // Assert
            Assert.NotEmpty(
                response);

            foreach (var hub in response)
            {
                Assert.True(
                (
                    (hub.atk == src.atk && hub.def != src.def && hub.level != src.level && hub.attribute != src.attribute && hub.race != src.race) ||
                    (hub.atk != src.atk && hub.def == src.def && hub.level != src.level && hub.attribute != src.attribute && hub.race != src.race) ||
                    (hub.atk != src.atk && hub.def != src.def && hub.level == src.level && hub.attribute != src.attribute && hub.race != src.race) ||
                    (hub.atk != src.atk && hub.def != src.def && hub.level != src.level && hub.attribute == src.attribute && hub.race != src.race) ||
                    (hub.atk != src.atk && hub.def != src.def && hub.level != src.level && hub.attribute != src.attribute && hub.race == src.race)
                )
                &&
                (
                    (hub.atk == dst.atk && hub.def != dst.def && hub.level != dst.level && hub.attribute != dst.attribute && hub.race != dst.race) ||
                    (hub.atk != dst.atk && hub.def == dst.def && hub.level != dst.level && hub.attribute != dst.attribute && hub.race != dst.race) ||
                    (hub.atk != dst.atk && hub.def != dst.def && hub.level == dst.level && hub.attribute != dst.attribute && hub.race != dst.race) ||
                    (hub.atk != dst.atk && hub.def != dst.def && hub.level != dst.level && hub.attribute == dst.attribute && hub.race != dst.race) ||
                    (hub.atk != dst.atk && hub.def != dst.def && hub.level != dst.level && hub.attribute != dst.attribute && hub.race == dst.race)
                ));
            }
        }
        [Theory]
        [InlineData("4が半角数字になっている", "死の4つ星てんとう虫", "ブラック・マジシャン・ガール")]
        [InlineData("中点がぬけている", "死の４つ星てんとう虫", "ブラックマジシャン・ガール")]
        public async Task NonExistentCardShouldResultIn404Error(string _, string from, string to)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var cards = await client.Get<DTOs.Card[]>($"/v1/cards");
            var response = await client.GetAsync($"{_baseUrl}/?from={from}&to={to}");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
