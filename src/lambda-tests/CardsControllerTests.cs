using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using Newtonsoft.Json;

using Netsoft.SmallWorld.Lambda;


namespace Netsoft.SmallWorld.Lambda.Tests
{
    public class CardsControllerTests
    {
        [Fact]
        public async Task TestGetCards()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/CardsController-GetCards.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(200, response.StatusCode);
            //Assert.Equal("[\"value1\",\"value2\"]", response.Body);
            Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
            Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        }
        [Fact]
        public async Task TestGetHubs()
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/CardsController-GetHubs.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(200, response.StatusCode);
            //Assert.Equal("[\"value1\",\"value2\"]", response.Body);
            Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
            Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        }
    }
}
