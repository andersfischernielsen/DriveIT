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
        public  ActionResult Index()
        {
            var cars =  controller.Get().Result as OkNegotiatedContentResult<IEnumerable<CustomerDto>>;

            return View(cars.Content);
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