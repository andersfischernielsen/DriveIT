using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class SaleController
    {
        public SaleController()
        {
        }

        public async Task<SaleDto> CreateSale(SaleDto sale)
        {
            return await DriveITWebAPI.Create("sales", sale);
        }
        public async Task<SaleDto> ReadSale(int id)
        {
            var saleToReturn = await DriveITWebAPI.Read<SaleDto>("sales/" + id);
            return saleToReturn;
        }
        public async Task<IList<SaleDto>> ReadSaleList()
        {
            var sales = await DriveITWebAPI.ReadList<SaleDto>("sales");
            return sales;
        }
        public async Task UpdateSale(SaleDto sale)
        {
            await DriveITWebAPI.Update("sales/" + sale.Id, sale);
        }
        public async Task DeleteSale(SaleDto sale)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales/" + sale.Id);
        }
        public async Task DeleteSale(int id)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales/" + id);
        }
    }
}
