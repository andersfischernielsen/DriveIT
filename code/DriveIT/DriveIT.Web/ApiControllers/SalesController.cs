using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;
using Microsoft.AspNet.Identity;

namespace DriveIT.Web.ApiControllers
{
    public class SalesController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public SalesController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public SalesController() : this(new EntityStorage()) { }

        // GET: api/Sales
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                from sale in await _repo.GetAllSales()
                select sale.ToDto());
        }

        // GET: api/Sales/5
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

        [Authorize]
        public async Task<IHttpActionResult> GetFromUserId(string userId)
        {
            if (User.IsInRole(Role.Customer.ToString()) && User.Identity.GetUserId() != userId)
            {
                return Unauthorized();
            }
            return Ok((from sale in await _repo.GetAllSales()
                       where sale.CustomerId == userId
                       select sale.ToDto()).ToList());
        }

        // POST: api/Sales
        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> Post([FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSaleId = await _repo.CreateSale(value.ToEntity());
            value.Id = newSaleId;
            return CreatedAtRoute("DefaultApi", new { id = newSaleId }, value);
        }

        // PUT: api/Sales/5
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
