using Kneat.Model;
using Kneat.Service.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kneat.Service.Repository.Implementation
{
    public class StarWarsRepository : IStarWarsRepository
    {
        private IStarWarsRepositoryHelper _starWarsRepositoryHelper;

        public StarWarsRepository(IStarWarsRepositoryHelper starWarsRepositoryHelper)
        {
            _starWarsRepositoryHelper = starWarsRepositoryHelper;
        }
        
        /// <summary>
        /// Method in char of retreive data from SWAPI
        /// </summary>
        /// <returns0>List of StarShips</returns>
        public async Task<IEnumerable<StarShip>> GetAllStarShips()
        {
            List<StarShip> result;
            int pageNumber = 1;
            try
            {
                string next = "";
                result = new List<StarShip>();
                do
                {
                    AllStarShipsResponse allStarShipsResponse = await _starWarsRepositoryHelper.GetAllStarShipsAsync(pageNumber.ToString());
                    result.AddRange(allStarShipsResponse.Result);
                    next = allStarShipsResponse.next;
                    pageNumber++;

                } while (!string.IsNullOrEmpty(next));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

       

    }
}
