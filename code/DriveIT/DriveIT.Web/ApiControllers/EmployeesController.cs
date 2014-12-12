using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.Web.Models;

namespace DriveIT.Web.ApiControllers
{
    /// <summary>
    /// EmployeesController is responsible for viewing public information about the employees of a company using DriveIT.
    /// The controller also supports update and deletion of employees.
    /// </summary>
    public class EmployeesController : ApiController
    {
        private readonly IPersistentStorage _repo;

        /// <summary>
        /// Constructor using dependency injection.
        /// </summary>
        /// <param name="repo">The underlying repository.</param>
        public EmployeesController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Default constructor using EntityStorage as underlying repository.
        /// </summary>
        public EmployeesController() : this(new EntityStorage()) { }

        // GET: api/Employees
        /// <summary>
        /// Get all employees in the repository.
        /// </summary>
        /// <returns>A Task resulting in an IHttpActionResult which holds a list of EmployeeDtos if it succeeded.</returns>
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllEmployees())
                .Select(employee => employee.ToDto())
                .ToList());
        }

        // GET: api/Employees/5
        /// <summary>
        /// Get an employee by his id.
        /// </summary>
        /// <param name="id">The id of the employee.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded or not.
        /// If it did succeed, it contains the EmployeeDto representing the employee with the id.</returns>
        public async Task<IHttpActionResult> Get(string id)
        {
            var employee = await _repo.GetEmployeeWithId(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.ToDto());
        }

        // PUT: api/Employees/5
        /// <summary>
        /// Update an employee.
        /// Only accessible by administrators.
        /// </summary>
        /// <param name="id">The id of the employee to update.</param>
        /// <param name="value">The updated information about the employee.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the request succeeded.</returns>
        [AuthorizeRoles(Role.Administrator)]
        public async Task<IHttpActionResult> Put(string id, [FromBody]EmployeeDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _repo.GetEmployeeWithId(id) == null)
            {
                return NotFound();
            }
            await _repo.UpdateEmployee(id, value.ToEntity());
            return Ok();
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Delete an employee.
        /// Only accessible by administrators.
        /// </summary>
        /// <param name="id">The id of the employee to delete.</param>
        /// <returns>A Task resulting in an IHttpActionResult which states whether the call succeeded or not.</returns>
        [AuthorizeRoles(Role.Administrator)]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var employee = await _repo.GetEmployeeWithId(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _repo.DeleteEmployee(id);
            return Ok();
        }
    }
}
