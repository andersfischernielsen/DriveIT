using System;

namespace DriveIT.Model
{
    class CommentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long CarId { get; set; }
        public long CustomerId { get; set; }
    }
}
