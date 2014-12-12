using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    /// <summary>
    /// A controller which creates the strings to CRUD cars in the DriveITWebAPI class
    /// </summary>
    public class CarController
    {
        /// <summary>
        /// An empty constructor for the CarController.
        /// </summary>
        public CarController()
        {
        }
        /// <summary>
        /// Creates a Car DTO object in the API.
        /// </summary>
        /// <param name="car">A Car DTO</param>
        /// <returns>Returns the newly created Car DTO from the database</returns>
        public async Task<CarDto> CreateCar(CarDto car)
        {
            return await DriveITWebAPI.Create("cars", car);
        }
        /// <summary>
        /// Reads a specific Car DTO object from the API.
        /// </summary>
        /// <param name="id">The id of the desired Car DTO</param>
        /// <returns>Returns the Car with the respective id from the database</returns>
        public async Task<CarDto> ReadCar(int id)
        {
            return await DriveITWebAPI.Read<CarDto>("cars/" + id);
        }
        /// <summary>
        /// Reads the list of Car DTO objects from the API.
        /// </summary>
        /// <returns>Returns the list of Car DTO's from the database</returns>
        public async Task<IList<CarDto>> ReadCarList()
        {
            return await DriveITWebAPI.ReadList<CarDto>("cars");
        }
        /// <summary>
        /// Updates the Car DTO sent to the API.
        /// </summary>
        /// <param name="car">The Car DTO to be updated</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task UpdateCar(CarDto car)
        {
            await DriveITWebAPI.Update("cars/" + car.Id, car);
        }
        /// <summary>
        /// Deletes the selected Car DTO from the API.
        /// </summary>
        /// <param name="car">The Car DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteCar(CarDto car)
        {
            await DriveITWebAPI.Delete<CarDto>("cars/" + car.Id);
        }
        /// <summary>
        /// Deletes the selected Car DTO from the API with the given id.
        /// </summary>
        /// <param name="id">The id of the Car DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteCar(int id)
        {
            await DriveITWebAPI.Delete<CarDto>("cars/" + id);
        }
    }
}
