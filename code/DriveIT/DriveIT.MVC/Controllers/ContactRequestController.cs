using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DriveIT.MVC.Controllers
{
    public class ContactRequestController : Controller
    {
        // GET: ContactRequest
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContactRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactRequest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactRequest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
