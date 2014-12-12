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
using Moq;
using NUnit.Framework;

namespace DriveIT.Web.Tests.ApiControllers
{
    [TestFixture]
    public class SalesControllerTests
    {
        private SalesController _controller;
        private Sale _sale3;

        [SetUp]
        public void SetUp()
        {
            var salesList = new List<Sale>
            {
                new Sale
                {
                    Id = 1,
                    Price = 60000,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    DateOfSale = new DateTime(2014, 12, 8),
                    EmployeeId = "mlin@itu.dk"
                },
                new Sale
                {
                    Id = 2,
                    Price = 100000,
                    CarId = 2,
                    CustomerId = "anothercust@driveit.dk",
                    DateOfSale = new DateTime(2014, 12, 10),
                    EmployeeId = "mlin@itu.dk"
                }
            };
            _sale3 = new Sale
            {
                Price = 15000,
                CarId = 3,
                CustomerId = "cust@driveit.dk",
                EmployeeId = "mlin@itu.dk",
                DateOfSale = new DateTime(2014, 12, 14)
            };


            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllSales()).Returns(Task.Run(() => salesList));
            mockRepo.Setup(x => x.GetSaleWithId(2)).Returns(Task.Run(() => salesList.Find(c => c.Id == 2)));

            mockRepo.Setup(x => x.CreateSale(It.IsAny<Sale>())).Returns(Task.Run(() => salesList.Max(x => x.Id) + 1));

            _controller = new SalesController(mockRepo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task Get_NoParameters_ReturnsListOfCarDto_Count3()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<SaleDto>>;
            // assert
            Assert.IsNotNull(message, _controller.Get().Result.GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<List<SaleDto>>(content);
            var carDtos = content as IList<SaleDto>;
            Assert.AreEqual(2, carDtos.Count());
            Assert.AreEqual(1, carDtos.First().Id);
            Assert.AreEqual(2, carDtos.Skip(1).First().Id);
        }

        [Test]
        public async Task Get_2_Result()
        {
            var message = await _controller.Get(2) as OkNegotiatedContentResult<SaleDto>;
            Assert.IsNotNull(message, string.Format("Was of type: {0}", _controller.Get().Result.GetType()));

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<SaleDto>(content);
            Assert.AreEqual(2, content.Id);
            Assert.AreEqual(2, content.CarId);
        }

        [Test]
        public async Task Get_NoResult_MultipleCalls()
        {
            var message = await _controller.Get(6) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(0) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(-1) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task GetFromUserId_Result()
        {
            var message = await _controller.GetFromUserId("cust@driveit.dk") as OkNegotiatedContentResult<List<SaleDto>>;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Post_Returns3()
        {
            var message = await _controller.Post(_sale3.ToDto()) as CreatedAtRouteNegotiatedContentResult<SaleDto>;

            // Assert
            Assert.IsNotNull(message);
            Assert.AreEqual("DefaultApi", message.RouteName);
            Assert.IsTrue(message.RouteValues.ContainsKey("id"));
            Assert.AreEqual(3, message.RouteValues["id"]);
        }

        [Test]
        public async Task Post_BadRequest()
        {
            var message = await _controller.Post(null) as BadRequestErrorMessageResult;
            Assert.IsNotNull(message);
            Assert.AreEqual("Null value not allowed.", message.Message);
        }

        [Test]
        public async Task Put_2_Success()
        {
            var message = await _controller.Put(2, _sale3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put(3, _sale3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Put(0, _sale3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Put(-1, _sale3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_Ok()
        {
            var message = await _controller.Delete(2) as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_NotFound()
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