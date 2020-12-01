using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kneat.Model;
using Kneat.Service.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kneat.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarShipsController : ControllerBase
    {
        private IStarShipService _starShipService;

        public StarShipsController(IStarShipService starShipService)
        {
            _starShipService = starShipService;
        }
     
        /// <summary>
        /// Endpoint in charge of retreive all starships
        /// </summary>
        /// <returns>number of starships found</returns>
        [HttpGet("GetAll")]
        public async Task<string> GetAllStarShips()
        {
            string result;
            try
            {
                var list = await _starShipService.GetAllStarShips();
                result = list.Count().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred.");
                Console.WriteLine($"Error:{ex.Message}");
                result = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// Method in charge of run calculation from given input
        /// </summary>
        /// <param name="distance">Distance on MGLT</param>
        /// <returns>List of starShips with numbers of stops needed to travel given distance</returns>
        [HttpGet("RunCalculations")]
        public IEnumerable<StarShipResponse> RunCalculation(int distance)
        {
            IEnumerable<StarShipResponse> result;
            try
            {
                Console.WriteLine("here");
                result = _starShipService.RunCalculation(distance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred.");
                Console.WriteLine($"Error:{ex.Message}");
                throw ex;
            }

            return result;
        }


    }
}
