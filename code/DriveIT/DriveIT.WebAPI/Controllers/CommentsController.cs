using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CommentsController : ApiController
    {
        private IPersistentStorage _repo = new EntityAdapter(new EntityContext());

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
        }

        // PUT: api/Comments/5
        public IHttpActionResult Put(int id, [FromBody]CommentDto value)
        {
        }

        // DELETE: api/Comments/5
        public IHttpActionResult Delete(int id)
        {
        }
    }
}
