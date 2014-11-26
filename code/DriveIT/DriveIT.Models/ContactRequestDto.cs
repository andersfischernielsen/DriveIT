using System;

namespace DriveIT.Models
{
    public class ContactRequestDto
    {
        public DateTime Requested { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public bool Handled { get; set; }
    }
}
