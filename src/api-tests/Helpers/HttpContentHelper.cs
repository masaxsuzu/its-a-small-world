using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Netsoft.SmallWorld.Api.Tests.Helpers
{
    public static class HttpContentHelper
    {
        public static HttpContent ToHttpContent<Req>(this Req request)
        {
            var json = request.ToJson();
            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
        public static HttpContent WithHeaders(this HttpContent content, string name, params string[] values)
        {
            content.Headers.Add(name, values);
            return content;
        }
    }
}
