using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class CarControllerTests
    {
        private CarController _carController;
        private int setupCarId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DriveITWebAPI.Login("admin@driveIT.dk", "4dmin_Password").Wait();
            _carController = new CarController();
            var carTask = _carController.CreateCar(new CarDto()
            {
                Color = "TestSetup",
                DistanceDriven = 100,
                Model = "TestModel",
                Make = "TestMake",
                Price = 100,
                Fuel = FuelType.Gasoline,
                Created = DateTime.Now,
                ImagePaths = new List<string>()
            });
            carTask.Wait();
            setupCarId = carTask.Result.Id.GetValueOrDefault();
            Console.WriteLine();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var deleteTask = _carController.DeleteCar(setupCarId);
            deleteTask.Wait();
            Console.WriteLine();
        }

        [Test]
        public async Task TestCreateCar()
        {
            var t = _carController.ReadCarList().Result;
            int amtOfCarsStart = t.Count;
            var carToCreate = new CarDto()
            {
                Color = "Red",
                DistanceDriven = 10000,
                Model = "A8",
                Make = "Audi",
                Price = 200000,
                Fuel = FuelType.Gasoline,
                Created = DateTime.Now,
                ImagePaths = new List<string>()
            };
            await _carController.CreateCar(carToCreate);
            Thread.Sleep(1000);
            t = _carController.ReadCarList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var carJustIn = t[t.Count - 1];
            Assert.AreEqual(carToCreate.Color, carJustIn.Color);
            Assert.AreEqual(carToCreate.DistanceDriven, carJustIn.DistanceDriven);
            Assert.AreEqual(carToCreate.Model, carJustIn.Model);
            Assert.AreEqual(carToCreate.Make, carJustIn.Make);
            Assert.AreEqual(carToCreate.Price, carJustIn.Price);
            Assert.AreEqual(carToCreate.Fuel, carJustIn.Fuel);

            await _carController.DeleteCar(carJustIn.Id.GetValueOrDefault());
        }

        [Test]
        public async Task TestDeleteCar()
        {
            var t = _carController.ReadCarList().Result;
            int amtOfCarsStart = t.Count;
            var carToCreate = new CarDto()
            {
                Color = "Red",
                DistanceDriven = 10000,
                Model = "A8",
                Make = "Audi",
                Price = 200000,
                Fuel = FuelType.Gasoline,
                Created = DateTime.Now,
                ImagePaths = new List<string>()
            };
            await _carController.CreateCar(carToCreate);
            Thread.Sleep(1000);
            t = _carController.ReadCarList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var carJustIn = t[t.Count - 1];
            await _carController.DeleteCar(carJustIn.Id.GetValueOrDefault());
            t = _carController.ReadCarList().Result;

            Assert.AreEqual(amtOfCarsStart, t.Count);
            foreach (var carDto in t)
            {
                Assert.AreNotEqual(carJustIn.Id.GetValueOrDefault(), carDto.Id.GetValueOrDefault());
            }
        }


        [Test]
        public async Task TestUpdateCar()
        {
            var t = _carController.ReadCarList().Result;
            int amtOfCarsStart = t.Count;
            var carToCreate = new CarDto()
            {
                Color = "Red",
                DistanceDriven = 100,
                Model = "A8",
                Make = "Audi",
                Price = 100,
                Fuel = FuelType.Gasoline,
                Created = DateTime.Now,
                ImagePaths = new List<string>()
            };
            await _carController.CreateCar(carToCreate);
            Thread.Sleep(1000);
            t = _carController.ReadCarList().Result;
            Assert.AreEqual(amtOfCarsStart+1, t.Count);
            var carJustIn = t[t.Count - 1];

            carJustIn.Color = "Silver";
            carJustIn.DistanceDriven = 200;
            carJustIn.Model = "Focus";
            carJustIn.Make = "Ford";
            carJustIn.Price = 200;
            carJustIn.Fuel = FuelType.Diesel;
            await _carController.UpdateCar(carJustIn);

            Thread.Sleep(1000);
            t = _carController.ReadCarList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var carUpdated = t[t.Count - 1];
            Assert.AreEqual(carUpdated.Id.GetValueOrDefault(), carJustIn.Id.GetValueOrDefault());

            Assert.AreEqual(carJustIn.Color, carUpdated.Color);
            Assert.AreEqual(carJustIn.DistanceDriven, carUpdated.DistanceDriven);
            Assert.AreEqual(carJustIn.Model, carUpdated.Model);
            Assert.AreEqual(carJustIn.Make, carUpdated.Make);
            Assert.AreEqual(carJustIn.Price, carUpdated.Price);
            Assert.AreEqual(carJustIn.Fuel, carUpdated.Fuel);

            await _carController.DeleteCar(carUpdated.Id.GetValueOrDefault());
        }
    }
}
