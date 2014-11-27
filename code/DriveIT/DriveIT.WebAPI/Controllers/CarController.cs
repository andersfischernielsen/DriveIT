using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using DriveIT.Entities;

namespace DriveIT.WebAPI.Controllers
{
    public class CarController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityAdapter();

        // GET: api/Car
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllCars());
        }

        // GET: api/Car/5
        public IHttpActionResult Get(int id)
        {
            var car = _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // GET: api/Car?fuelType=Diesel
        public IHttpActionResult GetCarsByFuelType(string fuelType)
        {
            return Ok(_repo.GetAllCars().Where(c => string.Equals(fuelType, c.Fuel, StringComparison.OrdinalIgnoreCase)));
        }

        // Get: api/Car?make=Opel
        public IHttpActionResult GetCarsByMake(string make)
        {
            return Ok(_repo.GetAllCars().Where(c => string.Equals(make, c.Make, StringComparison.OrdinalIgnoreCase)));
        }

        // Get: api/Car?make=Opel
        public IHttpActionResult GetCarsByModel(string model)
        {
            return Ok(_repo.GetAllCars().Where(c => string.Equals(model, c.Model, StringComparison.OrdinalIgnoreCase)));
        }

        // Get: api/Car?make=Opel&model=Zafira
        public IHttpActionResult GetCarsByMakeAndModel(string make, string model)
        {
            return
                Ok(
                    _repo.GetAllCars().Where(
                        c =>
                            string.Equals(make, c.Make, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(model, c.Model, StringComparison.OrdinalIgnoreCase)));
        }

        // POST: api/Car
        public IHttpActionResult Post([FromBody]Car value)
        {
            return new StatusCodeResult(HttpStatusCode.NotImplemented, this);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //// TODO: Make sure Id not already in collection.
            //_repo.GetAllCars().Add(value);
            //var response = Request.CreateResponse(HttpStatusCode.Created, value);

            //// TODO: Make sure that REPO sets the Id of the new car.
            //var uri = Url.Link("DefaultApi", new { id = value.Id });
            //response.Headers.Location = new Uri(uri);
            //return ResponseMessage(response);
        }

        // PUT: api/Car/5
        public IHttpActionResult Put(int id, [FromBody]Car value)
        {
            return new StatusCodeResult(HttpStatusCode.NotImplemented, this);
            //var car = _repo.GetCarWithId(id);
            //if (car == null)
            //{
            //    return NotFound();
            //}
            //_repo.Remove(car);
            //_repo.Add(value);
            //return Ok();
        }

        // DELETE: api/Car/5
        public IHttpActionResult Delete(int id)
        {
            return new StatusCodeResult(HttpStatusCode.NotImplemented, this);
            //return Ok();
        }
    }
}
