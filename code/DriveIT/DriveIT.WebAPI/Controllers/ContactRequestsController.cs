using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
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
            return Ok(from contactRequest in await _repo.GetAllContactRequests()
                      select contactRequest.ToDto());
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
            //Todo if customer, check id to see if it is the logged in one.
            return Ok((from contactRequest in await _repo.GetAllContactRequests()
                where contactRequest.CustomerId == userId
                select contactRequest.ToDto()).ToList());
        }

        // POST: api/ContactRequests
        [AuthorizeRoles(Role.Customer)]
        public async Task<IHttpActionResult> Post([FromBody]ContactRequestDto value)
        {
            //Todo make stuff valid for customer.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            var newContactRequestId = await _repo.CreateContactRequest(value.ToEntity());
            value.Id = newContactRequestId;
            return CreatedAtRoute("DefaultApi", new { id = newContactRequestId }, value);
        }

        // PUT: api/ContactRequests/5
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ContactRequestDto value)
        {
            //Todo check.
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
            //Todo customer check.
            var contactRequest = await _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            await _repo.DeleteContactRequest(id);
            return Ok();
        }
    }
}
