using System.Linq;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class SalesController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityAdapter();

        // GET: api/Sales
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllSales().Select(s =>
                new SaleDto
                {
                    CarId = s.Car.Id,
                    CustomerId = s.Customer.Id,
                    Price = s.Price,
                    Sold = s.DateOfSale
                }));
        }

        // GET: api/Sales/5
        public IHttpActionResult Get(int id)
        {
            var sale = _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(new SaleDto
            {
                CarId = sale.Car.Id,
                CustomerId = sale.Customer.Id,
                Price = sale.Price,
                Sold = sale.DateOfSale
            });
        }

        // POST: api/Sales
        public IHttpActionResult Post([FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.CreateSale(value.ToSale(_repo));
            return Ok();
        }

        // PUT: api/Sales/5
        public IHttpActionResult Put(int id, [FromBody]SaleDto value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //TODO: PUT implementation
            return Ok();
        }

        // DELETE: api/Sales/5
        public IHttpActionResult Delete(int id)
        {
            var sale = _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            _repo.DeleteSale(sale);
            return Ok();
        }
    }
}
