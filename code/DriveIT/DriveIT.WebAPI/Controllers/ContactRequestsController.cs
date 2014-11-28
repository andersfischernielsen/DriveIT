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
    public class ContactRequestsController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityStorage();

        // GET: api/ContactRequests
        public async Task<IHttpActionResult> Get()
        {
            return Ok(from contactRequest in await _repo.GetAllContactRequests()
                      select contactRequest.ToDto());
        }

        // GET: api/ContactRequests/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var contactRequest = _repo.GetContactRequestWithId(id);
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
            var newContactRequestId = await _repo.CreateContactRequest(value.ToEntity(_repo));
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newContactRequestId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/ContactRequests/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]ContactRequestDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.UpdateContactRequest(id, value.ToEntity(_repo));
            return Ok();
        }

        // DELETE: api/ContactRequests/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var contactRequest = _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            await _repo.DeleteContactRequest(id);
            return Ok();
        }
    }
}
