using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IPersistentStorage _repo;

        public EmployeesController(IPersistentStorage repo)
        {
            _repo = repo;
        }

        public EmployeesController() : this(new EntityStorage()) { }

        // GET: api/Employees
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                (await _repo.GetAllEmployees())
                .Select(employee => employee.ToDto())
                .ToList());
        }

        // GET: api/Employees/5
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
