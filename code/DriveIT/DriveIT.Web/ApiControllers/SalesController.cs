using System;
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
    /// SalesController is responsible for handling information about sales in the DriveIT system.
    /// </summary>
    public class SalesController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Constructor using dependency injection.
        /// </summary>
        /// <param name="repo">The underlying repository.</param>
        public SalesController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor using EntityStorage as underlying repository.
        /// </summary>
        public SalesController() : this(new EntityStorage()) { }

        // GET: api/Sales
        /// <summary>
        /// Get all sales.
        /// Only accessible by employees and administrator.
        /// </summary>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.
        /// If it did, it is a OkNegotiatedContentResult with a list of SaleDtos as content.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllSales())
                .Select(sale => sale.ToDto())
                .ToList());
        }

        // GET: api/Sales/5
        /// <summary>
        /// Get a sale by its id.
        /// </summary>
        /// <param name="id">The id of the sale.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.
        /// If it did, it will be a OkNegotiatedContentResult containing a SaleDto representing the sale.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get(int id)
        {
            var sale = await _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale.ToDto());
        }

        /// <summary>
        /// Get sales by the customerId.
        /// </summary>
        /// <param name="customerId">The id of the customer for which sales wants to be found.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.
        /// If it did it will be a OkNegotiatedContentResult with a (possibly empty) list of SaleDtos as content.</returns>
        [Authorize]
        public async Task<IHttpActionResult> GetFromCustomerId(string customerId)
        {
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != customerId)
            {
                return Unauthorized();
            }
            return Ok(
                (await _repo.GetAllSales())
                .Where(sale => sale.CustomerId == customerId)
                .Select(sale => sale.ToDto())
                .ToList());
        }

        // POST: api/Sales
        /// <summary>
        /// Create a sale.
        /// Only employees and administrators can create sales.
        /// </summary>
        /// <param name="value">The dto of the sale to create.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.
        /// If it did the content of the actionresult will be the sale with an id.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Post([FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            value.Sold = DateTime.Now;
            var newSaleId = await _repo.CreateSale(value.ToEntity());
            value.Id = newSaleId;
            return CreatedAtRoute("DefaultApi", new { id = newSaleId }, value);
        }

        // PUT: api/Sales/5
        /// <summary>
        /// Update a sale in the repository.
        /// Only employees and administrators has access to this.
        /// </summary>
        /// <param name="id">The id of the sale.</param>
        /// <param name="value">The updated sale information.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sale = await _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            await _repo.UpdateSale(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Sales/5
        /// <summary>
        /// Delete a sale.
        /// Only employees and administrators can do this.
        /// </summary>
        /// <param name="id">The id of the sale to delete</param>
        /// <returns>A Task resulting in an IHttpActionResult stating whether the request succeeded.</returns>
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var sale = await _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            await _repo.DeleteSale(id);
            return Ok();
        }
    }
}
