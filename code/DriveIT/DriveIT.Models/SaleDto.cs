using System;

namespace DriveIT.Models
{
    public class SaleDto
    {
        public decimal Price { get; set; }
        public DateTime Sold { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
    }
}
