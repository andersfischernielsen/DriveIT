using System;
using System.Collections.Generic;
using System.Data.Entity;
using DriveIT.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DriveIT.Entities.Tests
{
    [TestClass]
    public class EntityAdapterTests
    {
        //THIS IS A SHITTY TEST-THING. TESTING SHOULD BE DONE WITH MOCK, BUT I'M TIRED OF ENTITY FRAMEWORK NOW.
        public static void Main(string[] args)
        {
            var t = new EntityStorage();

            var newCar = new Car
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
            };
            
            t.UpdateCar(13, newCar);
            
        }

        private EntityStorage _toTest;

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

            _toTest = new EntityStorage();
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
    }
}
