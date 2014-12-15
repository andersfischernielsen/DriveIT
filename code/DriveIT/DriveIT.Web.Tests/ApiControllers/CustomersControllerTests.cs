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
    public class CustomersControllerTests
    {
        private CustomersController _controller;
        private Customer _customer3;

        [SetUp]
        public void SetUp()
        {
            var customerList = new List<Customer>
            {
                new Customer
                {
                    Id = "cust@driveit.dk",
                },
                new Customer
                {
                    Id = "anothercust@driveit.dk",
                }
            };
            _customer3 = new Customer
            {
                Id = "cust@driveit.dk",
            };


            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllCustomers()).Returns(Task.Run(() => customerList));
            mockRepo.Setup(x => x.GetCustomerWithId("cust@driveit.dk")).Returns(Task.Run(() => customerList.Find(c => c.Id == "cust@driveit.dk")));
            mockRepo.Setup(x => x.GetCustomerWithId("anothercust@driveit.dk")).Returns(Task.Run(() => customerList.Find(c => c.Id == "anothercust@driveit.dk")));


            _controller = new CustomersController(mockRepo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task Get_ReturnsListOfCustomerDto_Count2()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<CustomerDto>>;
            // assert
            Assert.IsNotNull(message, (await _controller.Get()).GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<List<CustomerDto>>(content);
            var customerDtos = content as IList<CustomerDto>;
            Assert.AreEqual(2, customerDtos.Count());
            Assert.AreEqual("cust@driveit.dk", customerDtos.First().Id);
            Assert.AreEqual("anothercust@driveit.dk", customerDtos.Skip(1).First().Id);
        }

        [Test]
        public async Task Get_NoResult_MultipleCalls()
        {
            var message = await _controller.Get("notacust@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_Success()
        {
            var message = await _controller.Put("cust@driveit.dk", _customer3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put("notacust@driveit.dk", _customer3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_Ok()
        {
            var message = await _controller.Delete("cust@driveit.dk") as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_NotFound()
        {
            var message = await _controller.Delete("notacust@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }
    }
}