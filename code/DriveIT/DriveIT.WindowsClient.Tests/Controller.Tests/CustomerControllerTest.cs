using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
            
        private CustomerController _customerController;
        [SetUp]
        public void Setup()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void TestAllMethods()
        {
            var t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
            try
            {
                _customerController.CreateCustomer(t[0]);
            }
            catch (Exception)
            {
                _customerController.CreateCustomer(new CustomerDto()
                {
                    Email = "jajaja@itu.dk",
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            }
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _customerController.ReadCustomer(t[t.Count - 1].Id.Value).Result.FirstName);
            int id = t[0].Id.Value;
            _customerController.UpdateCustomer(new CustomerDto()
            {
                Email = "jajaja@itu.dk",
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie"
            });
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _customerController.ReadCustomer(t[t.Count - 1].Id.Value).Result.FirstName);

            _customerController.DeleteCustomer(t[0].Id.Value);
            Thread.Sleep(2000);
            t = _customerController.ReadCustomerList().Result;
            Console.WriteLine(t.Count);
        }
    }
}
