using System.Linq;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityStorage();

        // GET: api/Comments/5
        // Where 5 is CarId
        public IHttpActionResult Get(int carId)
        {
            // Todo: ask for id:
            var comments = _repo.GetAllCommentsForCar(_repo.GetCarWithId(carId));
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.Select(comment => new CommentDto
            {
                CarId = comment.Car.Id,
                CustomerId = comment.Customer.Id,
                Date = comment.DateCreated,
                Description = comment.Description,
                Title = comment.Title
            }));
        }

        // POST: api/Comments
        public IHttpActionResult Post([FromBody]CommentDto value)
        {
            _repo.CreateComment(value.ToComment());
            return Ok();
        }

        // PUT: api/Comments/5
        public IHttpActionResult Put(int id, [FromBody]CommentDto value)
        {
            return BadRequest("Not implemented");
        }

        // DELETE: api/Comments/5
        public IHttpActionResult Delete(int id)
        {
            _repo.DeleteComment(_repo.GetCommentWithId(id));
            return Ok();
        }
    }
}
