using DriveIT.Entities;
using DriveIT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DriveIT.EntityFramework.DriveITContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DriveIT.EntityFramework.DriveITContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (!roleManager.RoleExists(role.ToString()))
                {
                    roleManager.Create(new IdentityRole(role.ToString()));
                }
            }

            var userStore = new UserStore<DriveITUser>(context);
            var userManager = new UserManager<DriveITUser>(userStore);
            var employee = (Employee)userManager.FindById("mlin@itu.dk");
            if (employee == null)
            {
                employee = new Employee
                {
                    Id = "mlin@itu.dk",
                    UserName = "mlin@itu.dk",
                    Email = "mlin@itu.dk",
                    FirstName = "Mikael",
                    LastName = "Jepsen",
                    PhoneNumber = "12345678",
                };

                CheckResult(userManager.Create(employee, "N0t_Really_a_password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(employee.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(employee.Id, role.ToString());
                }
            }

            userManager.AddToRoles(employee.Id, Role.Administrator.ToString(), Role.Employee.ToString());

            var customer = (Customer)userManager.FindById("cust@driveit.dk");
            if (customer == null)
            {
                customer = new Customer
                {
                    Id = "cust@driveit.dk",
                    UserName = "cust@driveit.dk",
                    Email = "cust@driveit.dk",
                    FirstName = "Cu",
                    LastName = "St",
                    PhoneNumber = "98765432",
                };

                CheckResult(userManager.Create(customer, "N1t_Really_a_password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(customer.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(customer.Id, role.ToString());
                }
            }

            userManager.AddToRoles(customer.Id, Role.Customer.ToString());

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

            var comment = new Comment
            {
                Id = 1,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                DateCreated = new DateTime(2014, 12, 1),
                Title = "Bad Car",
                Description = "I think it's a bad color",
            };
            var contactRequest = new ContactRequest
            {
                Id = 1,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                Created = new DateTime(2014, 12, 1),
                EmployeeId = "mlin@itu.dk"
            };
            var sale = new Sale
            {
                Id = 1,
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                EmployeeId = "mlin@itu.dk",
                Price = 1000,
                DateOfSale = new DateTime(2014, 12, 1)
            };

            context.Cars.AddOrUpdate(
                c => c.Id,
                car);
            context.Comments.AddOrUpdate(
                c => c.Id,
                comment);
            context.ContactRequests.AddOrUpdate(
                c => c.Id,
                contactRequest);
            context.Sales.AddOrUpdate(
                s => s.Id,
                sale);
        }

        private void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new Exception(string.Format("{0}", result.Errors));
            }
        }
    }
}
