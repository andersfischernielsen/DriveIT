using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;
using Microsoft.Ajax.Utilities;

namespace DriveIT.MVC.Controllers
{
    public class CarController : AsyncController
    {

        private CarsController controller = new CarsController();

        public async Task<ActionResult> Index()
        {
            var elements = await controller.Get() as OkNegotiatedContentResult<List<CarDto>>;
            return View(elements.Content);
        }

        [HttpGet]
        public async Task<ViewResult> Index(string model)
        {
            //Select all cars
            var elements = await controller.Get() as OkNegotiatedContentResult<List<CarDto>>;

            var carList = from e in elements.Content
                select e;

            //Get list of models
            var modelList = from c in carList
                               orderby c.Model
                               select c.Model;

            //Set distinct list of models in ViewBag property
            ViewBag.Models = new SelectList(modelList.Distinct());

            //Search records of models
            
                carList = carList.Where(e => e.Model.Equals(model));
            

            return View(carList);
        }

        public async Task<ActionResult> Details(int carId)
        {
            var car = await controller.Get(carId) as OkNegotiatedContentResult<CarDto>;
            return View(car.Content);
        }

        //public async Task<IList<CarDto>> GetAllCars()
        //{
        //    var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars");
        //    return carsToReturn;
        //}

        //public async Task<CarDto> GetSingleCar(int Id)
        //{
        //    var carToReturn = await DriveITWebAPI.Read<CarDto>("Cars/" + Id);
        //    return carToReturn;
        //}

        //public async Task<IList<CarDto>> GetCarsByFuelType(string fuelType)
        //{
        //    var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?fuelType=" + fuelType);
        //    return carsToReturn;
        //}

        //public async Task<IList<CarDto>> GetCarsByMake(string make)
        //{
        //    var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?make=" + make);
        //    return carsToReturn;
        //}

        //public async Task<IList<CarDto>> GetCarsByModel(string model)
        //{
        //    var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?model=" + model);
        //    return carsToReturn;
        //}

        //public async Task<IList<CarDto>> GetCarsByMakeAndModel(string make, string model)
        //{
        //    var carsToReturn = await DriveITWebAPI.ReadList<CarDto>("Cars?make=" + make + "&model=" + model);
        //    return carsToReturn;
        //}
    }
}