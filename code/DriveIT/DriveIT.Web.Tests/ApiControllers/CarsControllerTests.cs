using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DriveIT.EntityFramework;
using DriveIT.EntityFramework.Entities;
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
                    Transmission = "Manual",
                    Year = 2006,
                    ImagePaths = new List<ImagePath>
                    {
                        new ImagePath
                        {
                            CarId = 1,
                            Path = "http://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Suzuki_Swift_front-1.jpg/250px-Suzuki_Swift_front-1.jpg"
                        }
                    }
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
                    Transmission = "Manual",
                    Year = 2004,
                    ImagePaths = new List<ImagePath>()
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
                Transmission = "Manual",
                Year = 1993,
                ImagePaths = new List<ImagePath>()
            };

            var sales = new List<Sale>
            {
                new Sale
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    DateOfSale = DateTime.Now, // less than 5 days ago.
                    EmployeeId = "admin@driveit.dk",
                    Price = 20000
                },
                new Sale
                {
                    Id = 2,
                    CarId = 2,
                    CustomerId = "cust@driveit.dk",
                    DateOfSale = DateTime.Now.Subtract(TimeSpan.FromDays(6)), // More than 5 days ago
                    EmployeeId = "admin@driveit.dk",
                    Price = 10000
                }
            };

            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllCars()).ReturnsAsync(carList);
            mockRepo.Setup(x => x.GetCarWithId(2)).ReturnsAsync(carList.SingleOrDefault(c => c.Id == 2));
            mockRepo.Setup(x => x.CreateCar(It.IsAny<Car>())).ReturnsAsync(carList.Max(x => x.Id) + 1);
            mockRepo.Setup(x => x.GetSaleByCarId(1, null)).ReturnsAsync(sales.SingleOrDefault(sale => sale.CarId == 1));
            mockRepo.Setup(x => x.GetSaleByCarId(2, null)).ReturnsAsync(sales.SingleOrDefault(sale => sale.CarId == 2));

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
        public async Task WebCarList_NoParameters_ReturnsCars()
        {
            var result = await _controller.WebCarList();
            Assert.IsNotNull(result);
            foreach (var carDto in result)
            {
                Assert.IsTrue(carDto.Id != 2); //Test that the car that was sold more than 5 days ago is not in the list.
            }
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