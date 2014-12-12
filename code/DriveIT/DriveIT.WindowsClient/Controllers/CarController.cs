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

        public async Task<CarDto> CreateCar(CarDto car)
        {
            return await DriveITWebAPI.Create("cars", car);
        }

        public async Task<CarDto> ReadCar(int id)
        {
            return await DriveITWebAPI.Read<CarDto>("cars/" + id);
        }

        public async Task<IList<CarDto>> ReadCarList()
        {
            return await DriveITWebAPI.ReadList<CarDto>("cars");
        }
        public async Task UpdateCar(CarDto car)
        {
            await DriveITWebAPI.Update("cars/" + car.Id, car);
        }

        public async Task DeleteCar(CarDto car)
        {
            await DriveITWebAPI.Delete<CarDto>("cars/" + car.Id);
        }

        public async Task DeleteCar(int id)
        {
            await DriveITWebAPI.Delete<CarDto>("cars/" + id);
        }
    }
}
