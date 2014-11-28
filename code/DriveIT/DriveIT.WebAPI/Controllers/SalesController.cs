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
    public class SalesController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityStorage();

        // GET: api/Sales
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllSales()
                .Select(s => s.ToDto()));
        }

        // GET: api/Sales/5
        public IHttpActionResult Get(int id)
        {
            var sale = _repo.GetSaleWithId(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale.ToDto());
        }

        // POST: api/Sales
        public async Task<IHttpActionResult> Post([FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSaleId = await _repo.CreateSale(value.ToEntity(_repo));
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newSaleId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Sales/5
        public IHttpActionResult Put(int id, [FromBody]SaleDto value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.UpdateSale(id, value.ToEntity(_repo));
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
