using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    /// <summary>
    /// This class is responsible for two "mainly" static pages. The landing page and the about page.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Makes a CarsController in order to get a list of newest cars
        /// </summary>
        private CarsController controller = new CarsController();

        /// <summary>
        /// Serves a view with a list of unsold cars or cars sold within the last 5 days. 
        /// Just something to display on the landing page.
        /// </summary>
        /// <returns>List of cars</returns>
        public async Task<ActionResult> Index()
        {
            var cars = await controller.WebCarList();
            return View(cars);
        }

        /// <summary>
        /// Serves a static view with some static information about the application
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

    }
}