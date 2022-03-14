using System.Threading.Tasks;

namespace cmes_webapi.ServiceHelper.Contracts
{
    public interface IRestServiceClient
    {
        public Task<R> SendGetAsync<T,R>(string serviceUrl, string resource);
        public Task<R> SendPostAsync<T,R>(string baseUrl, T data);
    }
}
