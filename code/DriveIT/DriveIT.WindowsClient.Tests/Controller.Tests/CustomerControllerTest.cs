using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
            
        private CustomerController _customerController;
        [SetUp]
        public async Task Setup()
        {
            _customerController = new CustomerController();
            await DriveITWebAPI.Login("mlin@itu.dk", "N0t_Really_a_password");
        }

        [Test]
        public async Task TestAllMethods()
        {
            var t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
                await _customerController.CreateCustomer(new CustomerDto()
                {
                    Email = "jajaja@itu.dk",
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _customerController.ReadCustomer(t[t.Count - 1].Email).Result.FirstName);
            await _customerController.UpdateCustomer(new CustomerDto()
            {
                Email = "jajaja@itu.dk",
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie",
                Id = t[t.Count - 1].Id
            });
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _customerController.ReadCustomer(t[t.Count - 1].Email).Result.FirstName);

            await _customerController.DeleteCustomer(t[t.Count - 1]);
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
        }
    }
}
