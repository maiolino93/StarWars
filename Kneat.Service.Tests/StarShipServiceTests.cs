using Kneat.Model;
using Kneat.Service.Domain;
using Kneat.Service.Domain.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kneat.Service.Tests
{
    class StarShipServiceTests
    {
        private Mock<IStarWarsRepository> _starWarsRepository;
        private StarShipService starShipService;

        [SetUp]
        public void Setup()
        {
            _starWarsRepository = new Mock<IStarWarsRepository>();
            starShipService = new StarShipService(_starWarsRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _starWarsRepository = null;
            starShipService = null;
        }

        [Test]
        [Description("It will check if the type of list returned by the enpoint is List <StarShip>")]
        public async Task ShouldReturnStarShipListObject()
        {
            //Arrage
            IEnumerable<StarShip> starShipList = new List<StarShip>();
            _starWarsRepository.Setup(x => x.GetAllStarShips()).Returns(Task.FromResult(starShipList));
            //Act
            var result = await starShipService.GetAllStarShips();
            //Assert
            Assert.IsInstanceOf<IEnumerable<StarShip>>(result);
        }

        [Test]
        [TestCase(1,1)]
        [Description("It will check if the calculation to get number of hours is ok")]
        public void ShouldReturnNumberOfHoursItTakesToTravelAGivenDistance(double distance, double maxDistancePerHour)
        {
            //Arrage
            double hours = 1;
            //Act
            var result = starShipService.CalculateHoursForGivenDistance(distance, maxDistancePerHour);
            //Assert
            Assert.AreEqual(hours, result);
        }

        [Test]
        [TestCase(1,0)]
        [Description("It will check if the calculation of number of hours is Infinitive+ or Infinitive-, should return 0")]
        public void ShouldReturnCeroWhenDivisionByCeroOnCalculationOfNumberOfHourForGivenDistance(double distance, double maxDistancePerHour)
        {
            //Arrage
            double hours = 0;
            //Act
            var result = starShipService.CalculateHoursForGivenDistance(distance, maxDistancePerHour);
            //Assert
            Assert.AreEqual(hours, result);
        }

        [Test]
        [TestCase(1, 1, 1)]
        [Description("It will check if the calculation of Cargo needed for full distance is Ok.")]
        public void ShouldReturnCalculationOfCargoForFullDistance(double cargoCapacity, double durationOfConsumableOnHour, double timeForGivenDistance)
        {
            //Arrage
            double cargo = 1;
            //Act
            var result = starShipService.CargoNeededForFullDistance(cargoCapacity, durationOfConsumableOnHour, timeForGivenDistance);
            //Assert
            Assert.AreEqual(cargo, result);
        }

        [Test]
        [TestCase(1,1,0)]
        [Description("It will check if the calculation of Cargo needed for full distance is Infinitive+ or Infinitive-, should return 0")]
        public void ShouldReturnCeroWhenDivisionByCeroOnCalculationOfCargoForFullDistance(double cargoCapacity, double durationOfConsumableOnHour, double timeForGivenDistance)
        {
            //Arrage
            double cargo = 0;
            //Act
            var result = starShipService.CargoNeededForFullDistance(cargoCapacity, durationOfConsumableOnHour,timeForGivenDistance);
            //Assert
            Assert.AreEqual(cargo, result);
        }


        [Test]
        [TestCase(1, 0)]
        [Description("It will check if the calculation of cargo needed for a given distance is Infinitive+ or Infinitive-, should return 0")]
        public void ShouldReturnCeroWhenDivisionByCeroOnCalculationOfStopsForGivenCargo(double cargoNeededForTrip, double cargoCapacity)
        {
            //Arrage
            double stops = 0;
            //Act
            var result = starShipService.StopsForGivenCargo(cargoNeededForTrip, cargoCapacity);
            //Assert
            Assert.AreEqual(stops, result);
        }

        [Test]
        [TestCase(1, 1)]
        [Description("It will check if the calculation of cargo needed for a given distance is Ok")]
        public void ShouldReturnNumberOfStopsNeededForGivenCargo(double cargoNeededForTrip, double cargoCapacity)
        {
            //Arrage
            double stops = 1;
            //Act
            var result = starShipService.StopsForGivenCargo(cargoNeededForTrip, cargoCapacity);
            //Assert
            Assert.AreEqual(stops, result);
        }

        [Test]
        [TestCase("2 years")]
        [Description("It will provide number of hours within 2years")]
        public void ShouldReturnDurationOfConsumableOnHoursWithinYears(string consumables)
        {
            //Arrage
            double result = 17520;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

        [Test]
        [TestCase("2 years")]
        [Description("It will provide number of hours within 2years")]
        public void ShouldReturnCeroWhenDivisionByCeroOnCalculationOfConsumableOnHoursWithinYears(string consumables)
        {
            //Arrage
            double result = 17520;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

        [Test]
        [TestCase("2 months")]
        [Description("It will provide number of hours within  2 month")]
        public void ShouldReturnDurationOfConsumableOnHoursWithinMonths(string consumables)
        {
            //Arrage
            double result = 1460;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

        [Test]
        [TestCase("2 weeks")]
        [Description("It will provide number of hours within  2 weeks")]
        public void ShouldReturnDurationOfConsumableOnHoursWithinWeeks(string consumables)
        {
            //Arrage
            double result = 336;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

        [Test]
        [TestCase("2 days")]
        [Description("It will provide number of hours within  2 days")]
        public void ShouldReturnDurationOfConsumableOnHoursWithinDays(string consumables)
        {
            //Arrage
            double result = 48;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

        [Test]
        [TestCase("2 da")]
        [Description("It will provide number of hours within  1 day")]
        public void ShouldReturnDurationOfConsumableOnHoursWithinADay(string consumables)
        {
            //Arrage
            double result = 48;
            //Act
            var r = starShipService.GetDurationOfConsumableOnHours(consumables);
            //Assert
            Assert.AreEqual(result, r);
        }

    }


}
