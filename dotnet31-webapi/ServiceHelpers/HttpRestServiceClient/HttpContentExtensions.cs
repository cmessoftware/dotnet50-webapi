using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;
using System.Threading.Tasks;

namespace cmes_webapi.Common.Services
{
    public static class HttpContentExtensions
    {

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();

            var format = "dd/MM/yyyy"; // datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            T value = JsonConvert.DeserializeObject<T>(json, dateTimeConverter);
            return value;
        }

        //public static IEnumerable<R> DoSomething<T, R>(IEnumerable<T> things, Func<T, R> map)
        //{
        //    foreach (var t in things)
        //    {
        //        yield return map(t);
        //    }
        //}

    }
}
