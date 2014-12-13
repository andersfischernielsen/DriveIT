using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    /// <summary>
    /// Class that makes the methods needed for the web client in appliance with the requirements
    /// </summary>
    public class SaleController : Controller
    {
        /// <summary>
        /// Creates controllers from the Api 
        /// </summary>
        private SalesController controller = new SalesController();
        private CarsController cc = new CarsController();

        // GET: Order
        /// <summary>
        /// Makes a tuple with car and sale Dtos. Used in the sales view to display orders for the signed in customer
        /// </summary>
        /// <returns>List of tuples of CarDto and SaleDto</returns>
        public async Task<ActionResult> Index()
        {
            var cars = await cc.Get() as OkNegotiatedContentResult<List<CarDto>>;
            var sales = await controller.Get() as OkNegotiatedContentResult<List<SaleDto>>;
            var tuple = new Tuple<IEnumerable<CarDto>, IEnumerable<SaleDto>>(cars.Content, sales.Content);
            return View(tuple);
        }

        /// <summary>
        /// Shows detials for a given sale given an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A tuple of a CarDto and a SaleDto </returns>
        public async Task<ActionResult> Details(int id)
        {
            var car = await cc.Get(id) as OkNegotiatedContentResult<CarDto>;
            var sale = await controller.Get(id) as OkNegotiatedContentResult<SaleDto>;
            var tuple = new Tuple<CarDto, SaleDto>(car.Content, sale.Content);
            return View(tuple);
        }

    }
}