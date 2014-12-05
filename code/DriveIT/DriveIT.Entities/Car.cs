using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriveIT.Entities
{
    public class Car
    {
        [Key]
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
        public string Fuel { get; set; }
        public string Drive { get; set; }
        public string Transmission { get; set; }
        public float TopSpeed { get; set; }
        public float NoughtTo100 { get; set; }
        public List<string> ImagePaths { get; set; }
    }
}
