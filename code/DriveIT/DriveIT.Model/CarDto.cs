using System;

namespace DriveIT.Model
{
    public class CarDto
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public bool Sold { get; set; }
        public float Mileage { get; set; }
        public string Color { get; set; }
        public int DistanceDriven { get; set; }
        public string Fuel { get; set; }
        public string Drive { get; set; }
        public string Transmission { get; set; }
    }
}
