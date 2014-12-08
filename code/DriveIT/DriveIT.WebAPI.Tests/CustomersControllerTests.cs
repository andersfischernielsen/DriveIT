using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;
using DriveIT.WebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DriveIT.WebAPI.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        private CustomersController _controller;
        private Customer _customer3;

        [TestInitialize]
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

        [TestCleanup]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [TestMethod]
        public async Task Get_ReturnsListOfCustomerDto_Count2()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<CustomerDto>>;
            // assert
            Assert.IsNotNull(message, (await _controller.Get()).GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOfType(content, typeof(IEnumerable<CustomerDto>));
            var customerDtos = content as IList<CustomerDto>;
            Assert.AreEqual(2, customerDtos.Count());
            Assert.AreEqual("cust@driveit.dk", customerDtos.First().Id);
            Assert.AreEqual("anothercust@driveit.dk", customerDtos.Skip(1).First().Id);
        }

        [TestMethod]
        public async Task Get_NoResult_MultipleCalls()
        {
            var message = await _controller.Get("notacust@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Put_Success()
        {
            var message = await _controller.Put("cust@driveit.dk", _customer3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put("notacust@driveit.dk", _customer3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Delete_Ok()
        {
            var message = await _controller.Delete("cust@driveit.dk") as OkResult;
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public async Task Delete_NotFound()
        {
            var message = await _controller.Delete("notacust@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }
    }
}
