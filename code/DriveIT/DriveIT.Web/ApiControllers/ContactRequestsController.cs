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
    /// ContactRequestsController is responsible for handling customer's wishes to be contacted about a specified car.
    /// </summary>
    public class ContactRequestsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Constructor of ContactRequestsController which uses dependency injection.
        /// 
        /// Makes it possible to use non-default repositories, and even mocks in test situations.
        /// </summary>
        /// <param name="repo">The underlying repository.</param>
        public ContactRequestsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor of ContactRequestsController.
        /// Uses EntityStorage as the underlying repository.
        /// </summary>
        public ContactRequestsController() : this(new EntityStorage()) { }

        // GET: api/ContactRequests
        /// <summary>
        /// Get all available ContactRequests.
        /// Only employees and administrators can see contact requests.
        /// </summary>
        /// <returns>A Task resulting in an OkNegotiatedContentResult which has a list of ContactRequestDtos</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllContactRequests())
                .Select(contactRequest => contactRequest.ToDto())
                .ToList());
        }

        // GET: api/ContactRequests/5
        /// <summary>
        /// Get a specified contactrequest by its id.
        /// This method can only be called by employees and administrators.
        /// </summary>
        /// <param name="id">The id of the contact request.</param>
        /// <returns>A Task resulting in an IHttpActionResult, which states whether the call succeeded or not.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get(int id)
        {
            var contactRequest = await _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            return Ok(contactRequest.ToDto());
        }

        /// <summary>
        /// Get contact request by user id.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether call was successful or not.
        /// If any contact requests has been found they will be returned as a list in the content.</returns>
        [AuthorizeRoles(Role.Customer, Role.Administrator)]
        public async Task<IHttpActionResult> GetByUserId(string userId)
        {
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != userId)
            {
                return Unauthorized();
            }
            var contactRequests = (await _repo.GetAllContactRequests())
                .Where(contactRequest => contactRequest.CustomerId == userId)
                .Select(contactRequest => contactRequest.ToDto())
                .ToList();
            if (!contactRequests.Any())
            {
                return NotFound();
            }
            return Ok(contactRequests);
        }

        // POST: api/ContactRequests
        /// <summary>
        /// Create a contact request in the repository.
        /// Only customers can create new contact requests.
        /// </summary>
        /// <param name="value">The contact request to create. Information about the user should match the object.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call was successful or not.
        /// If it was, the content will be the received contact request with an id.</returns>
        [AuthorizeRoles(Role.Customer)]
        public async Task<IHttpActionResult> Post([FromBody]ContactRequestDto value)
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
                return BadRequest("CustomerId must match the logged in user!");
            }
            value.Requested = DateTime.Now;
            var newContactRequestId = await _repo.CreateContactRequest(value.ToEntity());
            value.Id = newContactRequestId;
            return CreatedAtRoute("DefaultApi", new { id = newContactRequestId }, value);
        }

        // PUT: api/ContactRequests/5
        /// <summary>
        /// Update a contact request.
        /// Can only be done by employees and administrators.
        /// </summary>
        /// <param name="id">The id of the contact request.</param>
        /// <param name="value">The updated contact request information.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded or not.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ContactRequestDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contactRequest = await _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            await _repo.UpdateContactRequest(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/ContactRequests/5
        /// <summary>
        /// Delete a contact request.
        /// A customer can only delete his/her own contact requests.
        /// Employees and administrators can delete all contact requests.
        /// </summary>
        /// <param name="id">The id of the contact request which should be deleted.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded or not.</returns>
        [AuthorizeRoles(Role.Customer, Role.Administrator, Role.Employee)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var contactRequest = await _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != contactRequest.CustomerId)
            {
                return Unauthorized();
            }
            await _repo.DeleteContactRequest(id);
            return Ok();
        }
    }
}
