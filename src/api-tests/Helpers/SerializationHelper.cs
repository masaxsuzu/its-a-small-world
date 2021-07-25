using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Net.Http;
using Xunit;

namespace Netsoft.SmallWorld.Api.Tests.Helpers
{
    public static class SerializationHelper
    {
        public static Res FromJson<Res>(this string json)
        {
            return JsonSerializer.Deserialize<Res>(
                json,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }

        public static string ToJson<Req>(this Req request)
        {
            return JsonSerializer.Serialize<Req>(
                request,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }
    }
}
