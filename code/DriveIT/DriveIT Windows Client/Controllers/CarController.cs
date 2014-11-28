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
        static string apiUrl = @"http://localhost:5552";
        public CarController()
        {
            testMethods();
        }

        private void testMethods()
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

            DeleteCar(t[0].Id.Value);
            Thread.Sleep(5000);
            t = ReadCarList().Result;
            Console.WriteLine(t.Count);

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
            Console.WriteLine(t[t.Count - 1].Color);
        }

        public async void CreateCar(CarDto car)
        {
            await DriveITWebAPI.Create("cars", car);
        }

        public async Task<CarDto> ReadCar(int id)
        {
            var cars = await DriveITWebAPI.Read<CarDto>("cars/" + id);
            return cars.FirstOrDefault();
        }

        public async Task<IList<CarDto>> ReadCarList()
        {
            var cars = await DriveITWebAPI.Read<CarDto>("cars");
            return cars;
        }
        public async void UpdateCar(CarDto car)
        {
            await DriveITWebAPI.Update("cars", car, car.Id.Value);
        }

        public void DeleteCar(CarViewModel car)
        {
            throw new NotImplementedException();
        }

        public async void DeleteCar(int id)
        {
            await DriveITWebAPI.Delete<CarDto>("cars", id);
        }
    }
}
