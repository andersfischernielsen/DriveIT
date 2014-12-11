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
            var cars = await _repo.GetAllCars();
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
            }
            return Ok(dtos);
        }

        // GET: api/Cars/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
            return Ok(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
        }

        internal async Task<List<CarDto>> WebCarList()
        {
            var cars = await _repo.GetAllCars();
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                var sale = await _repo.GetSaleByCarId(car.Id);
                if (sale == null || DateTime.Now.Subtract(sale.DateOfSale).Days < 5)
                {
                    car.Sold = sale != null;
                    dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
                }
            }
            return dtos;
        }

        // GET: api/Cars?fuelType=Diesel
        public async Task<IHttpActionResult> GetCarsByFuelType(string fuelType)
        {
            var cars =
                (await _repo.GetAllCars())
                .Where(car => string.Equals(fuelType, car.Fuel.ToString(), StringComparison.OrdinalIgnoreCase));
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
            }
            return Ok(dtos);
        }

        // Get: api/Cars?make=Opel
        public async Task<IHttpActionResult> GetCarsByMake(string make)
        {
            var cars = (await _repo.GetAllCars())
                .Where(car => string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase));
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
            }
            return Ok(dtos);
        }

        // Get: api/Cars?Model=Zafira
        public async Task<IHttpActionResult> GetCarsByModel(string model)
        {
            var cars = (await _repo.GetAllCars())
                .Where(car => string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase));
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
            }
            return Ok(dtos);
        }

        // Get: api/Cars?make=Opel&model=Zafira
        public async Task<IHttpActionResult> GetCarsByMakeAndModel(string make, string model)
        {
            var cars = (await _repo.GetAllCars())
                .Where(car => string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase));
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto(await _repo.GetImagePathsForCar(car.Id)));
            }
            return Ok(dtos);
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
            foreach (var imagePath in value.ToImagePaths())
            {
                await _repo.CreateImagePath(imagePath);
            }
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
            // Remove all imagePaths.
            foreach (var imagePath in await _repo.GetImagePathsForCar(id))
            {
                await _repo.RemoveImagePath(imagePath.Id);
            }
            // Add new/updated imagePaths.
            foreach (var imagePath in value.ToImagePaths())
            {
                await _repo.CreateImagePath(imagePath);
            }
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
