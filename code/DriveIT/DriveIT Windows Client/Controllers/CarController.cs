using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            var t = ReadCarList();
            //CreateCar(null);
            //ReadCarList();
        }   

        public void CreateCar(CarDetailDto car)
        {
            DriveITWebAPI.Create("cars", car);
        }

        public CarDetailDto ReadCar(int id)
        {
            //var downloadedString = DriveITWebAPI.Read("cars/" + id);
            //var car = JsonConvert.DeserializeObject<CarDto>(downloadedString);

            //var detailCar =new CarDetailDto()
            //{
            //    Color = car.Color,
            //    Created = car.Created,
            //    DistanceDriven = car.DistanceDriven,
            //    Fuel = car.Fuel,
            //    Make = car.Make,
            //    Model = car.Model,
            //};
            //return detailCar;
            return null;
        }

        public async Task<IList<CarDetailDto>> ReadCarList()
        {
            var cars = await DriveITWebAPI.Read<CarDetailDto>("cars");
            return cars;

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
