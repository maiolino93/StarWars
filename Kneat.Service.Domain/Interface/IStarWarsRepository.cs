using Kneat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kneat.Service.Domain.Interface
{
    public interface IStarWarsRepository
    {
        Task<IEnumerable<StarShip>> GetAllStarShips();
    }
}
