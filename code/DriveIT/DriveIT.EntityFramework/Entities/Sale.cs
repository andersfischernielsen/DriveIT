using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveIT.EntityFramework.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal Price { get; set; }
    }
}
