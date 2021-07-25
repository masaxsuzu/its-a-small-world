using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Netsoft.SmallWorld.Api.Tests.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<Res> Get<Res>(this HttpClient client, string url)
        {
            var message = await client.GetAsync(url);
            var ret = await message.Content.ReadAsStringAsync();
            return ret.ToJson().Replace("\"", "").Replace("\\u0022", "\"").Debug().FromJson<Res>();
        }

        public static async Task<Res> Post<Res>(this HttpClient client, string url, HttpContent content)
        {
            var message = await client.PostAsync(url, content);
            var ret = await message.Content.ReadAsStringAsync();
            return ret.ToJson().Replace("\"", "").Replace("\\u0022", "\"").Debug().FromJson<Res>();
        }

        public static async Task<Res> Put<Res>(this HttpClient client, string url, HttpContent content)
        {
            var message = await client.PutAsync(url, content);
            var ret = await message.Content.ReadAsStringAsync();
            return ret.ToJson().Replace("\"", "").Replace("\\u0022", "\"").Debug().FromJson<Res>();
        }

        public static T Debug<T>(this T value, bool on = false)
        {
            if (on)
            {
                Console.WriteLine(value.ToString());
            }
            return value;
        }
    }
}
