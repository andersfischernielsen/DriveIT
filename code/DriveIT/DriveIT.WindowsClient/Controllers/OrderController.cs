using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT_Windows_Client.Controllers
{
    public class OrderController
    {
        public OrderController()
        {
            TestMethod();
        }
        private void TestMethod()
        {
            var t = ReadOrderList().Result;
            Console.WriteLine(t.Count);
            try
            {
                CreateOrder(t[0]);
            }
            catch (Exception)
            {
                CreateOrder(new SaleDto()
                {
                    Price = 1000,
                    Sold = DateTime.Now,
                });
            }
            Thread.Sleep(5000);
            t = ReadOrderList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + ReadOrder(t[t.Count - 1].Id.Value).Result.Price);
            int id = t[0].Id.Value;
            CreateOrder(new SaleDto()
            {
                Price = 9999,
                Sold = DateTime.Now,
            });
            Thread.Sleep(5000);
            t = ReadOrderList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + ReadOrder(t[t.Count - 1].Id.Value).Result.Price);

            DeleteOrder(t[0].Id.Value);
            Thread.Sleep(5000);
            t = ReadOrderList().Result;
            Console.WriteLine(t.Count);
        }

        public async void CreateOrder(SaleDto sale)
        {
            await DriveITWebAPI.Create("sales", sale);
        }
        public async Task<SaleDto> ReadOrder(int id)
        {
            var saleToReturn = await DriveITWebAPI.Read<SaleDto>("sales/" + id);
            return saleToReturn;
        }
        public async Task<IList<SaleDto>> ReadOrderList()
        {
            var sales = await DriveITWebAPI.ReadList<SaleDto>("sales");
            return sales;
        }
        public async void UpdateOrder(SaleDto sale)
        {
            await DriveITWebAPI.Update("sales", sale, sale.Id.Value);
        }
        public async void DeleteOrder(SaleDto sale)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales", sale.Id.Value);
        }
        public async void DeleteOrder(int id)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales", id);
        }
    }
}
