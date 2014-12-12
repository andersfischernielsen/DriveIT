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
    public class ContactRequestController : Controller
    {
        private ContactRequestsController controller = new ContactRequestsController();
        
        // GET: ContactRequest
        public async Task<ActionResult> Index(string email)
        {
            var requests = await controller.GetByUserId(email) as OkNegotiatedContentResult<List<ContactRequestDto>>;
            return View(requests.Content);
        }

        //[HttpGet]
        //public async Task<ActionResult> Create()
        //{
        //    return View();
        //}

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

            return RedirectToAction("Details", "Car", new { carId = contactRequestDto.CarId});
        }

        // GET: ContactRequest/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index", User.Identity.GetUserId());
        }
    }
}
