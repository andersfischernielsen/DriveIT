using System;
using DriveIT.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }
            if (!roleManager.RoleExists("Employee"))
            {
                roleManager.Create(new IdentityRole("Employee"));
            }
            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }

            var userStore = new UserStore<DriveITUser>(context);
            var userManager = new UserManager<DriveITUser>(userStore);

            if (userManager.FindByEmail("mlin@itu.dk") == null)
            {
                var user = new DriveITUser
                {
                    Id = "mlin@itu.dk",
                    UserName = "mlin@itu.dk",
                    Email = "mlin@itu.dk",
                };
                var result = userManager.Create(user, "N0t_Really_a_password");
                userManager.AddToRole(user.Id, "Administrator");


                if (!result.Succeeded)
                {
                    throw new Exception(string.Format("{0}", result.Errors));
                }
            }

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
                PhoneNumber = "37 48 34 81",
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
                PhoneNumber = "36 75 69 33",
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
