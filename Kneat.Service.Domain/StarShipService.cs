using Kneat.Model;
using Kneat.Service.Domain.Glosary;
using Kneat.Service.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kneat.Service.Domain
{
    public class StarShipService : IStarShipService
    {
        private IStarWarsRepository _starWarsRepository;
        static IEnumerable<StarShip> starShips;

        public StarShipService(IStarWarsRepository starWarsRepository)
        {
            _starWarsRepository = starWarsRepository;
        }

        public async Task<IEnumerable<StarShip>> GetAllStarShips()
        {
            starShips = await _starWarsRepository.GetAllStarShips();
            return starShips;
        }
        /// <summary>
        /// Method in charge of calculate how many hours does it take to travel given distance
        /// </summary>
        /// <param name="distance">input distance on MGLT</param>
        /// <param name="maxDistancePerHour">max distance per hour that each starship can travel</param>
        /// <returns>Amount of hours starships takes to travel given distance on hours</returns>
        public double CalculateHoursForGivenDistance(double distance, double maxDistancePerHour)
        {
            double result;
            result = distance / maxDistancePerHour;

            if (double.IsPositiveInfinity(result) || double.IsNegativeInfinity(result))
                result = 0;

            return result;
        }
        /// <summary>
        /// Method in charge of calculate how much cargo is needed to travel a distance
        /// </summary>
        /// <param name="cargoCapacity">Max cargo each starShip</param>
        /// <param name="durationOfConsumableOnHour">DUration of max cargo </param>
        /// <param name="timeForGivenDistance">Time starship takes to travel a given distance on hours</param>
        /// <returns>amount of cargo needed to travel a distance</returns>
        public double CargoNeededForFullDistance(double cargoCapacity, double durationOfConsumableOnHour, double timeForGivenDistance)
        {
            double result;
            result = (durationOfConsumableOnHour * cargoCapacity) / timeForGivenDistance;

            if (double.IsPositiveInfinity(result) || double.IsNegativeInfinity(result))
                result = 0;

            return result;
        }
        /// <summary>
        /// Method in charge of calculate how many stops are needed for a given cargo
        /// </summary>
        /// <param name="cargoNeededForTrip">Cargo needed for given distance</param>
        /// <param name="cargoCapacity">Max capacity on starShip</param>
        /// <returns>amount of stops needed for a given  cargo</returns>
        public double StopsForGivenCargo(double cargoNeededForTrip, double cargoCapacity)
        {
            double result;
            result = cargoNeededForTrip / cargoCapacity;

            if (double.IsPositiveInfinity(result) || double.IsNegativeInfinity(result))
                result = 0;

            return result;
        }
        /// <summary>
        /// Calculate how long it lasts consumable on hours
        /// </summary>
        /// <param name="consumables">Period that lasts consumables</param>
        /// <returns> Duration of consumable on hour</returns>
        public double GetDurationOfConsumableOnHours(string consumables)
        {
            double result;
            List<string> consumableSplitted = GetConsumable(consumables).ToList();
            int quantity = int.Parse(consumableSplitted[0].ToString());
            string period = consumableSplitted[1].ToString();
            double baseNumber = GetBaseNumberHours(period);

            result = quantity * baseNumber;

            return result;
        }
        /// <summary>
        /// Private method in chrge of know how many hours are in a specific period
        /// </summary>
        /// <param name="period">Period consumable last on each starship</param>
        /// <returns>Amount of hours on a period</returns>
        public double GetBaseNumberHours(string period)
        {
            switch (period)
            {
                case "year":
                case "years":
                    return Constant.HoursOnYear;
                case "month":
                case "months":
                    return Constant.HoursOnMonth;
                case "week":
                case "weeks":
                    return Constant.HoursOnWeek;
                case "day":
                case "days":
                    return Constant.HoursOnDay;
                default:
                    return Constant.HoursOnDay;
            }
        }
        /// <summary>
        /// Method in char split consumable in a list
        /// </summary>
        /// <param name="consumables">Consumable each starShip</param>
        /// <returns>List with 2 items, amount & period</returns>
        private IEnumerable<string> GetConsumable(string consumables)
        {
            List<string> result = new List<string>();
            var consumablesSplitted = consumables.Split(' ');
            result.Add(consumablesSplitted[0].ToString());
            result.Add(consumablesSplitted[1].ToString());

            return result;
        }
        /// <summary>
        /// Method in charge of run calculations
        /// </summary>
        /// <param name="distance">Given distance from UI</param>
        /// <returns>List of starShip with its own stops needed</returns>
        public IEnumerable<StarShipResponse> RunCalculation(double distance)
        {
            List<StarShipResponse> result = new List<StarShipResponse>();
            foreach (var item in starShips)
            {
                double cargo;
                double mglt;
                string stops = "";
                if (double.TryParse(item.CargoCapacity, out cargo) && double.TryParse(item.MGLT, out mglt))
                {
                    var durationOfConsumable = GetDurationOfConsumableOnHours(item.Consumable);
                    var hoursForDistance = CalculateHoursForGivenDistance(distance, double.Parse(item.MGLT));
                    var cargoNedeed = CargoNeededForFullDistance(double.Parse(item.CargoCapacity), hoursForDistance, durationOfConsumable);
                    var stopsNeeded = StopsForGivenCargo(cargoNedeed, double.Parse(item.CargoCapacity));
                    stops = Math.Round(stopsNeeded).ToString();
                }
                else
                {
                    stops = "Not possible to calculate.";
                }

                result.Add(new StarShipResponse()
                {
                    Name = item.Name,
                    StopsNeeded = stops
                }) ;
            }
            return result;

        }
    }
}
