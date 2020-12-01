using Kneat.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kneat.Service.Domain
{
    public interface IStarShipService
    {
        Task<IEnumerable<StarShip>> GetAllStarShips();
        double CalculateHoursForGivenDistance(double distance, double maxDistancePerHour);
        double CargoNeededForFullDistance(double cargoCapacity, double durationOfConsumableOnHour, double timeForGivenDistance);
        double GetDurationOfConsumableOnHours(string consumables);
        double StopsForGivenCargo(double cargoNeededForTrip, double cargoCapacity);
        IEnumerable<StarShipResponse> RunCalculation(double distance);
    }
}