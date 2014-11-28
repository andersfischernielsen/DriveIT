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
    public class SalesController : ApiController
    {

        // GET: api/Sales
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                from sale in await _repo.GetAllSales()
                select sale.ToDto());
        }

        // GET: api/Sales/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var sale = await _repo.GetSaleWithId(id);
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
            var newSaleId = await _repo.CreateSale(await value.ToEntity());
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newSaleId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Sales/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]SaleDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repo.UpdateSale(id, await value.ToEntity());
            return Ok();
        }

        // DELETE: api/Sales/5
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
