﻿using System;
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
    public class OrderControllerTests
    {
        private OrderController _orderController;
        [SetUp]
        public void Setup()
        {
            _orderController = new OrderController();
        }

        [Test]
        public void TestAllMethods()
        {
            var t = _orderController.ReadOrderList().Result;
            Console.WriteLine(t.Count);
            try
            {
                _orderController.CreateOrder(t[0]);
            }
            catch (Exception)
            {
                _orderController.CreateOrder(new SaleDto()
                {
                    Price = 1000,
                    Sold = DateTime.Now,
                });
            }
            Thread.Sleep(2000);
            t = _orderController.ReadOrderList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _orderController.ReadOrder(t[t.Count - 1].Id.Value).Result.Price);
            int id = t[0].Id.Value;
            _orderController.UpdateOrder(new SaleDto()
            {
                Price = 9999,
                Sold = DateTime.Now,
            });
            Thread.Sleep(2000);
            t = _orderController.ReadOrderList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _orderController.ReadOrder(t[t.Count - 1].Id.Value).Result.Price);

            _orderController.DeleteOrder(t[0].Id.Value);
            Thread.Sleep(2000);
            t = _orderController.ReadOrderList().Result;
            Console.WriteLine(t.Count);
        }
    }
}