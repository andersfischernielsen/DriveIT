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
