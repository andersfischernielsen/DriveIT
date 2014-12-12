using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class CustomerController
    {
        /// <summary>
        /// An empty constructor for the CustomerController.
        /// </summary>
        public CustomerController()
        {
        }
        /// <summary>
        /// Creates a Customer DTO object in the API.
        /// </summary>
        /// <param name="customer">A Customer DTO</param>
        /// <param name="password">The desired password for the customer</param>
        /// <returns>Returns the newly created Customer DTO from the database</returns>
        public async Task CreateCustomer(CustomerDto customer, string password)
        {
            var registerModel = new RegisterViewModel
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.Phone,
                ConfirmPhoneNumber = customer.Phone,
                Password = password,
                ConfirmPassword = password,
                Role = Role.Customer
            };
            await DriveITWebAPI.Create("account/register", registerModel);
        }
        /// <summary>
        /// Reads a specific Customer DTO object from the API.
        /// </summary>
        /// <param name="email">The email of the desired Customer DTO</param>
        /// <returns>Returns the Customer with the respective email from the database</returns>
        public async Task<CustomerDto> ReadCustomer(string email)
        {
            string search = "?id=" + email;
            var customerToReturn = await DriveITWebAPI.Read<CustomerDto>("customers/" + search);
            return customerToReturn;
        }
        /// <summary>
        /// Reads the list of Customer DTO objects from the API.
        /// </summary>
        /// <returns>Returns the list of Customer DTO's from the database</returns>
        public async Task<IList<CustomerDto>> ReadCustomerList()
        {
            var customers = await DriveITWebAPI.ReadList<CustomerDto>("customers");
            return customers;
        }
        /// <summary>
        /// Updates the Customer DTO sent to the API.
        /// </summary>
        /// <param name="customer">The Customer DTO to be updated</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task UpdateCustomer(CustomerDto customer)
        {
            string search = "?id=" + customer.Email;
            await DriveITWebAPI.Update("customers/" + search, customer);
        }
        /// <summary>
        /// Deletes the selected Customer DTO from the API.
        /// </summary>
        /// <param name="customer">The Customer DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteCustomer(CustomerDto customer)
        {
            string search = "?id=" + customer.Email;
            await DriveITWebAPI.Delete<CustomerDto>("customers/" + search);
        }
        /// <summary>
        /// Deletes the selected Customer DTO from the API with the given id.
        /// </summary>
        /// <param name="email">The email of the Customer DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteCustomer(int email)
        {
            string search = "?id=" + email;
            await DriveITWebAPI.Delete<CustomerDto>("customers/" + search);
        }
    }
}
