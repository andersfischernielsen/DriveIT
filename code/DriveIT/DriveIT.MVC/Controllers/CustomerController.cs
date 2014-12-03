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

        public ActionResult Create()
        {
            return View();
        }
        
        //[HttpPost]
        public async Task<ActionResult> Create(CustomerDto cust)
        {
            var customer = await controller.Post(cust) as CreatedAtRouteNegotiatedContentResult<CustomerDto>;
            return RedirectToAction("Show", customer);
        }


        /*
         
        // GET: Customer
        public ActionResult CustomerView()
        {
            return View();
        }

        public async void CreateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Create("customers", customer);
        }

        public async Task<CustomerDto> ReadCustomer(int id)
        {
            var customerToReturn = await DriveITWebAPI.Read<CustomerDto>("customers/" + id);
            return customerToReturn;
        }

        public async void UpdateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Update("customers", customer, customer.Id.Value);
        }
         **/
    }
}