using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public CustomersController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public CustomersController() : this(new EntityStorage()) { }

        // GET: api/Customers
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (from customer in await _repo.GetAllCustomers()
                select customer.ToDto()).ToList());
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
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            var newCustomerId = await _repo.CreateCustomer(value.ToEntity());
            value.Id = newCustomerId;
            return CreatedAtRoute("DefaultApi", new { id = newCustomerId }, value);
        }

        // PUT: api/Customers/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]CustomerDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
