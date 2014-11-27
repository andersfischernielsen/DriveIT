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
            var mockSet = new Mock<DbSet<Car>>();

            var mockContext = new Mock<DriveITContext>();
            mockContext.Setup(m => m.Cars).Returns(mockSet.Object);

            _toTest = new EntityAdapter(mockContext.Object);
        }

        [TestMethod]
        public void AddCarTest()
        {
            _toTest.CreateCar(new Car {Make = "Ford"});
        }

        [TestMethod]
        public void GetCar_Gets_Correct_Car()
        {
            Assert.AreEqual("Ford", _toTest.GetCarWithId(1).Make);
        }
    }
}
