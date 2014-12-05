using System;

namespace DriveIT.Models
{
    public class SaleDto
    {
        public int? Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Sold { get; set; }
        public int CarId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
    }
}
