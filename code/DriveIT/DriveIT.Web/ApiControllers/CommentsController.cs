using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.ApiControllers
{
    /// <summary>
    /// The CommentsController is responsible for handling customers commenting cars.
    /// </summary>
    public class CommentsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Uses dependency injection to make it testable.
        /// Can also be used to have a CommentsController which works on a non-default repository.
        /// </summary>
        /// <param name="repo">The underlying repository.</param>
        public CommentsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor. Uses EntityStorage as the underlying repository.
        /// </summary>
        public CommentsController() : this(new EntityStorage()) { }

        // GET: api/Comments/?commentId=5
        /// <summary>
        /// Get comment by its comment-id.
        /// </summary>
        /// <param name="commentId">The Id of the Comment.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetByCommentId(int commentId)
        {
            // Remember: It is a car-id. Not commentId.
            var comment = await _repo.GetCommentWithId(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToDto());
        }

        // GET: api/Comments/5
        /// <summary>
        /// Get all comments that were commented on a specified car.
        /// </summary>
        /// <param name="id">The id of the car.</param>
        /// <returns>A Task which results in an IHttpActionResult which has information about whether the call succeeded or not.
        /// If it did succeed, the content of the IHttpActionResult holds a list of CommentDtos.</returns>
        public async Task<IHttpActionResult> GetByCarId(int id)
        {
            // Remember: It is a car-id. Not commentId.
            var carId = id;
            var comments = (await _repo.GetAllCommentsForCar(carId));
            if (!comments.Any())
            {
                return NotFound();
            }
            return Ok(comments
                .Select(comment => comment.ToDto())
                .ToList());
        }

        // POST: api/Comments
        /// <summary>
        /// Create a comment in the storage.
        /// Only users logged in as customers can use this call to the api.
        /// </summary>
        /// <param name="value">The CommentDto holding information about the user, the car and what was written.</param>
        /// <returns>A Task resulting in an IHttpActionResult, which states whether it succeeded or not.
        /// If it did succeed, the content will contain the CommentDto with an added Id.</returns>
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
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != value.CustomerId)
            {
                return BadRequest("CustomerId should be the same as the logged in user");
            }
            value.Date = DateTime.Now;
            var newCommentId = await _repo.CreateComment(value.ToEntity());
            value.Id = newCommentId;
            return CreatedAtRoute("DefaultApi", new { id = newCommentId }, value);
        }

        // PUT: api/Comments/5
        /// <summary>
        /// Update a comment.
        /// Can only be called by a customer.
        /// A customer can only update comments they have written.
        /// </summary>
        /// <param name="id">The Id of the comment to update.</param>
        /// <param name="value">The CommentDto holding updated information.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded or not.</returns>
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
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != comment.CustomerId)
            {
                return BadRequest("Customer cannot change other customers comments.");
            }
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != value.CustomerId)
            {
                return BadRequest("Customer cannot set author to another person.");
            }
            await _repo.UpdateComment(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Comments/5
        /// <summary>
        /// Delete a comment by its Id.
        /// Customers can only delete comments they have written.
        /// </summary>
        /// <param name="id">The Id of the comment.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the calle succeeded or not.</returns>
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
