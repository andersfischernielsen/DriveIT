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

        private string _createdCustId;
        private string _toDeleteCustId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DriveITWebAPI.Login("admin@driveIT.dk", "4dmin_Password").Wait();
            _customerController = new CustomerController();
            var custTask = _customerController.CreateCustomer(new CustomerDto
            {
                Email = "CustSetupTest@mail.dk",
                FirstName = "TestFirst",
                LastName = "TestLast",
                Phone = "12344321s",
            }, "CustTestPass1");
            custTask.Wait();
            _toDeleteCustId = "CustSetupTest@mail.dk";
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var deleteTask = _customerController.DeleteCustomer(_createdCustId);
            deleteTask.Wait();
        }

        [Test]
        public async Task TestCreateEmployee()
        {
            var t = _customerController.ReadCustomerList().Result;
            int amtOfCarsStart = t.Count;
            var custToCreate = new CustomerDto
            {
                Email = "CustCreateTest@mail.dk",
                FirstName = "TestFirst2",
                LastName = "TestLast2",
                Phone = "12344321",
            };
            await _customerController.CreateCustomer(custToCreate, "CustTestPass1");
            t = _customerController.ReadCustomerList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var custJustIn = _customerController.ReadCustomer("CustCreateTest@mail.dk").Result;
            Assert.AreEqual(custToCreate.Email, custJustIn.Email);
            Assert.AreEqual(custToCreate.FirstName, custJustIn.FirstName);
            Assert.AreEqual(custToCreate.LastName, custJustIn.LastName);
            Assert.AreEqual(custToCreate.Phone, custJustIn.Phone);

            _createdCustId = custJustIn.Id;
        }

        [Test]
        public async Task TestDeleteEmployee()
        {
            var t = _customerController.ReadCustomerList().Result;
            int amtOfCarsStart = t.Count;
            await _customerController.DeleteCustomer(_toDeleteCustId);
            t = _customerController.ReadCustomerList().Result;

            Assert.AreEqual(amtOfCarsStart - 1, t.Count);
            foreach (var carDto in t)
            {
                Assert.AreNotEqual(_toDeleteCustId, carDto.Id);
            }
        }


        [Test]
        public async Task TestUpdateEmployee()
        {
            var t = _customerController.ReadCustomerList().Result;
            int amtOfCarsStart = t.Count;
            var custToCreate = new CustomerDto
            {
                Email = "CustTestBefore@mail.dk",
                FirstName = "TestFirstBefore",
                LastName = "TestLastBefore",
                Phone = "12345678",
            };
            await _customerController.CreateCustomer(custToCreate, "EmpTestPass1");
            t = _customerController.ReadCustomerList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var custJustIn = _customerController.ReadCustomer("CustTestBefore@mail.dk").Result;

            custJustIn.FirstName = "TestFirstAfter";
            custJustIn.LastName = "TestLastAfter";
            custJustIn.Phone = "87654321";
            await _customerController.UpdateCustomer(custJustIn);

            t = _customerController.ReadCustomerList().Result;
            Assert.AreEqual(amtOfCarsStart + 1, t.Count);
            var custUpdated = _customerController.ReadCustomer("CustTestBefore@mail.dk").Result;
            Assert.AreEqual(custUpdated.Id, custJustIn.Id);

            Assert.AreEqual(custJustIn.Email, custUpdated.Email);
            Assert.AreEqual(custJustIn.FirstName, custUpdated.FirstName);
            Assert.AreEqual(custJustIn.LastName, custUpdated.LastName);
            Assert.AreEqual(custJustIn.Phone, custUpdated.Phone);

            await _customerController.DeleteCustomer(custUpdated.Id);
        }
    }
}
