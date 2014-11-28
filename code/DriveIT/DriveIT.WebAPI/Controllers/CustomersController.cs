using System.Linq;
using System.Web.Http;
using DriveIT.EntityFramework;
using DriveIT.Models;
using DriveIT.WebAPI.Models;

namespace DriveIT.WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly IPersistentStorage _repo = new EntityAdapter();

        // GET: api/Customer
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllCustomers().Select(
                c => new CustomerDto
                    {
                        Email = c.Email,
                        FirstName = c.FirstName,
                        Id = c.Id,
                        LastName = c.LastName,
                        //Todo fix
                        Phone = string.Format("{0}", c.PhoneNumber),
                        Username = c.Username
                    }
                )
            );
        }

        // GET: api/Customer/5
        public IHttpActionResult Get(int id)
        {
            var customer = _repo.GetCustomerWithId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(new CustomerDto
            {
                Email = customer.Email,
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                //Todo fix.
                Phone = string.Format("{0}", customer.PhoneNumber),
                Username = customer.Username
            });
        }

        // POST: api/Customer
        public IHttpActionResult Post([FromBody]CustomerDto value)
        {
            _repo.CreateCustomer(value.ToCustomer());
            return Ok(value);
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]CustomerDto value)
        {
        }

        // DELETE: api/Customer/5
        public IHttpActionResult Delete(int id)
        {
            _repo.DeleteCustomer(id);
            return Ok();
        }
    }
}
