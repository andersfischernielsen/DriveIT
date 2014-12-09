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
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        [SetUp]
        public void Setup()
        {
            _employeeController = new EmployeeController();
        }

        [Test]
        public async Task TestAllMethods()
        {
            var t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
                await _employeeController.CreateEmployee(new EmployeeDto()
                {
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _employeeController.ReadEmployee(t[t.Count - 1].Email).Result.FirstName);
            await _employeeController.UpdateEmployee(new EmployeeDto()
            {
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie",
                Id = t[t.Count - 1].Email
            });
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _employeeController.ReadEmployee(t[t.Count - 1].Email).Result.FirstName);

            await _employeeController.DeleteEmployee(t[t.Count - 1]);
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
        }
    }
}
