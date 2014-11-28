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

        public async Task<IList<CarDto>> ReadCarList()
        {
            //var downloadedString = DriveITWebAPI.Read("cars");
            //var cars = JsonConvert.DeserializeObject<CarDto[]>(downloadedString).ToList();
            //cars.ToList().ForEach(i => Console.WriteLine(i.Color));

            //return cars;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await httpClient.GetAsync("/api/cars");
                response.EnsureSuccessStatusCode();
                var cars = await response.Content.ReadAsAsync<CarDetailDto[]>(); 
                cars.ToList().ForEach(i => Console.WriteLine(i.Color));
            }
            return null;

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
