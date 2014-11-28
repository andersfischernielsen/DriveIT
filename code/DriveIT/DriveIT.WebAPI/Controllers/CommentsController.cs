using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            var comments = _repo.GetAllCommentsForCar(_repo.GetCarWithId(carId));
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.Select(comment => comment.ToDto()));
        }

        // POST: api/Comments
        public async Task<IHttpActionResult> Post([FromBody]CommentDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCommentId = await _repo.CreateComment(value.ToComment(_repo));
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newCommentId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Comments/5
        public IHttpActionResult Put(int id, [FromBody]CommentDto value)
        {
            var comment = _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            _repo.UpdateComment(id, value.ToComment(_repo));
            return Ok();
        }

        // DELETE: api/Comments/5
        public IHttpActionResult Delete(int id)
        {
            _repo.DeleteComment(id);
            return Ok();
        }
    }
}
