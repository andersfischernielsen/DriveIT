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
    public class ContactRequestsControllerTests
    {
        private ContactRequestsController _controller;
        private ContactRequest _contactRequest3;

        [SetUp]
        public void SetUp()
        {
            var contactRequests = new List<ContactRequest>
            {
                new ContactRequest
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    Created = new DateTime(2014, 12 , 7),
                },
                new ContactRequest
                {
                    Id = 2,
                    CarId = 1,
                    CustomerId = "anothercust@driveit.dk",
                    Created = new DateTime(2014, 12, 8)
                }
            };
            _contactRequest3 = new ContactRequest
            {
                Id = 2,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                Created = new DateTime(2014, 12, 13),
            };


            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllContactRequests()).Returns(Task.Run(() => contactRequests));
            mockRepo.Setup(x => x.GetContactRequestWithId(1)).Returns(Task.Run(() => contactRequests.Find(c => c.Id == 1)));
            mockRepo.Setup(x => x.GetContactRequestWithId(2)).Returns(Task.Run(() => contactRequests.Find(c => c.Id == 2)));

            mockRepo.Setup(x => x.CreateContactRequest(It.IsAny<ContactRequest>())).Returns(Task.Run(() => contactRequests.Max(x => x.Id) + 1));

            _controller = new ContactRequestsController(mockRepo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task Get_Count2()
        {
            var message = await _controller.Get() as OkNegotiatedContentResult<List<ContactRequestDto>>;
            // assert
            Assert.IsNotNull(message, _controller.Get().Result.GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<List<ContactRequestDto>>(content);
            var contactRequestDtos = content as IList<ContactRequestDto>;
            Assert.AreEqual(2, contactRequestDtos.Count());
            Assert.AreEqual(1, contactRequestDtos.First().Id);
            Assert.AreEqual(2, contactRequestDtos.Skip(1).First().Id);
        }

        [Test]
        public async Task Get_MultipleParameters_NotFoundResult()
        {
            var message = await _controller.Get(6) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(0) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.Get(-1) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task GetByUserId_Success()
        {
            var message =
                await _controller.GetByUserId("cust@driveit.dk") as OkNegotiatedContentResult<List<ContactRequestDto>>;
            Assert.IsNotNull(message, _controller.GetByUserId("cust@driveit.dk").GetType().ToString());
            Assert.AreEqual(1, message.Content.Count);

            message =
                await _controller.GetByUserId("anothercust@driveit.dk") as OkNegotiatedContentResult<List<ContactRequestDto>>;
            Assert.IsNotNull(message, _controller.GetByUserId("anothercust@driveit.dk").GetType().ToString());
            Assert.AreEqual(1, message.Content.Count);
        }

        [Test]
        public async Task GetByUserId_NotFound()
        {
            var message =
                await _controller.GetByUserId("notacust@driveit.dk") as NotFoundResult;
            Assert.IsNotNull(message, _controller.GetByUserId("notacust@driveit.dk").GetType().ToString());
        }

        [Test]
        public async Task Post_Returns3()
        {
            var message = await _controller.Post(_contactRequest3.ToDto()) as CreatedAtRouteNegotiatedContentResult<ContactRequestDto>;

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
        public async Task Put_Success()
        {
            var message = await _controller.Put(2, _contactRequest3.ToDto()) as OkResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put(29141, _contactRequest3.ToDto()) as NotFoundResult;
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