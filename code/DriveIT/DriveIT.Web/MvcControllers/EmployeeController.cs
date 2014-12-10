using System.Web.Mvc;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    public class EmployeeController : AsyncController
    {
        private EmployeesController controller = new EmployeesController();
        // GET: Employee
        //public async Task<ActionResult> Index()
        //{
        //    //var emps = await controller.Get() as OkNegotiatedContentResult<List<EmployeeDto>>;
        //    //return View(emps);
        //}
    }
}