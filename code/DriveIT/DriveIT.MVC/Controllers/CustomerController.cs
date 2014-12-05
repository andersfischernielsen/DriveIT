using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;

namespace DriveIT.MVC.Controllers
{
    public class CustomerController : AsyncController
    {
        private CustomersController controller = new CustomersController();
        
        //GET: Customer
        public async Task<ActionResult> Index()
        {
            var customers = await controller.Get() as OkNegotiatedContentResult<List<CustomerDto>>;
            return View(customers.Content);
        }

        public async Task<ActionResult> Show(int id)
        {
            var customer = await controller.Get(id) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Create(CustomerDto cust)
        {
            var customer = await controller.Post(cust) as CreatedAtRouteNegotiatedContentResult<CustomerDto>;
            return RedirectToAction("Show", customer.Content);
        }

        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await controller.Get(id) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Edit(CustomerDto cust)
        {
            await controller.Put(cust.Id.Value, cust);
            // Customer to redirect to
            var customer = await controller.Get(cust.Id.Value) as OkNegotiatedContentResult<CustomerDto>;
            return RedirectToAction("Show", customer.Content);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index");
        }
    }
}