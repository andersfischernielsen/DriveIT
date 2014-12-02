using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;

namespace DriveIT.MVC.Controllers
{
    public class EmployeeController : AsyncController
    {
        // GET: Employee
        public ActionResult EmployeeView()
        {
            return View();
        }

        public async Task<IList<EmployeeDto>> ReadEmployeeList()
        {
            var employees = await DriveITWebAPI.ReadList<EmployeeDto>("Employees");
            return employees;
        }
    }
}