using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DriveIT_MVC.Controllers
{
    public class HomeController : Controller
    {
        private DriveITDBContext db = new DriveITDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Jacob = "Mit navn er Jacob";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(db.Employees.ToList());
        }
    }
}