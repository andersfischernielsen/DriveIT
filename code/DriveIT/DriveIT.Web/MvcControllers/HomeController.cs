using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    public class HomeController : Controller
    {
        private CarsController controller = new CarsController();

        public async Task<ActionResult> Index()
        {
            var cars = await controller.WebCarList();
            return View(cars);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

    }
}