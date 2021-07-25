using Microsoft.AspNetCore.Mvc.Testing;

using System.Linq;
using System.Threading.Tasks;
using Netsoft.SmallWorld.Api.Tests.Helpers;
using Xunit;

namespace Netsoft.SmallWorld.Api.Tests
{
    public class TestCardsEndpoint
    : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string _url;

        public TestCardsEndpoint(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _url = "/v1/cards";
        }

        [Fact]
        public async Task CardsAreNotEmpty()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.Get<DTOs.Card[]>(_url);

            // Assert
            Assert.NotEmpty(
                response);
        }

        [Theory]
        [InlineData("ブラック・マジシャン")]
        [InlineData("オシリスの天空竜")]
        [InlineData("オベリスクの巨神兵")]
        [InlineData("ラーの翼神竜")]
        public async Task CardsWithSameNamesAreIncluded(string cardName)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.Get<DTOs.Card[]>(_url);
            var count = response.Where(c => c.name == cardName).Count();

            // Assert
            Assert.True(
                count > 1, $"count is {count}, but it must be more than 1.");
        }
    }
}
