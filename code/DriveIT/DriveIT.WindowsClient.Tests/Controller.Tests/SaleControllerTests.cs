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
    public class SaleControllerTests
    {
        private SaleController _orderController;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            DriveITWebAPI.Login("admin@driveIT.dk", "4dmin_Password").Wait();
        }

        [SetUp]
        public void Setup()
        {
            _orderController = new SaleController();
            
        }

        [Test]
        public async Task TestAllMethods()
        {
            var t = _orderController.ReadSaleList().Result;
            Console.WriteLine(t.Count);
                await _orderController.CreateSale(new SaleDto()
                {
                    Price = 1000,
                    Sold = DateTime.Now,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    EmployeeId = "mlin@itu.dk",
                });

            Thread.Sleep(2000);
            t = _orderController.ReadSaleList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _orderController.ReadSale(t[t.Count - 1].Id.Value).Result.Price);
            int id = t[0].Id.Value;
            await _orderController.UpdateSale(new SaleDto()
            {
                Price = 9999,
                Sold = DateTime.Now,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                EmployeeId = "mlin@itu.dk",
                Id = t[t.Count - 1].Id.Value
            });
            Thread.Sleep(2000);
            t = _orderController.ReadSaleList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _orderController.ReadSale(t[t.Count - 1].Id.Value).Result.Price);

            await _orderController.DeleteSale(t[t.Count - 1]);
            Thread.Sleep(2000);
            t = _orderController.ReadSaleList().Result;
            Console.WriteLine(t.Count);
        }
    }
}
