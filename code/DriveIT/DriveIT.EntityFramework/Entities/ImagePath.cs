using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveIT.EntityFramework.Entities
{
    public class ImagePath
    {
        [Column(Order = 0), Key]
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Column(Order = 1), Key]
        public string Path { get; set; }
    }
}
