using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;

namespace DriveIT.Web.ApiControllers
{
    /// <summary>
    /// The CarsController handles the calls to the api regarding Cars.
    /// Externally it uses Dto-objects from the DriveIT.Models assembly.
    /// Internally it uses Entity-objects so it can communicate with the DriveIT.EntityFramework-assembly.
    /// </summary>
    public class CarsController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Creates a new CarsController.
        /// 
        /// Testing constructor. Uses dependency injection to enable testability.
        /// </summary>
        /// <param name="repo">The Repository used to send and receive data</param>
        public CarsController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor. Creates connection to database through a EntityStorage.
        /// </summary>
        public CarsController() : this(new EntityStorage()) { }

        // GET: api/Cars
        /// <summary>
        /// HTTP Get. Gets all cars from the repository and returns them as a list of CarDto's
        /// </summary>
        /// <returns>A Task resulting in an IHttpActionResult which, if it succeeds holds a list of CarDto's.</returns>
        public async Task<IHttpActionResult> Get()
        {
            var cars = await _repo.GetAllCars();
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
                dtos.Add(car.ToDto());
            }
            return Ok(dtos);
        }

        // GET: api/Cars/5
        /// <summary>
        /// Gets a CarDto by its Id.
        /// </summary>
        /// <param name="id">The id of the car which is returned.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            var car = await _repo.GetCarWithId(id);
            if (car == null)
            {
                return NotFound();
            }
            car.Sold = await _repo.GetSaleByCarId(car.Id) != null;
            return Ok(car.ToDto());
        }

        /// <summary>
        /// Returns CarDto's of the Cars which has not been sold, or has been sold within the last 5 days.
        /// </summary>
        /// <returns>A Task resulting in a List of CarDto's</returns>
        [Route("api/cars/webcarlist")]
        [HttpGet]
        public async Task<List<CarDto>> WebCarList()
        {
            var cars = await _repo.GetAllCars();
            var dtos = new List<CarDto>();
            foreach (var car in cars)
            {
                var sale = await _repo.GetSaleByCarId(car.Id);
                // Check if car has been sold, and if it has, if it is less than 5 days ago.
                if (sale == null || DateTime.Now.Subtract(sale.DateOfSale).Days < 5)
                {
                    car.Sold = sale != null;
                    dtos.Add(car.ToDto());
                }
            }
            return dtos;
        }

        // POST: api/Cars
        /// <summary>
        /// Create a new car in the database.
        /// 
        /// This method can only be called by Administrators and Employees.
        /// </summary>
        /// <param name="value">The CarDto of the car that should be created in storage.</param>
        /// <returns>A Task resulting in an IHttpActionResult with information about whether the call succeeded or not.
        /// If it succeeded the content will contain the CarDto, where an Id has been added.</returns>
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
            value.Created = DateTime.Now;
            var newCarId = await _repo.CreateCar(value.ToEntity());
            value.Id = newCarId;
            return CreatedAtRoute("DefaultApi", new Dictionary<string, object> { { "id", newCarId } }, value);
        }

        // PUT: api/Cars/5
        /// <summary>
        /// Update a Car in storage.
        /// </summary>
        /// <param name="id">The Id of the car to update.</param>
        /// <param name="value">A CarDto holding the updated information of the car.</param>
        /// <returns>A Task resulting in an IHttpActionResult which says whether the request succeeded or not.</returns>
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
        /// <summary>
        /// Delete a Car in storage.
        /// </summary>
        /// <param name="id">The Id of the car that should be deleted.</param>
        /// <returns>A Task resulting in an IHttpActionResult which has information about whether the request has succeeded or not.</returns>
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
