using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.Web.ApiControllers;

namespace DriveIT.Web.MvcControllers
{
    /// <summary>
    /// Simple needed CRUD methods for employees in appliance with the requirements
    /// </summary>
    public class EmployeeController : AsyncController
    {
        /// <summary>
        /// Makes a new EmployeesController from the API
        /// </summary>
        private EmployeesController controller = new EmployeesController();
        
        //GET: Employee
        /// <summary>
        /// Gets a list of EmployeeDto's from all Employees
        /// </summary>
        /// <returns>List of EployeesDtos</returns>
        public async Task<ActionResult> Index()
        {
            var emps = await controller.Get() as OkNegotiatedContentResult<List<EmployeeDto>>;
            return View(emps.Content);
        }

        /// <summary>
        /// Gets details for a EmployeeDto with a given email as ID
        /// </summary>
        /// <param name="email"></param>
        /// <returns>EmployeeDto</returns>
        public async Task<ActionResult> Details(string email)
        {
            var emp = await controller.Get(email) as OkNegotiatedContentResult<EmployeeDto>;
            return View(emp.Content);
        }
    }
}