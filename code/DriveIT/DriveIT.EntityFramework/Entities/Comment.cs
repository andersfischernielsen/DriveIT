﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveIT.EntityFramework.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
