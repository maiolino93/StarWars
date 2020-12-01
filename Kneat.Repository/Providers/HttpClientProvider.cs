using System.Net.Http;
using System.Threading.Tasks;

namespace Kneat.Service.Domain.Providers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly HttpClient _httpClient;
        public HttpClientProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var a = _httpClient.GetAsync(requestUri);
            return a;
        }
    }
}
