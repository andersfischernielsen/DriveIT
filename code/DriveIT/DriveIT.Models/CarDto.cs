using System;

namespace DriveIT.Models
{
    /// <summary>
    /// The CarDto is a preview of the car.
    /// 
    /// It does not contain all the information of the car, but an overview to give the idea of what car it is.
    /// </summary>
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public bool Sold { get; set; }
        public string Color { get; set; }
        public int DistanceDriven { get; set; }
        public FuelType Fuel { get; set; }
        public string Transmission { get; set; }
        public string ImagePath { get; set; }
    }
}
