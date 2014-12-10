using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    public class ContactRequestController : Controller
    {
        private ContactRequestsController controller = new ContactRequestsController();
        
        // GET: ContactRequest
        public async Task<ActionResult> Index()
        {
            var requests = await controller.Get() as OkNegotiatedContentResult<List<ContactRequestDto>>;
            return View(requests.Content);
        }

        // GET: ContactRequest/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = await controller.Get(id) as OkNegotiatedContentResult<ContactRequestDto>;
            return View(request.Content);
        }

        //// GET: ContactRequest/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ContactRequest/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ContactRequest/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await controller.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
