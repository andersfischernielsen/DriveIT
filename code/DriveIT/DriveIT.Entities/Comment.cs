using System;

namespace DriveIT.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
