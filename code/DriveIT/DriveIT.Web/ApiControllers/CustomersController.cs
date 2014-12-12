using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.ApiControllers
{
    /// <summary>
    /// CustomersController gives a view of the registered customers in the system.
    /// </summary>
    public class CustomersController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Constructor with dependency injection.
        /// Uses the supplied repository.
        /// </summary>
        /// <param name="repo">The underlying repository.</param>
        public CustomersController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor.
        /// Uses EntityStorage as the underlying repository.
        /// </summary>
        public CustomersController() : this(new EntityStorage()) { }

        // GET: api/Customers
        /// <summary>
        /// Get all customers.
        /// Only employees and administrators can get this information.
        /// </summary>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded.
        /// If it succeeded the IHttpActionResult will be an OkNegotiatedContentResult which has a list of CustomerDtos as content.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllCustomers())
                .Select(customer => customer.ToDto())
                .ToList());
        }

        // GET: api/Customers/?id=mlin@itu.dk
        /// <summary>
        /// Get a customer by his/her id.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A Task which results in an IHttpActionResult which states whether the call succeeded.
        /// If it succeeds the result will contain a CustomerDto in the body.</returns>
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
        /// <summary>
        /// Update customer information.
        /// This one can only be used by employees and administrators.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="value">The updated customer-information.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded.</returns>
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
        /// <summary>
        /// Delete a customer.
        /// This can be called by customers and administrators.
        /// A customer can only delete itself.
        /// </summary>
        /// <param name="id">The id of the customer to delete.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded.</returns>
        [AuthorizeRoles(Role.Administrator, Role.Customer)]
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != id)
            {
                return BadRequest("A customer can only delete itself.");
            }
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
