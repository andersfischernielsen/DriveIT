using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;

namespace DriveIT.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    namespace DriveIT.MVC.Controllers
    {
        public class OrderController : AsyncController
        {
            // GET: Order
            public ActionResult OrderView()
            {
                return View();
            }

            public async Task<SaleDto> ReadOrder(int id)
            {
                var saleToReturn = await DriveITWebAPI.Read<SaleDto>("Sales/" + id);
                return saleToReturn;
            }
            public async Task<IList<SaleDto>> ReadOrderList()
            {
                var sales = await DriveITWebAPI.ReadList<SaleDto>("Sales");
                return sales;
            }
        }
    }
}