using System;

namespace DriveIT.Model
{
    public class ContactRequestDto
    {
        public DateTime Requested { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
    }
}
