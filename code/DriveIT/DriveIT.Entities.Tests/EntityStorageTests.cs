using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.EntityFramework;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DriveIT.Entities.Tests
{
    [TestFixture]
    public class EntityStorageTests
    {
        private IPersistentStorage _toTest;
        private Mock<DbSet<Car>> _mockSet;
        private Mock<DriveITContext> _mockContext;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _toTest = new EntityStorage();

            var cars = new List<Car> 
            { 
                new Car
                {
                    Color = "Green",
                    Created = DateTime.Now,
                    DistanceDriven = 20,
                    Drive = "FWD",
                    Fuel = "Gas",
                    Make = "Ford",
                    Mileage = 20.5f,
                    Model = "TT",
                    Price = 1000,
                    Sold = false,
                    Transmission = "Manual",
                    Year = 2001
                }, 
                new Car
                {
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 20,
                    Drive = "FWD",
                    Fuel = "Gas",
                    Make = "Bentley",
                    Mileage = 20.5f,
                    Model = "Continental GT",
                    Price = 20000,
                    Sold = true,
                    Transmission = "Manual",
                    Year = 2001
                }, 
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Car>>();
            _mockSet.As<IDbAsyncEnumerable<Car>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Car>(cars.GetEnumerator()));

            _mockSet.As<IQueryable<Car>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Car>(cars.Provider));

            _mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(cars.Expression);
            _mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(cars.ElementType);
            _mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(cars.GetEnumerator());

            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).Returns(_mockSet.Object.FindAsync(1));

            _mockContext = new Mock<DriveITContext>();
            _mockContext.Setup(c => c.Cars).Returns(_mockSet.Object);
        }

        [Test]
        public async void CreateCarTest()
        {
            await _toTest.CreateCar(new Car
            {
                Color = "Green",
                Created = DateTime.Now,
                DistanceDriven = 20,
                Drive = "FWD",
                Fuel = "Gas",
                Make = "Ford",
                Mileage = 20.5f,
                Model = "TT",
                Price = 2000,
                Sold = false,
                Transmission = "Manual",
                Year = 2001
            },
            _mockContext.Object);

            _mockSet.Verify(m => m.Add(It.IsAny<Car>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync());
        }

        [Test]
        public async void GetAllCarsTest()
        {
            var result = await _toTest.GetAllCars(_mockContext.Object);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Ford", result[0].Make);
            Assert.AreEqual("Bentley", result[1].Make);
        }

        [Test]
        public async void DeleteCarTest()
        {
            await _toTest.DeleteCar(1, _mockContext.Object);

            Assert.IsNull(_mockContext.Object.Cars.FindAsync(1));
            _mockSet.Verify(m => m.Remove(It.IsAny<Car>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async void UpdateCarTest()
        {
            await _toTest.UpdateCar(1, new Car {Color = "Turquoise" }, _mockContext.Object);

            var result = await _mockContext.Object.Cars.FirstOrDefaultAsync(x => x.Color == "Turquoise");
            Assert.AreEqual("Turquoise", result.Color);
            Assert.AreEqual(0, result.DistanceDriven);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
