using System;

namespace DriveIT.Models
{
    public class ContactRequestDto
    {
        public int? Id { get; set; }
        public DateTime Requested { get; set; }
        public string CustomerId { get; set; }
        public int CarId { get; set; }
        public string EmployeeId { get; set; }
    }
}
