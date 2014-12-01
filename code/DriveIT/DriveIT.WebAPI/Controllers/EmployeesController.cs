using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
		private readonly IPersistentStorage _repo;
		
		public EmployeesController(IPersistentStorage repo = null)
		{
			_repo = repo ?? new EntityAdapter();
		}
	
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
            var newEmployeeId = await _repo.CreateEmployee(value.ToEntity());
            var response = Request.CreateResponse(HttpStatusCode.Created, value);

            var uri = Url.Link("DefaultApi", new { id = newEmployeeId });
            response.Headers.Location = new Uri(uri);
            return ResponseMessage(response);
        }

        // PUT: api/Employees/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeDto value)
        {
            if (await _repo.GetEmployeeWithId(id) == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
