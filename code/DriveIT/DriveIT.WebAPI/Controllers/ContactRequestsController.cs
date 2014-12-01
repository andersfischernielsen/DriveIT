using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class ContactRequestsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public ContactRequestsController(IPersistentStorage repo = null)
        {
            _repo = repo ?? new EntityStorage();
        }

        // GET: api/ContactRequests
        public async Task<IHttpActionResult> Get()
        {
            return Ok(from contactRequest in await _repo.GetAllContactRequests()
                      select contactRequest.ToDto());
        }

        // GET: api/ContactRequests/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var contactRequest = await _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            return Ok(contactRequest.ToDto());
        }

        // POST: api/ContactRequests
        public async Task<IHttpActionResult> Post([FromBody]ContactRequestDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newContactRequestId = await _repo.CreateContactRequest(await value.ToEntity(_repo));
            return CreatedAtRoute("DefaultApi", new { id = newContactRequestId }, value);
        }

        // PUT: api/ContactRequests/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]ContactRequestDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.UpdateContactRequest(id, await value.ToEntity(_repo));
            return Ok();
        }

        // DELETE: api/ContactRequests/5
        public async Task<IHttpActionResult> Delete(int id)
        {
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
