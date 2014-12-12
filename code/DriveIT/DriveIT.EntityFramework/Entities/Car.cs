using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DriveIT.Models;

namespace DriveIT.EntityFramework.Entities
{
    public class Car
    {
        private int _id;

        [Key]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                if (ImagePaths != null)
                {
                    ImagePaths.ForEach(imagePath => imagePath.CarId = value);
                }
            }
        }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }

        [NotMapped]
        public bool Sold { get; set; }
        public float Mileage { get; set; }
        public string Color { get; set; }
        public int DistanceDriven { get; set; }
        public FuelType Fuel { get; set; }
        public string Drive { get; set; }
        public string Transmission { get; set; }
        public float TopSpeed { get; set; }
        public float NoughtTo100 { get; set; }
        public List<ImagePath> ImagePaths { get; set; }
    }
}
