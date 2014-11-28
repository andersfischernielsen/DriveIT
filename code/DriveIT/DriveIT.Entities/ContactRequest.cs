using System;

namespace DriveIT.Entities
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime Created { get; set; }

    }
}
