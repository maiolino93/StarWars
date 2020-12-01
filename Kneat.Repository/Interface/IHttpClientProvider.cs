using System.Net.Http;
using System.Threading.Tasks;

namespace Kneat.Service.Domain.Providers
{
    public interface IHttpClientProvider
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}