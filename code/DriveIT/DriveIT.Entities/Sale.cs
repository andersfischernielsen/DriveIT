using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.Entities
{
    class Sale
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal Price { get; set; }
    }
}
