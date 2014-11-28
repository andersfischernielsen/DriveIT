using System;

namespace DriveIT.Models
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
    }
}
