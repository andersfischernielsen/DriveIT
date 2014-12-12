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

        public async Task<ActionResult> Show(string email)
        {
            var customer = await controller.Get(email) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string email)
        {
            var customer = await controller.Get(email) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerDto cust)
        {
            await controller.Put(cust.Id, cust);
            // Customer to redirect to
            var customer = await controller.Get(cust.Id) as OkNegotiatedContentResult<CustomerDto>;
            return RedirectToAction("Index", "Manage");
        }

        public async Task<ActionResult> Delete(string email)
        {
            await controller.Delete(email);
            return RedirectToAction("Index", "Home");
        }
    }
}