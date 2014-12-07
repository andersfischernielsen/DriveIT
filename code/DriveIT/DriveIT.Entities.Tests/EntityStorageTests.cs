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

        [TestFixtureSetUp]
        public void SetUp()
        {
            _toTest = new EntityStorage();
        }

        [Test]
        public async void CreateCarTest()
        {
            var mockSet = new Mock<DbSet<Car>>();
            var mockContext = new Mock<DriveITContext>();
            mockContext.Setup(m => m.Cars).Returns(mockSet.Object);

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
            mockContext.Object);

            mockSet.Verify(m => m.Add(It.IsAny<Car>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync()); 
        }

        [Test]
        public async void GetAllCarsTest()
        {
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

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IDbAsyncEnumerable<Car>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Car>(cars.GetEnumerator()));

            mockSet.As<IQueryable<Car>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Car>(cars.Provider)); 

            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(cars.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(cars.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(cars.GetEnumerator());

            var mockContext = new Mock<DriveITContext>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            var result = await _toTest.GetAllCars(mockContext.Object);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Ford", result[0].Make);
            Assert.AreEqual("Bentley", result[1].Make);
        }

        [Test]
        public async void DeleteCarTest()
        {
            var cars = new List<Car> 
            { 
                new Car
                {
                    Id = 1,
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
                    Id = 2,
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

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IDbAsyncEnumerable<Car>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Car>(cars.GetEnumerator()));

            mockSet.As<IQueryable<Car>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Car>(cars.Provider));

            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(cars.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(cars.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(cars.GetEnumerator());

            mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).Returns(mockSet.Object.FindAsync(1));

            var mockContext = new Mock<DriveITContext>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            await _toTest.DeleteCar(1, mockContext.Object);

            Assert.IsNull(mockContext.Object.Cars.FindAsync(1));
            mockSet.Verify(m => m.Remove(It.IsAny<Car>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async void UpdateCarTest()
        {
            var cars = new List<Car> 
            { 
                new Car
                {
                    Id = 1,
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
                    Id = 2,
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

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IDbAsyncEnumerable<Car>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Car>(cars.GetEnumerator()));

            mockSet.As<IQueryable<Car>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Car>(cars.Provider));

            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(cars.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(cars.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(cars.GetEnumerator());
            mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).Returns(Task.Run(() => cars.ElementAt(1)));

            var mockContext = new Mock<DriveITContext>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            await _toTest.UpdateCar(1, new Car {Color = "Turquoise" }, mockContext.Object);

            var result = await mockContext.Object.Cars.FirstOrDefaultAsync(x => x.Color == "Turquoise");
            Assert.AreEqual("Turquoise", result.Color);
            Assert.AreEqual(0, result.DistanceDriven);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
