﻿using System;
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
    public class CarsController : ApiController
    {

        // GET: api/Cars
        public async Task<IHttpActionResult> Get()
        {
            var cars = from car in await _repo.GetAllCars()
                       select car.ToDto();

            return Ok(cars.ToList());
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
            var cars = from car in await _repo.GetAllCars()
                       where string.Equals(fuelType, car.Fuel, StringComparison.OrdinalIgnoreCase)
                       select car.ToDto();
            return Ok(cars);
        }

        // Get: api/Cars?make=Opel
        public async Task<IHttpActionResult> GetCarsByMake(string make)
        {
            return Ok(from car in await _repo.GetAllCars()
                      where string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase)
                      select car.ToDto());
        }

        // Get: api/Cars?make=Opel
        public async Task<IHttpActionResult> GetCarsByModel(string model)
        {
            return Ok(from car in await _repo.GetAllCars()
                      where string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase)
                      select car.ToDto());
        }

        // Get: api/Cars?make=Opel&model=Zafira
        public async Task<IHttpActionResult> GetCarsByMakeAndModel(string make, string model)
        {
            return
                Ok(
                    from car in await _repo.GetAllCars()
                    where string.Equals(make, car.Make, StringComparison.OrdinalIgnoreCase) &&
                          string.Equals(model, car.Model, StringComparison.OrdinalIgnoreCase)
                    select car.ToDto());
        }

        // POST: api/Cars
        public async Task<IHttpActionResult> Post([FromBody]CarDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCarId = await _repo.CreateCar(value.ToEntity());
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newCarId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Cars/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]CarDto value)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return BadRequest("id not found!");
            }
            await _repo.UpdateCar(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Cars/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return BadRequest("Id not found");
            }
            await _repo.DeleteCar(id);
            return Ok();
        }
    }
}