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
    public class CustomersController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityStorage();

        // GET: api/Customers
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllCustomers()
                .Select(c => c.ToDto()));
        }

        // GET: api/Customers/5
        public IHttpActionResult Get(int id)
        {
            var customer = _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToDto());
        }

        // POST: api/Customers
        public async Task<IHttpActionResult> Post([FromBody]CustomerDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCustomerId = await _repo.CreateCustomer(value.ToEntity());
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newCustomerId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Customers/5
        public IHttpActionResult Put(int id, [FromBody]CustomerDto value)
        {
            var customer = _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            _repo.UpdateCustomer(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Customers/5
        public IHttpActionResult Delete(int id)
        {
            var customer = _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            _repo.DeleteCustomer(id);
            return Ok();
        }
    }
}
