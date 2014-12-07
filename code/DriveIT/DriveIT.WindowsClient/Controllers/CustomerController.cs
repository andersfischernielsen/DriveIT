using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class CustomerController
    {
        public CustomerController()
        {
        }

        public async Task CreateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Create("customers", customer);
        }

        public async Task<CustomerDto> ReadCustomer(string email)
        {
            string search = "?id=" + email;
            var customerToReturn = await DriveITWebAPI.Read<CustomerDto>("customers/" + search);
            return customerToReturn;
        }

        public async Task<IList<CustomerDto>> ReadCustomerList()
        {
            var customers = await DriveITWebAPI.ReadList<CustomerDto>("customers");
            return customers;
        }
        public async Task UpdateCustomer(CustomerDto customer)
        {
            string search = "?id=" + customer.Email;
            await DriveITWebAPI.Update("customers/" + search, customer);
        }

        public async Task DeleteCustomer(CustomerDto customer)
        {
            string search = "?id=" + customer.Email;
            await DriveITWebAPI.Delete<CustomerDto>("customers/" + search);
        }

        public async Task DeleteCustomer(int email)
        {
            string search = "?id=" + email;
            await DriveITWebAPI.Delete<CustomerDto>("customers/" + search);
        }
    }
}
