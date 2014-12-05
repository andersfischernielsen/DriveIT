using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;

namespace DriveIT.MVC.Controllers
{
    public class CommentController : AsyncController
    {

        private CommentsController controller = new CommentsController();

        public ActionResult Create(int carId)
        {
            var newComment = new CommentDto();
            newComment.CarId = carId;
            return View(newComment);
        }

        public async Task<ActionResult> Get()
        {
            var carId = 1;
            var comments = await controller.Get(carId) as OkNegotiatedContentResult<IList<CommentDto>>;
            return View(comments.Content);
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