﻿using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _employeeController;

        private string _createdEmpId;
        private string _toDeleteEmpId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DriveITWebAPI.Login("awis@itu.dk", "4dmin_Password").Wait();
            _employeeController = new EmployeeController();
            var empTask = _employeeController.CreateEmployee(new EmployeeDto
            {
                Email = "EmpSetupTest@mail.dk",
                JobTitle = "Tester",
                FirstName = "TestFirst",
                LastName = "TestLast",
                Phone = "12344321s",
            },"EmpTestPass1",Role.Employee);
            empTask.Wait();
            _toDeleteEmpId = "EmpSetupTest@mail.dk";
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var deleteTask = _employeeController.DeleteEmployee(_createdEmpId);
            deleteTask.Wait();
        }

        [Test]
        public async Task TestCreateEmployee()
        {
            var t = _employeeController.ReadEmployeeList().Result;
            int amtOfCarsStart = t.Count;
            var empToCreate = new EmployeeDto
            {
                Email = "EmpCreateTest@mail.dk",
                JobTitle = "Tester2",
                FirstName = "TestFirst2",
                LastName = "TestLast2",
                Phone = "12344321",
            };
            await _employeeController.CreateEmployee(empToCreate, "EmpTestPass1", Role.Employee);
            t = _employeeController.ReadEmployeeList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var empJustIn = _employeeController.ReadEmployee("EmpCreateTest@mail.dk").Result;
            Assert.AreEqual(empToCreate.Email, empJustIn.Email);
            Assert.AreEqual(empToCreate.JobTitle, empJustIn.JobTitle);
            Assert.AreEqual(empToCreate.FirstName, empJustIn.FirstName);
            Assert.AreEqual(empToCreate.LastName, empJustIn.LastName);
            Assert.AreEqual(empToCreate.Phone, empJustIn.Phone);

            _createdEmpId = empJustIn.Id;
        }

        [Test]
        public async Task TestDeleteEmployee()
        {
            var t = _employeeController.ReadEmployeeList().Result;
            int amtOfCarsStart = t.Count;
            await _employeeController.DeleteEmployee(_toDeleteEmpId);
            t = _employeeController.ReadEmployeeList().Result;

            Assert.AreEqual(amtOfCarsStart - 1, t.Count);
            foreach (var carDto in t)
            {
                Assert.AreNotEqual(_toDeleteEmpId, carDto.Id);
            }
        }


        [Test]
        public async Task TestUpdateEmployee()
        {
            var t = _employeeController.ReadEmployeeList().Result;
            int amtOfCarsStart = t.Count;
            var empToCreate = new EmployeeDto
            {
                Email = "EmpTestBefore@mail.dk",
                JobTitle = "TesterBefore",
                FirstName = "TestFirstBefore",
                LastName = "TestLastBefore",
                Phone = "12345678",
            };
            await _employeeController.CreateEmployee(empToCreate, "EmpTestPass1", Role.Employee);
            t = _employeeController.ReadEmployeeList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var empJustIn = _employeeController.ReadEmployee("EmpTestBefore@mail.dk").Result;

            empJustIn.JobTitle = "TesterAfter";
            empJustIn.FirstName = "TestFirstAfter";
            empJustIn.LastName = "TestLastAfter";
            empJustIn.Phone = "87654321";
            await _employeeController.UpdateEmployee(empJustIn);

            t = _employeeController.ReadEmployeeList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var empUpdated = _employeeController.ReadEmployee("EmpTestBefore@mail.dk").Result;
            Assert.AreEqual(empUpdated.Id, empJustIn.Id);

            Assert.AreEqual(empJustIn.Email, empUpdated.Email);
            Assert.AreEqual(empJustIn.JobTitle, empUpdated.JobTitle);
            Assert.AreEqual(empJustIn.FirstName, empUpdated.FirstName);
            Assert.AreEqual(empJustIn.LastName, empUpdated.LastName);
            Assert.AreEqual(empJustIn.Phone, empUpdated.Phone);

            await _employeeController.DeleteEmployee(empUpdated.Id);
        }
    }
}
