using System;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class ContactRequestControllerTests
    {
        private ContactRequestController _contactRequestController;
        private int _createdContactRequestId;
        private int _toDeleteContactRequestId;

        private int _carId;
        private int _carId2;
        private string _customerId;
        private string _employeeId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            CreateOtherEntities();
            LogInAsCustomer();
            _contactRequestController = new ContactRequestController();

            var contactRequestTask = _contactRequestController.CreateContactRequest(new ContactRequestDto()
            {
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Requested = DateTime.Now
            });
            contactRequestTask.Wait();
            _toDeleteContactRequestId = contactRequestTask.Result.Id.GetValueOrDefault();
        }

        private void LogInAsCustomer()
        {
            try
            {
                DriveITWebAPI.Login("cust@driveIT.dk", "Cust0mer_Password").Wait();
            }
            catch (Exception)
            {
                Console.WriteLine("Log In As User Exception");
            }
        }

        private void LogInAsEmployee()
        {
            DriveITWebAPI.Login("admin@driveIT.dk", "4dmin_Password").Wait();
        }

        private void CreateOtherEntities()
        {
            LogInAsEmployee();
            _carId = new CarController().ReadCarList().Result[0].Id.GetValueOrDefault();
            _carId2 = new CarController().ReadCarList().Result[1].Id.GetValueOrDefault();
            _customerId = "cust@driveit.dk";
            _employeeId = new EmployeeController().ReadEmployeeList().Result[0].Id;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            LogInAsEmployee();
            var deleteTask = _contactRequestController.DeleteContactRequest(_createdContactRequestId);
            deleteTask.Wait();
        }

        [Test]
        public async Task TestCreateContactRequest()
        {
            LogInAsEmployee();
            var t = _contactRequestController.ReadContactRequests().Result;
            int amtOfContactRequestStart = t.Count;
            LogInAsCustomer();
            var contactRequestToCreate = new ContactRequestDto()
            {
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Requested = DateTime.Now
            };
            await _contactRequestController.CreateContactRequest(contactRequestToCreate);
            LogInAsEmployee();
            t = _contactRequestController.ReadContactRequests().Result;
            Assert.AreEqual(amtOfContactRequestStart + 1, t.Count);
            var contactRequestJustIn = t[t.Count - 1];
            Assert.AreEqual(contactRequestToCreate.CarId, contactRequestJustIn.CarId);
            Assert.AreEqual(contactRequestToCreate.CustomerId, contactRequestJustIn.CustomerId);
            Assert.AreEqual(contactRequestToCreate.EmployeeId, contactRequestJustIn.EmployeeId);

            _createdContactRequestId = contactRequestJustIn.Id.GetValueOrDefault();
        }

        [Test]
        public async Task TestDeleteContactRequest()
        {
            LogInAsEmployee();
            var t = _contactRequestController.ReadContactRequests().Result;
            int amtOfContactRequestStart = t.Count;
            await _contactRequestController.DeleteContactRequest(_toDeleteContactRequestId);
            t = _contactRequestController.ReadContactRequests().Result;

            Assert.AreEqual(amtOfContactRequestStart - 1, t.Count);
            foreach (var carDto in t)
            {
                Assert.AreNotEqual(_toDeleteContactRequestId, carDto.Id.GetValueOrDefault());
            }
        }


        [Test]
        public async Task TestUpdateContactRequest()
        {
            LogInAsEmployee();
            var t = _contactRequestController.ReadContactRequests().Result;
            LogInAsCustomer();
            int amtOfContactRequestsStart = t.Count;
            var contactRequestToCreate = new ContactRequestDto()
            {
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Requested = DateTime.Now
            };
            await _contactRequestController.CreateContactRequest(contactRequestToCreate);
            LogInAsEmployee();
            t = _contactRequestController.ReadContactRequests().Result;
            Assert.AreEqual(amtOfContactRequestsStart + 1, t.Count);
            var contactRequestJustIn = t[t.Count - 1];

            contactRequestJustIn.CarId = _carId2;

            await _contactRequestController.UpdateContactRequest(contactRequestJustIn);

            t = _contactRequestController.ReadContactRequests().Result;
            Assert.AreEqual(amtOfContactRequestsStart + 1, t.Count);
            var contactRequestUpdated = t[t.Count - 1];
            Assert.AreEqual(contactRequestUpdated.Id.GetValueOrDefault(), contactRequestJustIn.Id.GetValueOrDefault());

            Assert.AreEqual(contactRequestJustIn.CarId, contactRequestUpdated.CarId);


            await _contactRequestController.DeleteContactRequest(contactRequestUpdated.Id.GetValueOrDefault());
        }
    }
}
