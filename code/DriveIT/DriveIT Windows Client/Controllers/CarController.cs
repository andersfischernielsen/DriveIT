using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;
using Newtonsoft.Json;

namespace DriveIT_Windows_Client.Controllers
{
    public class CarController
    {
        public CarController()
        {
            TestMethod();
        }

        private void TestMethod()
        {
            var t = ReadCarList().Result;
            Console.WriteLine(t.Count);
            try
            {
                CreateCar(t[0]);
            }
            catch (Exception)
            {
                CreateCar(new CarDto()
                {
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 10000,
                    Model = "A8",
                    Make = "Audi",
                    Price = 200000
                });
            }
            Thread.Sleep(5000);
            t = ReadCarList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + ReadCar(t[t.Count - 1].Id.Value).Result.Color);
            int id = t[0].Id.Value;
            UpdateCar(new CarDto()
            {
                Color = "Silver",
                Created = DateTime.Now.AddDays(1),
                DistanceDriven = 12000,
                Model = "Swhifts",
                Make = "Suzuki",
                Price = 10000,
                Id = id
            });
            Thread.Sleep(5000);
            t = ReadCarList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + ReadCar(t[t.Count - 1].Id.Value).Result.Color);

            DeleteCar(t[0].Id.Value);
            Thread.Sleep(5000);
            t = ReadCarList().Result;
            Console.WriteLine(t.Count);
        }

        public async void CreateCar(CarDto car)
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
