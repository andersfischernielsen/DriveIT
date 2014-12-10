using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    public class CommentController : AsyncController
    {

        private CommentsController controller = new CommentsController();

        public async Task<ActionResult> Index(int carId, int customerId)
        {
            ViewBag.carId = carId;
            ViewBag.customerId = customerId;
            var comments = await controller.GetByCarId(carId) as OkNegotiatedContentResult<List<CommentDto>>;
            return View(comments.Content);
        }

        [HttpGet]
        public ActionResult Create(int carId, string customerId)
        {
            var newComment = new CommentDto();
            newComment.CarId = carId;
            newComment.CustomerId = customerId;
            return View(newComment);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CommentDto comment)
        {
            comment.Date = DateTime.Now;
            var commentToPost = await controller.Post(comment) as CreatedAtRouteNegotiatedContentResult<CommentDto>;
            return RedirectToAction("Index", commentToPost.Content);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int commentId)
        {
            var comment = await controller.GetByCommentId(commentId) as OkNegotiatedContentResult<CommentDto>;
            return View(comment.Content);
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(CommentDto comment)
        {
            await controller.Put(comment.Id.Value, comment);
            var updatedComment = await controller.GetByCommentId(comment.Id.Value) as OkNegotiatedContentResult<CommentDto>;
            return RedirectToAction("Index", updatedComment.Content);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index");
        }

        //public async Task<IList<CommentDto>> GetComments(int carId)
        //{
        //    var commentsToReturn = await DriveITWebAPI.ReadList<CommentDto>("Comments/" + carId);
        //    return commentsToReturn;
        //}

        //public async Task UpdateComment(CommentDto value)
        //{
        //    await DriveITWebAPI.Update("Comments", value, value.Id.Value);
        //}

        //public async Task CreateComment(int carId, CommentDto value)
        //{
        //    await DriveITWebAPI.Create("Comments/" + carId, value);
        //}

        //public async Task DeleteComment(int CarId, int CommentId)
        //{
        //    await DriveITWebAPI.Delete<CommentDto>("Comments/" + CarId, CommentId);
        //}
    }
}