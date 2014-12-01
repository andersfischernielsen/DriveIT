using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.Models;
using DriveIT.WebAPI.Models;
using _repo = DriveIT.EntityFramework.EntityStorage;

namespace DriveIT.WebAPI.Controllers
{
    public class CustomersController : ApiController
    {

        // GET: api/Customers
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                from customer in await _repo.GetAllCustomers()
                select customer.ToDto());
        }

        // GET: api/Customers/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var customer = await _repo.GetCustomerWithId(id);
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
        public async Task<IHttpActionResult> Put(int id, [FromBody]CustomerDto value)
        {
            var customer = await _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _repo.UpdateCustomer(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Customers/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var customer = await _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _repo.DeleteCustomer(id);
            return Ok();
        }
    }
}
