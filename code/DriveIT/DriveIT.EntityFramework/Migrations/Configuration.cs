using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DriveIT.Entities;
using DriveIT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework.Migrations
{

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

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (!roleManager.RoleExists(role.ToString()))
                {
                    roleManager.Create(new IdentityRole(role.ToString()));
                }
            }

            var userStore = new UserStore<DriveITUser>(context);
            var userManager = new UserManager<DriveITUser>(userStore);

            SeedAdminUser(userManager);
            SeedCustomerUser(userManager);

            SeedCars(context);
            SeedComments(context);
            SeedContactRequests(context);
            SeedSales(context);
        }

        private void SeedAdminUser(UserManager<DriveITUser> userManager)
        {
            var employee = (Employee)userManager.FindById("admin@driveit.dk");
            if (employee == null)
            {
                employee = new Employee
                {
                    Id = "admin@driveit.dk",
                    UserName = "admin@driveit.dk",
                    Email = "admin@driveit.dk",
                    FirstName = "DriveIT",
                    LastName = "Adminson",
                    PhoneNumber = "88888888",
                };

                CheckResult(userManager.Create(employee, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(employee.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(employee.Id, role.ToString());
                }
            }

            userManager.AddToRoles(employee.Id, Role.Administrator.ToString(), Role.Employee.ToString());
        }
        private void SeedCustomerUser(UserManager<DriveITUser> userManager)
        {
            var customer = (Customer)userManager.FindById("cust@driveit.dk");

            if (customer == null)
            {
                customer = new Customer
                {
                    Id = "cust@driveit.dk",
                    UserName = "cust@driveit.dk",
                    Email = "cust@driveit.dk",
                    FirstName = "Cust",
                    LastName = "Omer",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(customer, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(customer.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(customer.Id, role.ToString());
                }
            }

            userManager.AddToRoles(customer.Id, Role.Customer.ToString());
        }

        private void SeedSales(DriveITContext context)
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    EmployeeId = "admin@driveit.dk",
                    Price = 1000000,
                    DateOfSale = new DateTime(2014, 12, 5) // More than 5 days ago (shouldn't appear on web)
                },
                new Sale
                {
                    Id = 2,
                    CarId = 2,
                    CustomerId = "cust@driveit.dk",
                    EmployeeId = "admin@driveit.dk",
                    Price = 400000,
                    DateOfSale = DateTime.Now // Less than 5 days ago (should appear on web at time of deployment!)
                },
            };

            foreach (var sale in sales)
            {
                context.Sales.AddOrUpdate(sale);
            }
        }

        private void SeedContactRequests(DriveITContext context)
        {
            var requests = new List<ContactRequest>
            {
                new ContactRequest
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    Created = DateTime.Now,
                    EmployeeId = "admin@driveit.dk"
                }
            };

            foreach (var request in requests)
            {
                context.ContactRequests.AddOrUpdate(request);
            }
        }

        private void SeedComments(DriveITContext context)
        {
            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    DateCreated = new DateTime(2014, 12, 1),
                    Title = "Bad Color",
                    Description = "I think it's a hideous color!",
                }, 
                new Comment
                {
                    Id = 2,
                    CarId = 3,
                    CustomerId = "cust@driveit.dk",
                    DateCreated = new DateTime(2014, 12, 1),
                    Title = "Bad Car",
                    Description = "I think it's a disgusting car! Why would anyone buy this?",
                }
            };

            foreach (var comment in comments)
            {
                context.Comments.AddOrUpdate(comment);
            }
        }

        private static void SeedCars(DriveITContext context)
        {
            var cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Make = "Ford",
                    Model = "Focus",
                    Fuel = FuelType.Gasoline,
                    Color = "Black",
                    Created = DateTime.Now,
                    DistanceDriven = 10000,
                    Drive = "FWD",
                    Mileage = 20,
                    Price = 10000,
                    Transmission = "Manual",
                    Year = 2008,
                },
                new Car
                {
                    Id = 2,
                    Make = "Ford",
                    Model = "Fiesta",
                    Fuel = FuelType.Gasoline,
                    Color = "Dark Green",
                    Created = DateTime.Now,
                    DistanceDriven = 20000,
                    Drive = "FWD",
                    Mileage = 15,
                    Price = 50000,
                    Transmission = "Manual",
                    Year = 2005
                },
                new Car
                {
                    Id = 3,
                    Make = "Audi",
                    Model = "A4",
                    Fuel = FuelType.Gasoline,
                    Color = "Dark Blue",
                    Created = DateTime.Now,
                    DistanceDriven = 80000,
                    Drive = "FWD",
                    Mileage = 10,
                    Price = 100000,
                    Transmission = "Automatic",
                    Year = 2008
                },
                new Car
                {
                    Id = 4,
                    Make = "Audi",
                    Model = "R8",
                    Fuel = FuelType.Gasoline,
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 100000,
                    Drive = "RWD",
                    Mileage = 10,
                    Price = 500000,
                    Transmission = "Manual",
                    Year = 2010
                }
            };

            foreach (var car in cars)
            {
                context.Cars.AddOrUpdate(c => c.Id, car);
            }
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
