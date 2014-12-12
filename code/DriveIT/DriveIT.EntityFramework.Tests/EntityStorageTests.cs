using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using DriveIT.EntityFramework;
using DriveIT.EntityFramework.Entities;
using DriveIT.Models;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DriveIT.Entities.Tests
{
    [TestFixture]
    public class EntityStorageTests
    {
        private IPersistentStorage _toTest;
        private FakeDbSet<Car> _carMockSet;
        private FakeDbSet<ImagePath> _imagePathMockSet;
        private Mock<DriveITContext> _mockContext;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _toTest = new EntityStorage();

            var imagePaths = new List<ImagePath> {new ImagePath {CarId = 1}, new ImagePath {CarId = 2}};

            var cars = new List<Car> 
            { 
                new Car
                {
                    Color = "Green",
                    Created = DateTime.Now,
                    DistanceDriven = 20,
                    Drive = "FWD",
                    Fuel = FuelType.Gasoline,
                    Make = "Ford",
                    Mileage = 20.5f,
                    Model = "TT",
                    Price = 1000,
                    Sold = false,
                    Transmission = "Manual",
                    Year = 2001, 
                    ImagePaths = imagePaths
                }, 
                new Car
                {
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 20,
                    Drive = "FWD",
                    Fuel = FuelType.Gasoline,
                    Make = "Bentley",
                    Mileage = 20.5f,
                    Model = "Continental GT",
                    Price = 20000,
                    Sold = true,
                    Transmission = "Manual",
                    Year = 2001,
                    ImagePaths = imagePaths
                }, 
            }.AsQueryable();

            var imagePath = imagePaths.ElementAt(0);
            cars.ElementAt(0).ImagePaths.Add(imagePath);
            cars.ElementAt(1).ImagePaths.Add(imagePath);

            _carMockSet = new FakeDbSet<Car>();
            _carMockSet.SetData(cars);

            _imagePathMockSet = new FakeDbSet<ImagePath>();
            _imagePathMockSet.SetData(imagePaths);

            _carMockSet.Setup(x => x.FindAsync(It.IsAny<int>())).Returns(_carMockSet.Object.FindAsync(1));

            _mockContext = new Mock<DriveITContext>();
            _mockContext.Setup(c => c.Cars).Returns(_carMockSet.Object);
            _mockContext.Setup(x => x.ImagePaths).Returns(_imagePathMockSet.Object);

            _mockContext.Object.Cars.Include(p => p.ImagePaths);
        }

        internal class FakeDbSet<T> : Mock<DbSet<T>> where T : class
        {
            public void SetData(IEnumerable<T> data)
            {
                var mockDataQueryable = data.AsQueryable();

                As<IQueryable<T>>().Setup(x => x.Provider).Returns(mockDataQueryable.Provider);
                As<IQueryable<T>>().Setup(x => x.Expression).Returns(mockDataQueryable.Expression);
                As<IQueryable<T>>().Setup(x => x.ElementType).Returns(mockDataQueryable.ElementType);
                As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(mockDataQueryable.GetEnumerator());
            }
        }

        private static Mock<IDbSet<T>> CreateMockSet<T>(IQueryable<T> childlessParents) where T : class
        {
            var mockSet = new Mock<IDbSet<T>>();

            mockSet.Setup(m => m.Provider).Returns(childlessParents.Provider);
            mockSet.Setup(m => m.Expression).Returns(childlessParents.Expression);
            mockSet.Setup(m => m.ElementType).Returns(childlessParents.ElementType);
            mockSet.Setup(m => m.GetEnumerator()).Returns(childlessParents.GetEnumerator());
            return mockSet;
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
                Fuel = FuelType.Gasoline,
                Make = "Ford",
                Mileage = 20.5f,
                Model = "TT",
                Price = 2000,
                Sold = false,
                Transmission = "Manual",
                Year = 2001
            },
            _mockContext.Object);

            _carMockSet.Verify(m => m.Add(It.IsAny<Car>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync());
        }

        [Test]
        public void Test()
        {
            var child = new ImagePath();
            var parent = new Car { ImagePaths = new List<ImagePath>()};
            parent.ImagePaths.Add(child);

            var parents = new List<Car>
                {
                    parent
                }.AsQueryable();

            var children = new List<ImagePath>
                {
                    child
                }.AsQueryable();

            var mockContext = new Mock<DriveITContext>();

            var mockParentSet = CreateMockSet(parents);
            var mockChildSet = CreateMockSet(children);

            mockContext.SetupGet(mc => (IDbSet<Car>) mc.Cars).Returns(mockParentSet.Object);
            mockContext.SetupGet(mc => (IDbSet<ImagePath>) mc.ImagePaths).Returns(mockChildSet.Object);

            var query = mockContext.Object.Cars.Include(p => p.ImagePaths).Select(p => p);
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
            _carMockSet.Verify(m => m.Remove(It.IsAny<Car>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
        }

        //[Test]
        //public async void UpdateCarTest()
        //{
        //    await _toTest.UpdateCar(1, new Car {Color = "Turquoise" }, _mockContext.Object);

        //    var result = await _mockContext.Object.Cars.FirstOrDefaultAsync(x => x.Color == "Turquoise");
        //    Assert.AreEqual("Turquoise", result.Color);
        //    Assert.AreEqual(0, result.DistanceDriven);
        //    _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        //}

        public static void Main(string[] args)
        {
            
        }
    }
}
