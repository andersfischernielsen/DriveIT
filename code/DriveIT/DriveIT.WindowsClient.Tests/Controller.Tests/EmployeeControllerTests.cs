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
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        [SetUp]
        public void Setup()
        {
            _employeeController = new EmployeeController();
        }

        [Test]
        public void TestAllMethods()
        {
            var t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
                _employeeController.CreateEmployee(new EmployeeDto()
                {
                    Username = "sexydude123",
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _employeeController.ReadEmployee(t[t.Count - 1].Id.Value).Result.FirstName);
            _employeeController.UpdateEmployee(new EmployeeDto()
            {
                Username = "sexydude123",
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie",
                Id = t[t.Count - 1].Id.Value
            });
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _employeeController.ReadEmployee(t[t.Count - 1].Id.Value).Result.FirstName);

            _employeeController.DeleteEmployee(t[t.Count - 1].Id.Value);
            Thread.Sleep(2000);
            t = _employeeController.ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
        }
    }
}
