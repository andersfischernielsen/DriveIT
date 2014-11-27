using System.Collections.Generic;
using System.Data.Entity;
using DriveIT.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DriveIT.Entities.Tests
{
    [TestClass]
    public class DriveItContextTests
    {
        private EntityAdapter _toTest;

        [TestInitialize]
        public void SetUp()
        {
            var cars = new List<Car>
            {
                new Car {Id = 1, Make = "Ford"},
                new Car {Id = 2, Make = "Volvo"},
                new Car {Id = 3, Make = "Saab"},
                new Car {Id = 4, Make = "Opel"}
            };
            var mockSet = new Mock<DbSet<Car>>().SetupData(cars);

            var context = new Mock<DriveITContext>();
            context.Setup(c => c.Cars).Returns(mockSet.Object);

            _toTest = new EntityAdapter(context.Object);
        }

        [TestMethod]
        public void AddCarTest()
        {
            _toTest.CreateCar(new Car {Id = 5, Make = "Mercedes Benz"});
        }

        [TestMethod]
        public void GetCarTest()
        {
            Assert.AreEqual(1, _toTest.GetCarWithId(1).Id);
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
