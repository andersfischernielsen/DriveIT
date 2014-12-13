using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    /// <summary>
    /// Class that makes the different CRUD methods needed for the web application.
    /// All these methods only work if a customer is signed in
    /// </summary>
    public class CustomerController : AsyncController
    {
        /// <summary>
        /// creates a new CustomersController from the API that calls directly on the api controller
        /// </summary>
        private CustomersController controller = new CustomersController();

        /// <summary>
        /// GET: Customer/Show/(email)
        /// Shows details for the signed in customer.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>CustomerDto object for given customer</returns>
        public async Task<ActionResult> Show(string email)
        {
            var customer = await controller.Get(email) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        /// <summary>
        /// Gets the view with the form for editing a customerDto. The fields are already filled out with the current information.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>CustomerDto object</returns>
        [HttpGet]
        public async Task<ActionResult> Edit(string email)
        {
            var customer = await controller.Get(email) as OkNegotiatedContentResult<CustomerDto>;
            return View(customer.Content);
        }

        /// <summary>
        /// Updates the CustomerDto object with the edited data.
        /// </summary>
        /// <param name="cust"></param>
        /// <returns>A new CustomerDto object</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(CustomerDto cust)
        {
            await controller.Put(cust.Id, cust);
            // Customer to redirect to
            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        /// Deletes the signed in user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Redirects to the landing page of the application</returns>
        public async Task<ActionResult> Delete(string email)
        {
            await controller.Delete(email);
            return RedirectToAction("Index", "Home");
        }
    }
}