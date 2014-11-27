using System;

namespace DriveIT.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal Price { get; set; }
    }
}
