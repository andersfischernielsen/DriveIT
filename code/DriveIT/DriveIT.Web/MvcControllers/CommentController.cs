using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.MvcControllers
{
    public class CommentController : AsyncController
    {

        private CommentsController controller = new CommentsController();

        /// <summary>
        /// View containing every comments of a specific car (the view is rendered in another view, as it is a partial view).
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Creates a new CommentDto object, that is to be created in the database. This object will be used by the second create method.
        /// </summary>
        /// <param name="carId">carId to create a comment on.</param>
        /// <param name="customerId">customerId to create a comment on.</param>
        /// <returns>~Views/Comment/Create.cshtml</returns>
        [HttpGet]
        public ActionResult Create(int carId, string customerId)
        {
            var newComment = new CommentDto();
            newComment.CarId = carId;
            newComment.CustomerId = User.Identity.GetUserName();
            return View(newComment);
        }

        /// <summary>
        /// Sets the DateTime of the object to now and then posts the comment.
        /// </summary>
        /// <param name="comment">CommentDto to be posted.</param>
        /// <returns>Redirect to ~Views/Car/Details.cshtml</returns>
        [HttpPost]
        public async Task<ActionResult> Create(CommentDto comment)
        {
            comment.Date = DateTime.Now;
            var commentToPost = await controller.Post(comment) as CreatedAtRouteNegotiatedContentResult<CommentDto>;
            return RedirectToAction("Details", "Car", new { carId = comment.CarId });
        }

        /// <summary>
        /// Gets the comment object with the specific commentId. This object will be used by the second edit method.
        /// </summary>
        /// <param name="commentId">The commentId of the comment to be edited.</param>
        /// <returns>~View/Comment/Edit.cshtml</returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int commentId)
        {
            var comment = await controller.GetByCommentId(commentId) as OkNegotiatedContentResult<CommentDto>;          
            return View(comment.Content);          
        }
        
        /// <summary>
        /// Receives the object, sets the DateTime of the object to now and puts the comment.
        /// </summary>
        /// <param name="comment">Comment object to be updated.</param>
        /// <returns>Redirect to ~Views/Car/Details.csthml</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(CommentDto comment)
        {
            comment.Date = DateTime.Now;     
            var updatedComment = await controller.Put(comment.Id.Value, comment) as OkNegotiatedContentResult<CommentDto>;
            return RedirectToAction("Details", "Car", new { carId = comment.CarId });
        }

        /// <summary>
        /// Deletes the object with the specific commentId.
        /// </summary>
        /// <param name="commentId">commentId of the comment to delete.</param>
        /// <param name="redirectCarId">carId to redirect to car details after comment deletion.</param>
        /// <returns>Redirect to ~Views/Car/Details.csthml</returns>
        public async Task<ActionResult> Delete(int commentId, int redirectCarId)
        {
            await controller.Delete(commentId);
            return RedirectToAction("Details", "Car", new { carId = redirectCarId });
        }
    }
}