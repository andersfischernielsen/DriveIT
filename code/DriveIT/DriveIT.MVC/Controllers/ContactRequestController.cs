using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;

namespace DriveIT.MVC.Controllers
{
    public class ContactRequestController : AsyncController
    {
        // GET: ContactRequest
        public ActionResult Index()
        {
            return View();
        }

        public async void CreateRequestForContact(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Create("ContactRequests", contactRequest);
        }
        public async Task<ContactRequestDto> ReadRequestForContact(int id)
        {
            var contactRequestToReturn = await DriveITWebAPI.Read<ContactRequestDto>("ContactRequests/" + id);
            return contactRequestToReturn;
        }
        public async Task<IList<ContactRequestDto>> ReadRequestForContactList()
        {
            var contactRequests = await DriveITWebAPI.ReadList<ContactRequestDto>("ContactRequests");
            return contactRequests;
        }
        public async void UpdateRequestForContact(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Update("ContactRequests", contactRequest, contactRequest.Id.Value);
        }

        public async void DeleteRequestForContact(int id)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("ContactRequests", id);
        }
    }
}