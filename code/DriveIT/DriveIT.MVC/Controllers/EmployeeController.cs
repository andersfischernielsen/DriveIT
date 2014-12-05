using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.WebAPI.Controllers;

namespace DriveIT.MVC.Controllers
{
    public class EmployeeController : AsyncController
    {
        private EmployeesController controller = new EmployeesController();
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            //var emps = await controller.Get() as OkNegotiatedContentResult<List<EmployeeDto>>;
            //return View(emps);
        }

        public async Task<IList<EmployeeDto>> ReadEmployeeList()
        {
            var employees = await DriveITWebAPI.ReadList<EmployeeDto>("Employees");
            return employees;
        }
    }
}