using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DriveIT.EntityFramework.Entities;
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

        /// <summary>
        /// Seed the initial Entities into the database. This is to avoid having to spend a lot
        /// of time doing this manually at deployment. 
        /// This creates an admin user, a customer and a bunch of Orders, Cars and ContactRequests.
        /// </summary>
        /// <param name="context">The DriveITContext to seed.</param>
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

            SeedAdminUsers(userManager);
            SeedCustomerUsers(userManager);

            SeedCars(context);
            SeedComments(context);
            SeedContactRequests(context);
            SeedSales(context);
        }

        private void SeedAdminUsers(UserManager<DriveITUser> userManager)
        {
            var mikael = (Employee)userManager.FindById("mlin@itu.dk");
            if (mikael == null)
            {
                mikael = new Employee
                {
                    Id = "mlin@itu.dk",
                    UserName = "mlin@itu.dk",
                    Email = "mlin@itu.dk",
                    FirstName = "Mikael",
                    LastName = "Jepsen",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Bitch"
                };

                CheckResult(userManager.Create(mikael, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(mikael.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(mikael.Id, role.ToString());
                }
            }

            userManager.AddToRoles(mikael.Id, Role.Employee.ToString());

            var fislo = (Employee)userManager.FindById("afin@itu.dk");
            if (fislo == null)
            {
                fislo = new Employee
                {
                    Id = "afin@itu.dk",
                    UserName = "afin@itu.dk",
                    Email = "afin@itu.dk",
                    FirstName = "Anders",
                    LastName = "Fischer-Nielsen",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Boss"
                };

                CheckResult(userManager.Create(fislo, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(fislo.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(fislo.Id, role.ToString());
                }
            }

            userManager.AddToRoles(fislo.Id, Role.Administrator.ToString());

            var wind = (Employee)userManager.FindById("awis@itu.dk");
            if (wind == null)
            {
                wind = new Employee
                {
                    Id = "awis@itu.dk",
                    UserName = "awis@itu.dk",
                    Email = "awis@itu.dk",
                    FirstName = "Anders",
                    LastName = "Wind",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Bully"
                };

                CheckResult(userManager.Create(wind, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(wind.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(wind.Id, role.ToString());
                }
            }

            userManager.AddToRoles(wind.Id, Role.Administrator.ToString());

            var gollum = (Employee)userManager.FindById("cnbl@itu.dk");
            if (gollum == null)
            {
                gollum = new Employee
                {
                    Id = "cnbl@itu.dk",
                    UserName = "cnbl@itu.dk",
                    Email = "cnbl@itu.dk",
                    FirstName = "Christoffer",
                    LastName = "Blundell",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Badass"
                };

                CheckResult(userManager.Create(gollum, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(gollum.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(gollum.Id, role.ToString());
                }
            }

            userManager.AddToRoles(gollum.Id, Role.Employee.ToString());

            var pierre = (Employee)userManager.FindById("ppma@itu.dk");
            if (pierre == null)
            {
                pierre = new Employee
                {
                    Id = "ppma@itu.dk",
                    UserName = "ppma@itu.dk",
                    Email = "ppma@itu.dk",
                    FirstName = "Pierre",
                    LastName = "Mandas",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Baboon"
                };

                CheckResult(userManager.Create(pierre, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(pierre.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(pierre.Id, role.ToString());
                }
            }

            userManager.AddToRoles(pierre.Id, Role.Administrator.ToString());

            var robo = (Employee)userManager.FindById("jstc@itu.dk");
            if (robo == null)
            {
                robo = new Employee
                {
                    Id = "jstc@itu.dk",
                    UserName = "jstc@itu.dk",
                    Email = "jstc@itu.dk",
                    FirstName = "Jacob",
                    LastName = "Czepluch",
                    PhoneNumber = "12345678",
                    JobTitle = "Student Bum"
                };

                CheckResult(userManager.Create(robo, "4dmin_Password"));
            }

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(robo.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(robo.Id, role.ToString());
                }
            }

            userManager.AddToRoles(robo.Id, Role.Administrator.ToString());
        }

        private void SeedCustomerUsers(UserManager<DriveITUser> userManager)
        {
            var mikael = (Customer)userManager.FindById("mikaellindemannjepsen@gmail.com");

            if (mikael == null)
            {
                mikael = new Customer
                {
                    Id = "mikaellindemannjepsen@gmail.com",
                    UserName = "mikaellindemannjepsen@gmail.com",
                    Email = "mikaellindemannjepsen@gmail.com",
                    FirstName = "Mikael",
                    LastName = "Jepsen",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(mikael, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(mikael.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(mikael.Id, role.ToString());
                }
            }

            userManager.AddToRoles(mikael.Id, Role.Customer.ToString());

            var fislo = (Customer)userManager.FindById("andersfischern@me.com");

            if (fislo == null)
            {
                fislo = new Customer
                {
                    Id = "andersfischern@me.com",
                    UserName = "andersfischern@me.com",
                    Email = "andersfischern@me.com",
                    FirstName = "Anders",
                    LastName = "Fischer-Nielsen",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(fislo, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(fislo.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(fislo.Id, role.ToString());
                }
            }

            userManager.AddToRoles(fislo.Id, Role.Customer.ToString());

            var wind = (Customer)userManager.FindById("awia00@gmail.com");

            if (wind == null)
            {
                wind = new Customer
                {
                    Id = "awia00@gmail.com",
                    UserName = "awia00@gmail.com",
                    Email = "awia00@gmail.com",
                    FirstName = "Anders",
                    LastName = "Wind",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(wind, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(wind.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(wind.Id, role.ToString());
                }
            }

            userManager.AddToRoles(wind.Id, Role.Customer.ToString());

            var blundell = (Customer)userManager.FindById("christopher.n.blundell@gmail.com");

            if (blundell == null)
            {
                blundell = new Customer
                {
                    Id = "christopher.n.blundell@gmail.com",
                    UserName = "christopher.n.blundell@gmail.com",
                    Email = "christopher.n.blundell@gmail.com",
                    FirstName = "Christopher",
                    LastName = "Blundell",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(blundell, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(blundell.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(blundell.Id, role.ToString());
                }
            }

            userManager.AddToRoles(blundell.Id, Role.Customer.ToString());

            var robo = (Customer)userManager.FindById("j.czepluch@gmail.com");

            if (robo == null)
            {
                robo = new Customer
                {
                    Id = "j.czepluch@gmail.com",
                    UserName = "j.czepluch@gmail.com",
                    Email = "j.czepluch@gmail.com",
                    FirstName = "Jacob",
                    LastName = "Czepluch",
                    PhoneNumber = "11221144",
                };

                CheckResult(userManager.Create(robo, "Cust0mer_Password"));
            }
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (userManager.IsInRole(robo.Id, role.ToString()))
                {
                    userManager.RemoveFromRole(robo.Id, role.ToString());
                }
            }

            userManager.AddToRoles(robo.Id, Role.Customer.ToString());
        }

        private void SeedSales(DriveITContext context)
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Id = 1,
                    CarId = 7,
                    CustomerId = "andersfischern@me.com",
                    EmployeeId = "mlin@itu.dk",
                    Price = 90000,
                    DateOfSale = DateTime.Now // Less than 5 days ago (will appear on web)
                },
                new Sale
                {
                    Id = 2,
                    CarId = 8,
                    CustomerId = "christopher.n.blundell@gmail.com",
                    EmployeeId = "ppma@itu.dk",
                    Price = 80000,
                    DateOfSale = DateTime.Now.Subtract(TimeSpan.FromDays(5)) // More than 5 days ago (shouldn't appear on web at time of deployment!)
                },
            };

            context.Sales.AddOrUpdate(s => s.Id, sales.ToArray());
        }

        private void SeedContactRequests(DriveITContext context)
        {
            var requests = new List<ContactRequest>
            {
                new ContactRequest
                {
                    Id = 1,
                    CarId = 5,
                    CustomerId = "awia00@gmail.com",
                    Created = new DateTime(2014, 12, 2),
                    EmployeeId = "awis@itu.dk"
                },
                new ContactRequest
                {
                    Id = 2,
                    CarId = 2,
                    CustomerId = "christopher.n.blundell@gmail.com",
                    Created = new DateTime(2014, 12, 15),
                }
            };

            context.ContactRequests.AddOrUpdate(r => r.Id, requests.ToArray());
        }

        private void SeedComments(DriveITContext context)
        {
            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = 2,
                    CarId = 3,
                    CustomerId = "mikaellindemannjepsen@gmail.com",
                    DateCreated = new DateTime(2014, 12, 1),
                    Title = "Bad Car",
                    Description = "I think it's a disgusting car! Why would anyone buy this?",
                },
                new Comment
                {
                    Id = 3,
                    CarId = 5,
                    CustomerId = "awia00@gmail.com",
                    DateCreated = new DateTime(2014, 12, 2),
                    Title = "What a wonderful car!",
                    Description = "And I think to myself, what a wonderful car, I one day hope to buy"
                },
                new Comment
                {
                    Id = 1,
                    CarId = 2,
                    CustomerId = "christopher.n.blundell@gmail.com",
                    DateCreated = new DateTime(2014, 12, 13),
                    Title = "Nice exhaust pipe",
                    Description = "I lost my virginity to THIS car!",
                }, 
                new Comment
                {
                    Id = 4,
                    CarId = 6,
                    CustomerId = "j.czepluch@gmail.com",
                    DateCreated = new DateTime(2014, 12, 14),
                    Title = "Hot wheels?",
                    Description = "Boy, would I like to take this car for a ride!"
                },
                new Comment
                {
                    Id = 5,
                    CarId = 6,
                    CustomerId = "andersfischern@me.com",
                    DateCreated = new DateTime(2014, 12, 15),
                    Title = "I feel sick!",
                    Description = "I was feeling so well and happy today, until I found this car. It's a horrible piece of shit.",
                }
            };

            context.Comments.AddOrUpdate(comment => comment.Id, comments.ToArray());
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
                    NoughtTo100 = 15.2f,
                    TopSpeed = 171
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
                    Year = 2005,
                    NoughtTo100 = 18.8f,
                    TopSpeed = 152
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
                    Year = 2008,
                    NoughtTo100 = 9.9f,
                    TopSpeed = 250
                },
                new Car
                {
                    Id = 4,
                    Make = "Audi",
                    Model = "A6",
                    Fuel = FuelType.Gasoline,
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 100000,
                    Drive = "RWD",
                    Mileage = 10,
                    Price = 500000,
                    Transmission = "Manual",
                    Year = 2010,
                    NoughtTo100 = 12,
                    TopSpeed = 195
                },
                new Car
                {
                    Id = 5,
                    Year = 2002,
                    Price = 25000,
                    Make = "Fiat",
                    Model = "Punto",
                    Color = "Silver",
                    Created = DateTime.Now,
                    Fuel = FuelType.Gasoline,
                    Drive = "FWD",
                    DistanceDriven = 486000,
                    Mileage = 14,
                    NoughtTo100 = 14.3f,
                    TopSpeed = 155,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 6,
                    Year = 2013,
                    Price = 100000,
                    Make = "Lexus",
                    Model = "RX450",
                    Color = "White",
                    Created = DateTime.Now,
                    Fuel = FuelType.Electric,
                    Drive = "FWD",
                    DistanceDriven = 25000,
                    Mileage = 14.9f,
                    NoughtTo100 = 8,
                    TopSpeed = 180,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 7,
                    Year = 2008,
                    Price = 88000,
                    Make = "Mini",
                    Model = "Clubman Cooper",
                    Color = "Red",
                    Created = DateTime.Now,
                    Fuel = FuelType.Diesel,
                    Drive = "FWD",
                    DistanceDriven = 174000,
                    Mileage = 17.5f,
                    NoughtTo100 = 14.3f,
                    TopSpeed = 155,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 8,
                    Year = 2008,
                    Price = 95000,
                    Make = "Lotus",
                    Model = "Elise",
                    Color = "Pink",
                    Created = DateTime.Now,
                    Fuel = FuelType.Gasoline,
                    Drive = "FWD",
                    DistanceDriven = 90000,
                    Mileage = 7.5f,
                    NoughtTo100 = 5.2f,
                    TopSpeed = 241,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 9,
                    Year = 1988,
                    Price = 15000,
                    Make = "Volkswagen",
                    Model = "Golf",
                    Color = "Black",
                    Created = DateTime.Now,
                    Fuel = FuelType.Gasoline,
                    Drive = "FWD",
                    DistanceDriven = 300000,
                    Mileage = 7,
                    NoughtTo100 = 23f,
                    TopSpeed = 145,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 10,
                    Year = 2005,
                    Price = 40000,
                    Make = "Skoda",
                    Model = "Fabia",
                    Color = "Dark Green",
                    Created = DateTime.Now,
                    Fuel = FuelType.Gasoline,
                    Drive = "FWD",
                    DistanceDriven = 29000,
                    Mileage = 10.9f,
                    NoughtTo100 = 15.9f,
                    TopSpeed = 160,
                    Transmission = "Manual"
                },
                new Car
                {
                    Id = 11,
                    Year = 2005,
                    Price = 70000,
                    Make = "Skoda",
                    Model = "Octavia",
                    Color = "Red",
                    Created = DateTime.Now,
                    Fuel = FuelType.Gasoline,
                    Drive = "FWD",
                    DistanceDriven = 100001,
                    Mileage = 10.3f,
                    NoughtTo100 = 15.3f,
                    TopSpeed = 171,
                    Transmission = "Manual"
                }
            };

            context.Cars.AddOrUpdate(c => c.Id, cars.ToArray());
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
