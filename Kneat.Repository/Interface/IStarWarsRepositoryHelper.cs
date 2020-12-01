using Kneat.Model;
using System.Threading.Tasks;

namespace Kneat.Service.Repository.Implementation
{
    public interface IStarWarsRepositoryHelper
    {
        Task<AllStarShipsResponse> GetAllStarShipsAsync(string page);
    }
}