using cmes_webapi.ServiceHelper.Contracts;
using RestSharp;
using System.Threading.Tasks;

namespace cmes_webapi.ServiceHelper
{
    public class RestServiceClient : IRestServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="serviceUrl">
        /// localhost:8080/
        /// </param>
        /// <param name="resource">
        /// "/item/{id}"
        /// </param>
        /// <returns></returns>
        public async Task<R> SendGetAsync<T, R>(string serviceUrl, string resource)
        {
            var client = new RestClient(serviceUrl);
            var request = new RestRequest(resource, Method.Get);
            var response = await client.GetAsync<R>(request);

            return response;
        }


        public async Task<R> SendPostAsync<T,R>(string baseUrl, T data)
        {
            var client = new RestClient(baseUrl);

            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddBody(data);

            var response = await client.PostAsync<R>(request);

            return response;
        }
    }
}
