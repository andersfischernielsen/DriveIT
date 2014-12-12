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

        public ActionResult Index()
        {
            return View();
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
            return RedirectToAction("Details", "Car", new { carId = comment.CarId });
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
            comment.Date = DateTime.Now;
          
            var updatedComment = await controller.Put(comment.Id.Value, comment) as OkNegotiatedContentResult<CommentDto>;

            return RedirectToAction("Details", "Car", new { carId = comment.CarId });
        }

        public async Task<ActionResult> Delete(int commentId, int redirectCarId)
        {
            await controller.Delete(commentId);
            return RedirectToAction("Details", "Car", new { carId = redirectCarId });
        }
    }
}