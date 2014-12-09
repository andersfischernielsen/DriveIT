using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;
using DriveIT.WebAPI.Models;
using Moq;
using NUnit.Framework;

namespace DriveIT.WebAPI.Tests
{
    [TestFixture]
    public class EmployeesControllerTests
    {
        private EmployeesController _controller;
        private Employee _employee3;

        [SetUp]
        public void SetUp()
        {
            var employeeList = new List<Employee>
            {
                new Employee
                {
                    Id = "mlin@itu.dk",
                },
                new Employee
                {
                    Id = "afin@itu.dk",
                }
            };
            _employee3 = new Employee
            {
                Id = "mlin@itu.dk",
            };


            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllEmployees()).Returns(Task.Run(() => employeeList));
            mockRepo.Setup(x => x.GetEmployeeWithId("mlin@itu.dk")).Returns(Task.Run(() => employeeList.Find(c => c.Id == "mlin@itu.dk")));
            mockRepo.Setup(x => x.GetEmployeeWithId("afin@itu.dk")).Returns(Task.Run(() => employeeList.Find(c => c.Id == "mlin@itu.dk")));


            _controller = new EmployeesController(mockRepo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task Get_ReturnsListOfCustomerDto_Count2()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<EmployeeDto>>;
            // assert
            Assert.IsNotNull(message, (await _controller.Get()).GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<List<EmployeeDto>>(content);
            var employeeDtos = content as IList<EmployeeDto>;
            Assert.AreEqual(2, employeeDtos.Count());
            Assert.AreEqual("mlin@itu.dk", employeeDtos.First().Id);
            Assert.AreEqual("afin@itu.dk", employeeDtos.Skip(1).First().Id);
        }

        [Test]
        public async Task Get_NoResult_MultipleCalls()
        {
            var message = await _controller.Get("notanemp@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_Success()
        {
            var message = await _controller.Put("mlin@itu.dk", _employee3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put("notanemp@driveit.dk", _employee3.ToDto()) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_Ok()
        {
            var message = await _controller.Delete("mlin@itu.dk") as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Delete_NotFound()
        {
            var message = await _controller.Delete("notanemp@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message);
        }
    }
}
