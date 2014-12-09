using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.ApiControllers
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
                (from customer in await _repo.GetAllCustomers()
                select customer.ToDto()).ToList());
        }

        // GET: api/Customers/?id=mlin@itu.dk
        [Authorize]
        public async Task<IHttpActionResult> Get(string id)
        {
            if (User.IsInRole("Customer") && User.Identity.GetUserId() != id)
            {
                return Unauthorized();
            }
            var customer = await _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToDto());
        }

        // PUT: api/Customers/5
        [AuthorizeRoles(Role.Administrator, Role.Employee)]
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
        [AuthorizeRoles(Role.Administrator, Role.Customer)]
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
