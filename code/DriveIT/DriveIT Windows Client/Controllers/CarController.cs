using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            var t = ReadCarList().Result;
            Console.WriteLine(t);
            //CreateCar(t[1]);
            //t = ReadCarList().Result;
            //Console.WriteLine(t.Count);
        }   

        public async void CreateCar(CarDetailDto car)
        {
            await DriveITWebAPI.Create("cars", car);
        }

        public async Task<CarDetailDto> ReadCar(int id)
        {
            var cars = await DriveITWebAPI.Read<CarDetailDto>("cars/" + id);
            return cars.FirstOrDefault();
        }

        public async Task<IList<CarDetailDto>> ReadCarList()
        {
            var cars = DriveITWebAPI.Read<CarDetailDto>("cars");
            return await cars;
        }
        public void UpdateCar(CarViewModel car)
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(CarViewModel car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
