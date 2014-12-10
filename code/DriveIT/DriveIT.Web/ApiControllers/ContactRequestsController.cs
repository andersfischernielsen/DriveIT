using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.ApiControllers
{
    public class ContactRequestsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public ContactRequestsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public ContactRequestsController() : this(new EntityStorage()) { }

        // GET: api/ContactRequests
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllContactRequests())
                .Select(contactRequest => contactRequest.ToDto())
                .ToList());
        }

        // GET: api/ContactRequests/5
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
            var newContactRequestId = await _repo.CreateContactRequest(value.ToEntity());
            value.Id = newContactRequestId;
            return CreatedAtRoute("DefaultApi", new { id = newContactRequestId }, value);
        }

        // PUT: api/ContactRequests/5
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
