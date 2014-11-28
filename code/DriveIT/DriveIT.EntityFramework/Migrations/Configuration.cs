using DriveIT.Entities;

namespace DriveIT.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DriveITContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DriveIT.EntityFramework.DriveITContext";
        }

        protected override void Seed(DriveITContext context)
        {
            context.Cars.AddOrUpdate(
                c => c.Id,
                new Car
                {
                    Id = 1,
                    Price = 100000,
                    Sold = false,
                    Drive = "FWD",
                    Mileage = 15.6f,
                    Fuel = "Gasoline",
                    Model = "TT",
                    Make = "Ford",
                    Created = DateTime.Now,
                    Color = "Rust",
                    DistanceDriven = 340000,
                    Transmission = "Manual",
                    Year = 1982
                });
            context.Customers.AddOrUpdate(
                c => c.Id,
                new Customer
                {
                    Id = 1,
                    LastName = "Jepsen",
                    FirstName = "Mikael",
                    Username = "mlin",
                    Email = "mlin@itu.dk",
                    Password = "Negative",
                    PhoneNumber = "42752687"
                });
        }
    }
}
