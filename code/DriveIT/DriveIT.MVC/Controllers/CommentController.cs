using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;

namespace DriveIT.MVC.Controllers
{
    public class CommentController : AsyncController
    {
        // GET: Comment
        public ActionResult CommentView()
        {
            return View();
        }

        public async Task<IList<CommentDto>> GetComments(int carId)
        {
            var commentsToReturn = await DriveITWebAPI.ReadList<CommentDto>("Comments/" + carId);
            return commentsToReturn;
        }

        public async Task UpdateComment(CommentDto value)
        {
            await DriveITWebAPI.Update("Comments", value, value.Id.Value);
        }

        public async Task CreateComment(int carId, CommentDto value)
        {
            await DriveITWebAPI.Create("Comments/" + carId, value);
        }

        public async Task DeleteComment(int CarId, int CommentId)
        {
            await DriveITWebAPI.Delete<CommentDto>("Comments/" + CarId, CommentId);
        }
    }
}