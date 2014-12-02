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
                from employee in await _repo.GetAllEmployees()
                select employee.ToDto());
        }

        // GET: api/Employees/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var employee = await _repo.GetEmployeeWithId(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.ToDto());
        }

        // POST: api/Employees
        public async Task<IHttpActionResult> Post([FromBody]EmployeeDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value == null)
            {
                return BadRequest("Null value not allowed.");
            }
            var newEmployeeId = await _repo.CreateEmployee(value.ToEntity());
            value.Id = newEmployeeId;
            return CreatedAtRoute("DefaultApi", new { id = newEmployeeId }, value);
        }

        // PUT: api/Employees/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeDto value)
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
        public async Task<IHttpActionResult> Delete(int id)
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
