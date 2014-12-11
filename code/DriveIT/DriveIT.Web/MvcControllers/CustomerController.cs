using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
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

        public async Task<ActionResult> Show(string id)
        {
            var customer = await controller.Get(id) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        //[System.Web.Mvc.HttpPost]
        //public async Task<ActionResult> Create(CustomerDto cust)
        //{
        //    var customer = await controller.Post(cust) as CreatedAtRouteNegotiatedContentResult<CustomerDto>;
        //    return RedirectToAction("Show", customer.Content);
        //}

        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var customer = await controller.Get(id) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Edit(CustomerDto cust)
        {
            await controller.Put(cust.Id, cust);
            // Customer to redirect to
            var customer = await controller.Get(cust.Id) as OkNegotiatedContentResult<CustomerDto>;
            return RedirectToAction("Show", customer.Content);
        }

        public async Task<ActionResult> Delete(string id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index");
        }
    }
}