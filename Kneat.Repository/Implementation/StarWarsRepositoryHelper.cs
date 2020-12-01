using Kneat.Model;
using Kneat.Service.Domain.Providers;
using Kneat.Service.Repository.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kneat.Service.Repository.Implementation
{
    public class StarWarsRepositoryHelper : EndpointGlossary, IStarWarsRepositoryHelper
    {

        private readonly IHttpClientProvider _httpClientProvider;

        public StarWarsRepositoryHelper(IHttpClientProvider httpClientProvider)
        {
            _httpClientProvider = httpClientProvider;
        }
        /// <summary>
        /// Method in char of send request to SWAPI
        /// </summary>
        /// <param name="page">Number of page of SWAPI</param>
        /// <returns>Response of SWAPI </returns>
        public async Task<AllStarShipsResponse> GetAllStarShipsAsync(string page)
        {
            AllStarShipsResponse result;
            try
            {
                HttpResponseMessage response = await _httpClientProvider.GetAsync($"{GetAllStarShipsUrlByPage}{page}");
                var stringResponse = await response.Content.ReadAsStringAsync();
                var parsedResponse = JsonConvert.DeserializeObject<AllStarShipsResponse>(stringResponse);
                result = parsedResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
