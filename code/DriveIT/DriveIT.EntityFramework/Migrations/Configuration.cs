using System;
using DriveIT.Entities;

namespace DriveIT.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DriveITContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DriveITContext context)
        {

            var car = new Car
            {
                Id = 1,
                Make = "Ford",
                Model = "Focus",
                Fuel = "Gasoline",
                Color = "Black",
                Created = new DateTime(2014, 12, 01),
                DistanceDriven = 10000,
                Drive = "FWD",
                Mileage = 17.0f,
                Price = 100000,
                Sold = false,
                Transmission = "Manual",
                Year = 2008
            };

            var customer = new Customer
            {
                Id = 1,
                Email = "cust@driveit.dk",
                FirstName = "Cu",
                LastName = "St",
                Password = "Should not be present in this object",
                PhoneNumber = "37 48 34 81",
                Username = "cust" //Should probably not be part of this object as well.
            };
            var comment = new Comment
            {
                Id = 1,
                CarId = 1,
                CustomerId = 1,
                DateCreated = new DateTime(2014, 12, 1),
                Title = "Bad Car",
                Description = "I think it's a bad color",
            };
            var employee = new Employee
            {
                Id = 1,
                Email = "empl@driveit.dk",
                FirstName = "Em",
                LastName = "Pl",
                Password = "Should not be present in this object",
                PhoneNumber = "36 75 69 33",
                Username = "empl" //Should probably no be part of this object as well.
            };
            var contactRequest = new ContactRequest
            {
                Id = 1,
                CarId = 1,
                CustomerId = 1,
                Created = new DateTime(2014, 12, 1),
                EmployeeId = 1
            };
            var sale = new Sale
            {
                Id = 1,
                CarId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                Price = 1000,
                DateOfSale = new DateTime(2014, 12, 1)
            };

            context.Cars.AddOrUpdate(
                c => c.Id,
                car);
            context.Customers.AddOrUpdate(
                c => c.Id,
                customer);
            context.Comments.AddOrUpdate(
                c => c.Id,
                comment);
            context.Employees.AddOrUpdate(
                e => e.Id,
                employee);
            context.ContactRequests.AddOrUpdate(
                c => c.Id,
                contactRequest);
            context.Sales.AddOrUpdate(
                s => s.Id,
                sale);
        }
    }
}
