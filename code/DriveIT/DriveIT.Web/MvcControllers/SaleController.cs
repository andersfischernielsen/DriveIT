using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    namespace DriveIT.MVC.Controllers
    {
        public class SaleController : Controller
        {
            private SalesController controller = new SalesController();
            private CarsController cc = new CarsController();
            // GET: Order
            public async Task<ActionResult> Index()
            {

                var cars = await cc.Get() as OkNegotiatedContentResult<List<CarDto>>;
                var sales = await controller.Get() as OkNegotiatedContentResult<List<SaleDto>>;
                var tuple = new Tuple<IEnumerable<CarDto>, IEnumerable<SaleDto>>(cars.Content, sales.Content);
                return View(tuple);
            }

        }
    }
}