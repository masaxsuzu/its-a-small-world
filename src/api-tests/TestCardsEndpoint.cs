using Microsoft.AspNetCore.Mvc.Testing;

using System.Linq;
using System.Threading.Tasks;
using Netsoft.SmallWorld.Lambda;
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
            var response = await client.Get<DTOs.CardInfoList>(_url);

            // Assert
            Assert.NotEmpty(
                response.data);
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
            var response = await client.Get<DTOs.CardInfoList>(_url);
            var count = response.data.Where(c => c.name == cardName).Count();

            // Assert
            Assert.True(
                count > 1, $"count is {count}, but it must be more than 1.");
        }
        [Theory]
        [InlineData("オシリスの天空竜","https://storage.googleapis.com/ygoprodeck.com/pics/10000020.jpg")]
        [InlineData("オベリスクの巨神兵","https://storage.googleapis.com/ygoprodeck.com/pics/10000000.jpg")]
        [InlineData("ラーの翼神竜","https://storage.googleapis.com/ygoprodeck.com/pics/10000010.jpg")]
        public async Task CardImageUrlAreIncluded(string cardName, string imageurl)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.Get<DTOs.CardInfoList>(_url);
            var card = response.data.Where(c => c.name == cardName).FirstOrDefault();

            // Assert
            Assert.Equal(
                imageurl, card.card_images[0].image_url);
        }
        [Theory]
        [InlineData("オシリスの天空竜","https://storage.googleapis.com/ygoprodeck.com/pics_small/10000020.jpg")]
        [InlineData("オベリスクの巨神兵","https://storage.googleapis.com/ygoprodeck.com/pics_small/10000000.jpg")]
        [InlineData("ラーの翼神竜","https://storage.googleapis.com/ygoprodeck.com/pics_small/10000010.jpg")]
        public async Task CardSmallImageUrlAreIncluded(string cardName, string imageurl)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.Get<DTOs.CardInfoList>(_url);
            var card = response.data.Where(c => c.name == cardName).FirstOrDefault();

            // Assert
            Assert.Equal(
                imageurl, card.card_images[0].image_url_small);
        }
    }
}
