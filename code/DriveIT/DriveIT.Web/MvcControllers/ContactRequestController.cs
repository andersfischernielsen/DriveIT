using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;

namespace DriveIT.Web.MvcControllers
{
    /// <summary>
    /// This class is responsible for the CRUD methods needed for making ContactRequests
    /// </summary>
    public class ContactRequestController : Controller
    {
        // Makes an instance of the ContactRequestsController from the API
        private ContactRequestsController controller = new ContactRequestsController();
        // Makes an istance of the CarsController from the API
        private CarsController cc = new CarsController();
        
        /// <summary>
        /// GET: ContactRequest/
        /// Makes a tuple of cars and requests. Only requests with the given email as owner are returned.
        /// The tuple is used in the view to display data from the two DTO's.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>tuple of CarDto and ContactRequest</returns>
        public async Task<ActionResult> Index(string email)
        {
            var cars = await cc.Get() as OkNegotiatedContentResult<List<CarDto>>;
            var requests = await controller.GetByUserId(email) as OkNegotiatedContentResult<List<ContactRequestDto>>;
            var tuple = new Tuple<IEnumerable<CarDto>, IEnumerable<ContactRequestDto>>(cars.Content, requests.Content); 
            return View(tuple);

        }

        /// <summary>
        /// POST: ContactRequest/Create
        /// Takes a carId and creates a ContactRequestDto with the current user and Datetime as values.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing, since it's a post method. Creates a Dto in the database though</returns>
        [AuthorizeRoles(Role.Customer)]
        [HttpPost]
        public async Task<ActionResult> Create(int id)
        {            
            var contactRequestDto = new ContactRequestDto
            {
                CarId = id,
                CustomerId = User.Identity.GetUserId(),
                Requested = DateTime.Now
            };
            await controller.Post(contactRequestDto);

            return RedirectToAction("Details", "Car", new { carId = contactRequestDto.CarId });
        }


        /// <summary>
        /// GET: ContactsRequest/Delete/5
        /// Deletes a given entry in the database and returns to the user view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing. Deletes and entry and redirects</returns>
        public async Task<ActionResult> Delete(int id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index", "ContactRequest", new { email = User.Identity.GetUserId() });
        }
    }
}
