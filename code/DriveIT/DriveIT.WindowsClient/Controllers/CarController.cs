using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class CarController
    {
        public CarController()
        {
        }

        public async Task CreateCar(CarDto car)
        {
            await DriveITWebAPI.Create("cars", car);
        }

        public async Task<CarDto> ReadCar(int id)
        {
            var carToReturn = await DriveITWebAPI.Read<CarDto>("cars/" + id);
            return carToReturn;
        }

        public async Task<IList<CarDto>> ReadCarList()
        {
            var cars = await DriveITWebAPI.ReadList<CarDto>("cars");
            return cars;
        }
        public async void UpdateCar(CarDto car)
        {
            await DriveITWebAPI.Update("cars", car, car.Id.Value);
        }

        public async void DeleteCar(CarDto car)
        {
            await DriveITWebAPI.Delete<CarDto>("cars", car.Id.Value);
        }

        public async void DeleteCar(int id)
        {
            await DriveITWebAPI.Delete<CarDto>("cars", id);
        }
    }
}
