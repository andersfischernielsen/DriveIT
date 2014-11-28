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
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllContactRequests().Select(c => c.ToDto()));
        }

        // GET: api/ContactRequests/5
        public IHttpActionResult Get(int id)
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
        public IHttpActionResult Put(int id, [FromBody]ContactRequestDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.UpdateContactRequest(id, value.ToEntity(_repo));
            return Ok();
        }

        // DELETE: api/ContactRequests/5
        public IHttpActionResult Delete(int id)
        {
            var contactRequest = _repo.GetContactRequestWithId(id);
            if (contactRequest == null)
            {
                return NotFound();
            }
            _repo.DeleteContactRequest(id);
            return Ok();
        }
    }
}
