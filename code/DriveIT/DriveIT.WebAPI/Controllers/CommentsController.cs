using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public CommentsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public CommentsController() : this(new EntityStorage()) { }

        // GET: api/Comments/5
        // Where 5 is CarId

        public async Task<IHttpActionResult> GetByCarId(int id)
        {
            // Remember: It is a car-id. Not commentId.
            var carId = id;
            var comments = (from comment in await _repo.GetAllCommentsForCar(carId)
                            select comment.ToDto()).ToList();
            if (!comments.Any())
            {
                return NotFound();
            }
            return Ok(comments);
        }

        // POST: api/Comments
        [AuthorizeRoles(Role.Customer, Role.Administrator)]
        public async Task<IHttpActionResult> Post([FromBody]CommentDto value)
        {
            //Todo if customer, check that it is his own comment.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            var newCommentId = await _repo.CreateComment(value.ToEntity());
            return CreatedAtRoute("DefaultApi", new { id = newCommentId }, value);
        }

        // PUT: api/Comments/5
        [AuthorizeRoles(Role.Customer, Role.Administrator)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CommentDto value)
        {
            //Todo if customer, check that it is his own comment.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _repo.UpdateComment(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Comments/5
        [AuthorizeRoles(Role.Customer, Role.Administrator)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            //Todo if customer check that it is his own comment.
            var comment = await _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _repo.DeleteComment(id);
            return Ok();
        }
    }
}
