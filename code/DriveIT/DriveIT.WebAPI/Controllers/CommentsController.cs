﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;
using Microsoft.AspNet.Identity;

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
            var comments = await _repo.GetAllCommentsForCar(carId);
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            return Ok(
                comments
                .Select(comment => comment.ToDto())
                .ToList());
        }

        // POST: api/Comments
        [AuthorizeRoles(Role.Customer)]
        public async Task<IHttpActionResult> Post([FromBody]CommentDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            if (User.IsInRole("Customer") && User.Identity.GetUserId() != value.CustomerId)
            {
                return BadRequest("CustomerId should be the same as the logged in user");
            }

            var newCommentId = await _repo.CreateComment(value.ToEntity());
            return CreatedAtRoute("DefaultApi", new { id = newCommentId }, value);
        }

        // PUT: api/Comments/5
        [AuthorizeRoles(Role.Customer)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CommentDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Customer") && User.Identity.GetUserId() != comment.CustomerId)
            {
                return BadRequest("Customer cannot change other customers comments.");
            }
            if (User.IsInRole("Customer") && User.Identity.GetUserId() != value.CustomerId)
            {
                return BadRequest("Customer cannot set author to another person.");
            }
            await _repo.UpdateComment(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Comments/5
        [AuthorizeRoles(Role.Customer, Role.Administrator)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var comment = await _repo.GetCommentWithId(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != comment.CustomerId)
            {
                return BadRequest("Customer cannot delete others comments.");
            }
            await _repo.DeleteComment(id);
            return Ok();
        }
    }
}
