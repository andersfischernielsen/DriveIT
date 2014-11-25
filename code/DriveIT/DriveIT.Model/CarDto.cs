using System;

namespace DriveIT.Model
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public bool Sold { get; set; }
        public float Mileage { get; set; }
        public string Color { get; set; }
        public int DistanceDriven { get; set; }
        public FuelType Fuel { get; set; }
        public string Drive { get; set; }
        public string Transmission { get; set; }
        public int TopSpeed { get; set; }
        public int NoughtTo100 { get; set; }
        public string ImagePath { get; set; }
    }
}
