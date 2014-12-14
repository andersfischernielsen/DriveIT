using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;
using DriveIT.Web.Models;
using Microsoft.Ajax.Utilities;

namespace DriveIT.Web.MvcControllers
{
    public class CarController : AsyncController
    {

        private CarsController carController = new CarsController();
        private CommentsController commentsController = new CommentsController();
        private ContactRequestsController rc = new ContactRequestsController();
       
        /// <summary>
        /// This method gets all the cars, fill in into the viewbag properties to be used for the select boxes and
        /// sort out the cars according to the parameters. A list of cars containing the result of the search will thereafter
        /// be returned to the view for displaying the information.
        /// </summary>
        /// <param name="fuelType">fueltype of the car to find.</param>
        /// <param name="make">make of the car to find.</param>
        /// <param name="model">model of the car to find.</param>
        /// <returns>~Views/Car/Index.cshtml</returns>
        public async Task<ActionResult> Index(String fuelType, String make, String model)
        {
            //Select all cars
            IEnumerable<CarDto> carList = await carController.WebCarList();

            //Get list of fuel types
            var fuelList = from c in carList
                           orderby c.Fuel
                           select c.Fuel;

            //Get list of make
            var makeList = from c in carList
                           orderby c.Make
                           select c.Make;

            //Get list of models
            var modelList = from c in carList
                            orderby c.Model
                            select c.Model;

            //Set distinct list of fuel types in ViewBag property
            ViewBag.fuelType = new SelectList(fuelList.Distinct());

            //Set distinct list of makes in ViewBag property
            ViewBag.make = new SelectList(makeList.Distinct());

            //Set distinct list of models in ViewBag property
            ViewBag.model = new SelectList(modelList.Distinct());

            //Search records of fuel type
            if (!String.IsNullOrEmpty(fuelType))
            {
                carList = carList.Where(c => fuelType.Contains(c.Fuel.ToString()));
            }

            //Search records of make
            if (!String.IsNullOrEmpty(make))
            {
                carList = carList.Where(c => c.Make == make);
            }

            //Search records of models
            if (!String.IsNullOrEmpty(model))
            {
                carList = carList.Where(c => c.Model == model);
            }

            return View(carList);
        }

        /// <summary>
        /// Gets the car according to the carId, gets the comments according to the carId and a list of contact requests.
        /// A viewmodel is filled with these information and being given to the view for displaying the information.
        /// </summary>
        /// <param name="carId">carId of the specific car.</param>
        /// <returns>~Views/Car/Details.cshtml</returns>
        public async Task<ActionResult> Details(int carId)
        {
            var car = await carController.Get(carId) as OkNegotiatedContentResult<CarDto>;
            var comments = await commentsController.GetByCarId(carId) as OkNegotiatedContentResult<List<CommentDto>>;
            var requests = await rc.Get() as OkNegotiatedContentResult<List<ContactRequestDto>>;

            var viewModel = new CarCommentViewModel();
            viewModel.Car = car.Content;

            if (comments != null)
            {
                viewModel.Comments = comments.Content;
            }
            else
            {
                viewModel.Comments = new List<CommentDto>();
            }
            viewModel.ContactRequest = requests.Content;
            return View(viewModel);
        }
    }
}