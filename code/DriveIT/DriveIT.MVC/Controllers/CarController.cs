using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;

namespace DriveIT.MVC.Controllers
{
    public class CarController : AsyncController
    {

        // GET: Car/CarSearch
        public ActionResult CarSearch()
        {
            return View();
        }

        public async Task<IList<CarDto>> getAllCars()
        {
            var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars");
            return carsToReturn;
        }

        public async Task<CarDto> getSingleCar(int Id)
        {
            var carToReturn = await DriveITWebAPI.Read<CarDto>("Cars/" + Id);
            return carToReturn;
        }

        public async Task<IList<CarDto>> GetCarsByFuelType(string fuelType)
        {
            var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?fuelType=" + fuelType);
            return carsToReturn;
        }

        public async Task<IList<CarDto>> GetCarsByMake(string make)
        {
            var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?make=" + make);
            return carsToReturn;
        }

        public async Task<IList<CarDto>> GetCarsByModel(string model)
        {
            var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?model=" + model);
            return carsToReturn;
        }

        public async Task<IList<CarDto>> GetCarsByMakeAndModel(string make, string model)
        {
            var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?make=" + make + "&model=" + model);
            return carsToReturn;
        }
    }
}