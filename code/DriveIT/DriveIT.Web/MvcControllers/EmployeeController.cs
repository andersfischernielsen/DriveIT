using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    public class EmployeeController : AsyncController
    {
        private EmployeesController controller = new EmployeesController();
        
        //GET: Employee
        public async Task<ActionResult> Index()
        {
            var emps = await controller.Get() as OkNegotiatedContentResult<List<EmployeeDto>>;
            return View(emps.Content);
        }

      //  [Route("Employee/Details/{id}")]
        public async Task<ActionResult> Details(string id)
        {
            var emp = await controller.Get(id) as OkNegotiatedContentResult<EmployeeDto>;
            return View(emp.Content);
        }
    }
}