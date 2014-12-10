using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;

namespace DriveIT.Web.ApiControllers
{
    public class CarsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public CarsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public CarsController() : this(new EntityStorage()) { }

        // GET: api/Cars
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllCars())
                .Select(car => car.ToDto())
                .ToList());
        }

        // GET: api/Cars/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car.ToDto());
        }

        // GET: api/Cars?fuelType=Diesel
        public async Task<IHttpActionResult> GetCarsByFuelType(string fuelType)
        {
            return Ok(
                (await _repo.GetAllCars())
                .Where(car => string.Equals(fuelType, car.Fuel, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.ToDto())
                .ToList());
        }

        // Get: api/Cars?make=Opel
        public async Task<IHttpActionResult> GetCarsByMake(string make)
        {
            return Ok(
                (await _repo.GetAllCars())
                .Where(car => string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.ToDto())
                .ToList());
        }

        // Get: api/Cars?Model=Zafira
        public async Task<IHttpActionResult> GetCarsByModel(string model)
        {
            return Ok(
                (await _repo.GetAllCars())
                .Where(car => string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.ToDto())
                .ToList());
        }

        // Get: api/Cars?make=Opel&model=Zafira
        public async Task<IHttpActionResult> GetCarsByMakeAndModel(string make, string model)
        {
            return Ok(
                (await _repo.GetAllCars())
                .Where(car => string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.ToDto()));
        }

        // POST: api/Cars
        [AuthorizeRoles(Role.Administrator, Role.Employee)]
        public async Task<IHttpActionResult> Post([FromBody]CarDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            var newCarId = await _repo.CreateCar(value.ToEntity());
            value.Id = newCarId;
            return CreatedAtRoute("DefaultApi", new Dictionary<string, object> { { "id", newCarId } }, value);
        }

        // PUT: api/Cars/5
        [AuthorizeRoles(Role.Administrator, Role.Employee)]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CarDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            await _repo.UpdateCar(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Cars/5
        [AuthorizeRoles(Role.Administrator, Role.Employee)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            await _repo.DeleteCar(id);
            return Ok();
        }
    }
}
