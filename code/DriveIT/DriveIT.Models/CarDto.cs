using System;
using System.Collections.Generic;

namespace DriveIT.Models
{
    /// <summary>
    /// CarDetailDto is used to get all details about a car.
    /// </summary>
    public class CarDto
    {
        public int? Id { get; set; }
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
        public float NoughtTo100 { get; set; }
        public List<string> ImagePaths { get; set; }
    }
}
