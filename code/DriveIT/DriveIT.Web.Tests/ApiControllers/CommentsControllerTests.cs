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
    public class CommentsControllerTests
    {
        private CommentsController _controller;
        private Comment _comment3;

        [SetUp]
        public void SetUp()
        {
            var commentsList = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    DateCreated = new DateTime(2014, 12, 6),
                    Description = "Badass!!",
                    Title = "Nice!",
                },
                new Comment
                {
                    Id = 2,
                    CarId = 1,
                    CustomerId = "anothercust@driveit.dk",
                    DateCreated = new DateTime(2014, 12, 7),
                    Description = "Fucking shit man!",
                    Title = "Bad"
                }
            };
            _comment3 = new Comment
            {
                Id = 2,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                DateCreated = new DateTime(2014, 12, 3),
                Description = "Pretty",
                Title = "Alright"
            };


            var repo = new MockRepository(MockBehavior.Loose);
            var mockRepo = repo.Create<IPersistentStorage>();
            mockRepo.Setup(x => x.GetAllCommentsForCar(1)).ReturnsAsync(commentsList.Where(c => c.CarId == 1).ToList());
            mockRepo.Setup(x => x.GetCommentWithId(1)).ReturnsAsync(commentsList.Find(c => c.Id == 1));
            mockRepo.Setup(x => x.GetCommentWithId(2)).ReturnsAsync(commentsList.Find(c => c.Id == 2));
            
            mockRepo.Setup(x => x.GetAllCommentsForCar(It.IsAny<int>())).ReturnsAsync(commentsList.Where(c => c.CarId == It.IsAny<int>()).ToList());
            mockRepo.Setup(x => x.GetAllCommentsForCar(1)).ReturnsAsync(commentsList.Where(c => c.CarId == 1).ToList());

            mockRepo.Setup(x => x.CreateComment(It.IsAny<Comment>())).ReturnsAsync(commentsList.Max(x => x.Id) + 1);

            _controller = new CommentsController(mockRepo.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public async Task GetByCarId_1_Count2()
        {
            var message = await _controller.GetByCarId(1) as OkNegotiatedContentResult<List<CommentDto>>;
            // assert
            Assert.IsNotNull(message, _controller.GetByCarId(1).Result.GetType().ToString());

            var content = message.Content;
            Assert.IsNotNull(content);
            Assert.IsInstanceOf<List<CommentDto>>(content);
            Assert.AreEqual(2, content.Count());
            Assert.AreEqual(1, content.First().Id);
            Assert.AreEqual("Nice!", content.First().Title);
            Assert.AreEqual(2, content.Skip(1).First().Id);
            Assert.AreEqual("Bad", content.Skip(1).First().Title);
        }

        [Test]
        public async Task GetByCarId_MultipleParameters_NotFoundResult()
        {
            var message = await _controller.GetByCarId(6) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.GetByCarId(0) as NotFoundResult;
            Assert.IsNotNull(message);

            message = await _controller.GetByCarId(-1) as NotFoundResult;
            Assert.IsNotNull(message);
        }

        [Test]
        public async Task Post_ReturnsId3()
        {
            var message = await _controller.Post(_comment3.ToDto()) as CreatedAtRouteNegotiatedContentResult<CommentDto>;

            // Assert
            Assert.IsNotNull(message, (await _controller.Post(_comment3.ToDto())).GetType().ToString());
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
            var message = await _controller.Put(2, _comment3.ToDto()) as OkResult;
            Assert.IsNotNull(message, (await _controller.Put(2, _comment3.ToDto())).GetType().ToString());
        }

        [Test]
        public async Task Put_NotFound()
        {
            var message = await _controller.Put(29141, _comment3.ToDto()) as NotFoundResult;
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