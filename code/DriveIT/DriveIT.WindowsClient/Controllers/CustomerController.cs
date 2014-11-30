using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT_Windows_Client.Controllers
{
    public class CustomerController
    {
        public CustomerController()
        {
            TestMethod();
        }

        private void TestMethod()
        {
            var t = ReadCustomerList().Result;
            Console.WriteLine(t.Count);
            try
            {
                CreateCustomer(t[0]);
            }
            catch (Exception)
            {
                CreateCustomer(new CustomerDto()
                {
                    Email = "jajaja@itu.dk",
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            }
            Thread.Sleep(5000);
            t = ReadCustomerList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + ReadCustomer(t[t.Count - 1].Id.Value).Result.FirstName);
            int id = t[0].Id.Value;
            CreateCustomer(new CustomerDto()
            {
                Email = "jajaja@itu.dk",
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie"
            });
            Thread.Sleep(5000);
            t = ReadCustomerList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + ReadCustomer(t[t.Count - 1].Id.Value).Result.FirstName);

            DeleteCustomer(t[0].Id.Value);
            Thread.Sleep(5000);
            t = ReadCustomerList().Result;
            Console.WriteLine(t.Count);
        }

        public async void CreateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Create("customers", customer);
        }

        public async Task<CustomerDto> ReadCustomer(int id)
        {
            var customerToReturn = await DriveITWebAPI.Read<CustomerDto>("customers/" + id);
            return customerToReturn;
        }

        public async Task<IList<CustomerDto>> ReadCustomerList()
        {
            var customers = await DriveITWebAPI.ReadList<CustomerDto>("customers");
            return customers;
        }
        public async void UpdateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Update("customers", customer, customer.Id.Value);
        }

        public async void DeleteCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Delete<CustomerDto>("customers", customer.Id.Value);
        }

        public async void DeleteCustomer(int id)
        {
            await DriveITWebAPI.Delete<CustomerDto>("customers", id);
        }
    }
}
