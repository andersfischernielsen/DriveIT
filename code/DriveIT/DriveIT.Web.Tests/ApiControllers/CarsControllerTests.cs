using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;
using DriveIT.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DriveIT.Web.Tests.ApiControllers
{
    [TestClass]
    public class CarsControllerTests
    {
        private CarsController _controller;
        private Car _car3;

        [TestInitialize]
        public void SetUp()
        {
            var carList = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Color = "Silver",
                    Created = new DateTime(2014, 11, 1),
                    DistanceDriven = 50000,
                    Drive = "FWD",
                    Fuel = FuelType.Diesel,
                    Make = "Suzuki",
                    Model = "Swift",
                    Mileage = 18.6f,
                    Price = 60000,
                    Sold = false,
                    Transmission = "Manual",
                    Year = 2006
                },
                new Car
                {
                    Id = 2,
                    Color = "Silver",
                    Created = new DateTime(2014, 11, 2),
                    DistanceDriven = 150000,
                    Drive = "FWD",
                    Fuel = FuelType.Gasoline,
                    Make = "Volkswagen",
                    Mileage = 15f,
                    Model = "Touran",
                    Price = 100000,
                    Sold = false,
                    Transmission = "Manual",
                    Year = 2004
                }
            };
            _car3 = new Car
            {
                Color = "White",
                Created = new DateTime(2014, 11, 30),
                DistanceDriven = 200000,
                Drive = "FWD",
                Fuel = FuelType.Gasoline,
                Make = "Mazda",
                Model = "626",
                Mileage = 12f,
                Price = 15000,
                Sold = false,
                Transmission = "Manual",
                Year = 1993
            };

            var cars = Task.Run(() => carList);

            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllCars(null)).Returns(cars);
            mockRepo.Setup(x => x.GetCarWithId(2, null)).Returns(Task.Run(() => carList.Find(c => c.Id == 2)));

            mockRepo.Setup(x => x.CreateCar(It.IsAny<Car>(), null)).Returns(Task.Run(() => carList.Max(x => x.Id) + 1));

            _controller = new CarsController(mockRepo.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [TestMethod]
        public async Task Get_NoParameters_ReturnsListOfCarDto_Count3()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<CarDto>>;
            // assert
            Assert.IsNotNull(message, _controller.Get().Result.GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOfType(content, typeof(List<CarDto>));
            var carDtos = content as IList<CarDto>;
            Assert.AreEqual(2, carDtos.Count());
            Assert.AreEqual(1, carDtos.First().Id);
            Assert.AreEqual("Suzuki", carDtos.First().Make);
            Assert.AreEqual(2, carDtos.Skip(1).First().Id);
            Assert.AreEqual("Touran", carDtos.Skip(1).First().Model);
        }

        [TestMethod]
        public async Task Get_2_Result()
        {
            var message = await _controller.Get(2) as OkNegotiatedContentResult<CarDto>;
            Assert.IsNotNull(message, string.Format("Was of type: {0}", _controller.Get().Result.GetType()));

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOfType(content, typeof(CarDto));
            Assert.AreEqual("Volkswagen", content.Make);
        }

        [TestMethod]
        public async Task Get_NoResult_MultipleCalls()
        {
            var message = await _controller.Get(6) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(0) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(-1) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task GetCarsByFuelType_Result()
        {
            var message = await _controller.GetCarsByFuelType("Gasoline") as OkNegotiatedContentResult<List<CarDto>>;
            Assert.IsNotNull(message);

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.AreEqual(1, content.Count());
            Assert.AreEqual(FuelType.Gasoline, content.First().Fuel);

            message = await _controller.GetCarsByFuelType("Diesel") as OkNegotiatedContentResult<List<CarDto>>;
            Assert.IsNotNull(message);

            content = message.Content;
            Assert.IsNotNull(content);
            Assert.AreEqual(1, content.Count());
            Assert.AreEqual(FuelType.Diesel, content.First().Fuel);
        }

        [TestMethod]
        public async Task GetCarsByFuelType_NoResult()
        {
            var message = await _controller.GetCarsByFuelType("Electric") as OkNegotiatedContentResult<List<CarDto>>;
            Assert.IsNotNull(message);

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.AreEqual(0, content.Count());
        }

        [TestMethod]
        public async Task GetCarsByMake_Result()
        {
            var message = await _controller.GetCarsByMake("Suzuki") as OkNegotiatedContentResult<List<CarDto>>;
            Assert.IsNotNull(message);

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.AreNotEqual(0, content.Count());
            Assert.AreEqual("Suzuki", content.First().Make);
        }

        [TestMethod]
        public async Task GetCarsByMake_NoResult()
        {
            var message =
                await _controller.GetCarsByMake("I'm not a make") as OkNegotiatedContentResult<List<CarDto>>;
            Assert.IsNotNull(message);

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsFalse(content.Any());
        }

        [TestMethod]
        public async Task Post_Returns3()
        {
            var message = await _controller.Post(_car3.ToDto()) as CreatedAtRouteNegotiatedContentResult<CarDto>;

            // Assert
            Assert.IsNotNull(message);
            Assert.AreEqual("DefaultApi", message.RouteName);
            Assert.IsTrue(message.RouteValues.ContainsKey("id"));
            Assert.AreEqual(3, message.RouteValues["id"]);
        }

        [TestMethod]
        public async Task Post_BadRequest()
        {
            var message = await _controller.Post(null) as BadRequestErrorMessageResult;
            Assert.IsNotNull(message);
            Assert.AreEqual("Null value not allowed.", message.Message);

            //TODO Model state cannot be tested without running the api.
        }

        [TestMethod]
        public async Task Put_Success()
        {
            var message = await _controller.Put(2, _car3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put(29141, _car3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Delete_Ok()
        {
            var message = await _controller.Delete(2) as OkResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task NotFound()
        {
            var message = await _controller.Delete(8) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Delete(0) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Delete(-1) as NotFoundResult;
            Assert.IsNotNull(message);
        }
    }
}