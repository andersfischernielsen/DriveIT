using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.Entities;
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
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                from customer in await _repo.GetAllCustomers()
                select customer.ToDto());
        }

        // GET: api/Customers/5
        [Authorize]
        public async Task<IHttpActionResult> Get(string id)
        {
            //TODO Figure out if this should go with AccountController.
            if (User.IsInRole("Customer"))
            {
                //TODO check that Id is only their own.
            }
            var customer = await _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToDto());
        }

        // PUT: api/Customers/5
        [Authorize]
        public async Task<IHttpActionResult> Put(string id, [FromBody]CustomerDto value)
        {
            //Todo check user is changing himself!
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
        [Authorize]
        public async Task<IHttpActionResult> Delete(string id)
        {
            //Todo check that user can only delete himself.
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
