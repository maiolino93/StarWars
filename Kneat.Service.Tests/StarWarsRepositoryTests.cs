using Kneat.Model;
using Kneat.Service.Repository.Implementation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kneat.Service.Tests
{
    class StarWarsRepositoryTests
    {
        private Mock<IStarWarsRepositoryHelper> _starWarsRepositoryHelper;

        [SetUp]
        public void Setup()
        {
            _starWarsRepositoryHelper = new Mock<IStarWarsRepositoryHelper>();
        }

        [TearDown]
        public void TearDown()
        {
            _starWarsRepositoryHelper = null;
        }

        [Test]
        [Description("It will check if exception is thrown when error ocurrs")]
        public void ShouldReturnExceptionWhenErrorOcurrs()
        {
            //Arrage
            IEnumerable<StarShip> starShip = new List<StarShip>();
            AllStarShipsResponse result = new AllStarShipsResponse() { next = "", Result = starShip };
            _starWarsRepositoryHelper.Setup(x => x.GetAllStarShipsAsync("1")).Throws(new Exception());
            StarWarsRepository repository = new StarWarsRepository(_starWarsRepositoryHelper.Object);

            //Act
            AsyncTestDelegate act = () => repository.GetAllStarShips();

            //Assert
            Assert.That(act, Throws.TypeOf<Exception>());
        }


        [Test]
        [Description("It will check if the type of list returned by the enpoint is List <StarShip>")]
        public async Task ShouldReturnStarShipListObject()
        {
            //Arrage
            IEnumerable<StarShip> starShip = new List<StarShip>();
            
            AllStarShipsResponse result = new AllStarShipsResponse() { next = "", Result = starShip };
            _starWarsRepositoryHelper.Setup(x => x.GetAllStarShipsAsync("1")).Returns(Task.FromResult(result));
            StarWarsRepository repository = new StarWarsRepository(_starWarsRepositoryHelper.Object);

            //Act
            var starShips = await repository.GetAllStarShips();

            //Assert
            Assert.IsInstanceOf<IEnumerable<StarShip>>(starShips);
        }


    }
}

