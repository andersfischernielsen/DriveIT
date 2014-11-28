using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CarsController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityStorage();

        // GET: api/Car
        public IHttpActionResult Get()
        {
            var cars = from c in _repo.GetAllCars()
                       select new CarDto
                       {
                           Color = c.Color,
                           Created = c.Created,
                           DistanceDriven = c.DistanceDriven,
                           Id = c.Id,
                           Make = c.Make,
                           Model = c.Model,
                           Price = c.Price,
                           Sold = c.Sold,
                           Transmission = c.Transmission,
                           Year = c.Year,
                           Fuel = (FuelType)Enum.Parse(typeof(FuelType), c.Fuel)
                       };

            return Ok(cars.ToList());
        }

        // GET: api/Car/5
        public IHttpActionResult Get(int id)
        {
            var car = _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(new CarDetailDto
            {
                Color = car.Color,
                Created = car.Created,
                DistanceDriven = car.DistanceDriven,
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Price = car.Price,
                Sold = car.Sold,
                Transmission = car.Transmission,
                Year = car.Year,
                Fuel = (FuelType)Enum.Parse(typeof(FuelType), car.Fuel),
                Drive = car.Drive,
                Mileage = car.Mileage
            });
        }

        // GET: api/Car?fuelType=Diesel
        public IHttpActionResult GetCarsByFuelType(string fuelType)
        {
            var cars = _repo.GetAllCars()
                .Where(c => string.Equals(fuelType, c.Fuel, StringComparison.OrdinalIgnoreCase))
                .Select(c => new CarDto
                {
                    Color = c.Color,
                    Created = c.Created,
                    DistanceDriven = c.DistanceDriven,
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Sold = c.Sold,
                    Transmission = c.Transmission,
                    Year = c.Year,
                    Fuel = (FuelType)Enum.Parse(typeof(FuelType), c.Fuel)
                });
            return Ok(cars);
        }

        // Get: api/Car?make=Opel
        public IHttpActionResult GetCarsByMake(string make)
        {
            return Ok(_repo.GetAllCars()
                .Where(c => string.Equals(make, c.Make, StringComparison.OrdinalIgnoreCase))
                .Select(c => new CarDto
                {
                    Color = c.Color,
                    Created = c.Created,
                    DistanceDriven = c.DistanceDriven,
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Sold = c.Sold,
                    Transmission = c.Transmission,
                    Year = c.Year,
                    Fuel = (FuelType)Enum.Parse(typeof(FuelType), c.Fuel)
                }));
        }

        // Get: api/Car?make=Opel
        public IHttpActionResult GetCarsByModel(string model)
        {
            return Ok(_repo.GetAllCars()
                .Where(c => string.Equals(model, c.Model, StringComparison.OrdinalIgnoreCase))
                .Select(c => new CarDto
                {
                    Color = c.Color,
                    Created = c.Created,
                    DistanceDriven = c.DistanceDriven,
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Sold = c.Sold,
                    Transmission = c.Transmission,
                    Year = c.Year,
                    Fuel = (FuelType)Enum.Parse(typeof(FuelType), c.Fuel)
                }));
        }

        // Get: api/Car?make=Opel&model=Zafira
        public IHttpActionResult GetCarsByMakeAndModel(string make, string model)
        {
            return
                Ok(
                    _repo.GetAllCars().Where(
                        c =>
                            string.Equals(make, c.Make, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(model, c.Model, StringComparison.OrdinalIgnoreCase))
                            .Select(c => new CarDto
                {
                    Color = c.Color,
                    Created = c.Created,
                    DistanceDriven = c.DistanceDriven,
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Sold = c.Sold,
                    Transmission = c.Transmission,
                    Year = c.Year,
                    Fuel = (FuelType)Enum.Parse(typeof(FuelType), c.Fuel)
                }));
        }

        // POST: api/Car
        public IHttpActionResult Post([FromBody]CarDetailDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Make sure Id not already in collection.
            _repo.CreateCar(value.ToCar());
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            // TODO: Make sure that REPO sets the Id of the new car.
            var uri = Url.Link("DefaultApi", new { id = value.Id });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Car/5
        public IHttpActionResult Put(int id, [FromBody]CarDetailDto value)
        {
            var car = _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            _repo.UpdateCar(id, value.ToCar());
            return Ok();
        }

        // DELETE: api/Car/5
        public IHttpActionResult Delete(int id)
        {
            _repo.DeleteCar(id);
            return Ok();
        }
    }
}
