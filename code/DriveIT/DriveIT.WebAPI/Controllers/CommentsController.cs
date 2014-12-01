using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public CommentsController(IPersistentStorage repo = null)
        {
            _repo = repo ?? new EntityStorage();
        }

        // GET: api/Comments/5
        // Where 5 is CarId
        public async Task<IHttpActionResult> Get(int carId)
        {
            var comments = (from comment in await _repo.GetAllCommentsForCar(carId)
                            select comment.ToDto()).ToList();
            if (!comments.Any())
            {
                return NotFound();
            }
            return Ok(comments);
        }

        // POST: api/Comments
        public async Task<IHttpActionResult> Post([FromBody]CommentDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCommentId = await _repo.CreateComment(await value.ToEntity(_repo));
            return CreatedAtRoute("DefaultApi", new { id = newCommentId }, value);
        }

        // PUT: api/Comments/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]CommentDto value)
        {
            var comment = await _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _repo.UpdateComment(id, await value.ToEntity(_repo));
            return Ok();
        }

        // DELETE: api/Comments/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.DeleteComment(id);
            return Ok();
        }
    }
}
