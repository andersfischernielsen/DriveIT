using System;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class SaleControllerTests
    {
        private SaleController _saleController;
        private int _createdSaleId;
        private int _toDeleteSaleId;

        private int _carId;
        private string _customerId;
        private string _employeeId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DriveITWebAPI.Login("awis@itu.dk", "4dmin_Password").Wait();
            _saleController = new SaleController();
            CreateOtherEntities();

            var saleTask = _saleController.CreateSale(new SaleDto
            {
                Price = 100,
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Sold = DateTime.Now
            });
            saleTask.Wait();
            _toDeleteSaleId = saleTask.Result.Id.GetValueOrDefault();
        }

        private void CreateOtherEntities()
        {
            _carId = new CarController().ReadCarList().Result[0].Id.GetValueOrDefault();
            _customerId = new CustomerController().ReadCustomerList().Result[0].Id;
            _employeeId = new EmployeeController().ReadEmployeeList().Result[0].Id;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var deleteTask = _saleController.DeleteSale(_createdSaleId);
            deleteTask.Wait();
        }

        [Test]
        public async Task TestCreateSale()
        {
            var t = _saleController.ReadSaleList().Result;
            int amtOfSalesStart = t.Count;
            var saleToCreate = new SaleDto
            {
                Price = 200,
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Sold = DateTime.Now
            };
            await _saleController.CreateSale(saleToCreate);
            t = _saleController.ReadSaleList().Result;
            Assert.AreEqual(amtOfSalesStart + 1, t.Count);
            var saleJustIn = t[t.Count - 1];
            Assert.AreEqual(saleToCreate.Price, saleJustIn.Price);
            Assert.AreEqual(saleToCreate.CarId, saleJustIn.CarId);
            Assert.AreEqual(saleToCreate.CustomerId, saleJustIn.CustomerId);
            Assert.AreEqual(saleToCreate.EmployeeId, saleJustIn.EmployeeId);

            _createdSaleId = saleJustIn.Id.GetValueOrDefault();
        }

        [Test]
        public async Task TestDeleteSale()
        {
            var t = _saleController.ReadSaleList().Result;
            int amtOfSalesStart = t.Count;
            await _saleController.DeleteSale(_toDeleteSaleId);
            t = _saleController.ReadSaleList().Result;

            Assert.AreEqual(amtOfSalesStart - 1, t.Count);
            foreach (var carDto in t)
            {
                Assert.AreNotEqual(_toDeleteSaleId, carDto.Id.GetValueOrDefault());
            }
        }


        [Test]
        public async Task TestUpdateSale()
        {
            var t = _saleController.ReadSaleList().Result;
            int amtOfSaleStart = t.Count;
            var saleToCreate = new SaleDto()
            {
                Price = 300,
                CarId = _carId,
                CustomerId = _customerId,
                EmployeeId = _employeeId,
                Sold = DateTime.Now
            };
            await _saleController.CreateSale(saleToCreate);
            t = _saleController.ReadSaleList().Result;
            Assert.AreEqual(amtOfSaleStart + 1, t.Count);
            var saleJustIn = t[t.Count - 1];

            saleJustIn.Price = 400;

            await _saleController.UpdateSale(saleJustIn);

            t = _saleController.ReadSaleList().Result;
            Assert.AreEqual(amtOfSaleStart + 1, t.Count);
            var saleUpdated = t[t.Count - 1];
            Assert.AreEqual(saleUpdated.Id.GetValueOrDefault(), saleJustIn.Id.GetValueOrDefault());

            Assert.AreEqual(saleJustIn.Price, saleUpdated.Price);


            await _saleController.DeleteSale(saleUpdated.Id.GetValueOrDefault());
        }
    }
}
