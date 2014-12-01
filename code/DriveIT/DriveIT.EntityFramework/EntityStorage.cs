﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public static class EntityStorage
    {
        public static async Task<Car> GetCarWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Cars.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<Car>> GetAllCars()
        {
            using (var context = new DriveITContext())
            {
                return await context.Cars.Select(cars => cars).ToListAsync();
            }
        }

        public static async Task<int> CreateCar(Car carToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Add(carToCreate);
                await context.SaveChangesAsync();
                return carToCreate.Id;
            }
        }

        public static async Task<int> UpdateCar(int idToUpdate, Car carToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldCar = await context.Cars.FindAsync(idToUpdate);
                CopyCarProperties(oldCar, carToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteCar(int id)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Remove(await context.Cars.FindAsync(id));
                return await context.SaveChangesAsync();
            }
        }

        private static void CopyCarProperties(Car toChange, Car toSetFrom)
        {
            toChange.Color = toSetFrom.Color;
            toChange.Created = toSetFrom.Created;
            toChange.DistanceDriven = toSetFrom.DistanceDriven;
            toChange.Drive = toSetFrom.Drive;
            toChange.Fuel = toSetFrom.Fuel;
            toChange.Make = toSetFrom.Make;
            toChange.Mileage = toSetFrom.Mileage;
            toChange.Model = toSetFrom.Model;
            toChange.Price = toSetFrom.Price;
            toChange.Sold = toSetFrom.Sold;
            toChange.Transmission = toSetFrom.Transmission;
            toChange.Year = toSetFrom.Year;
        }

        public static async Task<Employee> GetEmployeeWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Employees.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            using (var context = new DriveITContext())
            {
                return await context.Employees.Select(empl => empl).ToListAsync();
            }
        }

        public static async Task<int> CreateEmployee(Employee employeeToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Employees.Add(employeeToCreate);
                await context.SaveChangesAsync();
                return employeeToCreate.Id;
            }
        }

        public static async Task<int> UpdateEmployee(int idToUpdate, Employee employeeToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldEmployee = await context.Employees.FindAsync(idToUpdate);
                CopyEmployeeProperties(oldEmployee, employeeToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteEmployee(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Remove(await context.Cars.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private static void CopyEmployeeProperties(Employee toChange, Employee toSetFrom)
        {
            toChange.Email = toSetFrom.Email;
            toChange.FirstName = toSetFrom.FirstName;
            toChange.LastName = toSetFrom.LastName;
            toChange.Password = toSetFrom.Password;
            toChange.PhoneNumber = toSetFrom.PhoneNumber;
            toChange.Username = toSetFrom.Username;
        }

        public static async Task<Customer> GetCustomerWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Customers.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using (var context = new DriveITContext())
            {
                return await context.Customers.Select(cust => cust).ToListAsync();
            }
        }

        public static async Task<int> CreateCustomer(Customer customerToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Customers.Add(customerToCreate);
                await context.SaveChangesAsync();
                return customerToCreate.Id;
            }
        }

        public static async Task<int> UpdateCustomer(int idToUpdate, Customer customerToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldCustomer = await context.Customers.FindAsync(idToUpdate);
                CopyCustomerProperties(oldCustomer, customerToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteCustomer(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Customers.Remove(await context.Customers.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private static void CopyCustomerProperties(Customer toChange, Customer toSetFrom)
        {
            toChange.Email = toSetFrom.Email;
            toChange.FirstName = toSetFrom.FirstName;
            toChange.LastName = toSetFrom.LastName;
            toChange.Password = toSetFrom.Password;
            toChange.PhoneNumber = toSetFrom.PhoneNumber;
            toChange.Username = toSetFrom.Username;
        }

        public static async Task<ContactRequest> GetContactRequestWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.ContactRequests.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<ContactRequest>> GetAllContactRequests()
        {
            using (var context = new DriveITContext())
            {
                return await context.ContactRequests.Select(req => req).ToListAsync();
            }
        }

        public static async Task<int> CreateContactRequest(ContactRequest contactRequestToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.ContactRequests.Add(contactRequestToCreate);
                await context.SaveChangesAsync();
                return contactRequestToCreate.Id;
            }
        }

        public static async Task<int> UpdateContactRequest(int idToUpdate, ContactRequest contactRequestToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldRequest = await context.ContactRequests.FindAsync(idToUpdate);
                CopyContactRequestProperties(oldRequest, contactRequestToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteContactRequest(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.ContactRequests.Remove(await context.ContactRequests.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }
        private static void CopyContactRequestProperties(ContactRequest toChange, ContactRequest toSetFrom)
        {
            toChange.Car = toSetFrom.Car;
            toChange.Created = toSetFrom.Created;
            toChange.Customer = toSetFrom.Customer;
        }

        public static async Task<Comment> GetCommentWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Comments.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<Comment>> GetAllCommentsForCar(int carId)
        {
            using (var context = new DriveITContext())
            {
                return await context.Comments.Where(c => c.Car.Id == carId).ToListAsync();
            }
        }

        public static async Task<int> CreateComment(Comment commentToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Comments.Add(commentToCreate);
                await context.SaveChangesAsync();
                return commentToCreate.Id;
            }
        }

        public static async Task<int> UpdateComment(int idToUpdate, Comment commentToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldComment = await context.Comments.FindAsync(idToUpdate);
                CopyCommentProperties(oldComment, commentToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteComment(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Comments.Remove(await context.Comments.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private static void CopyCommentProperties(Comment toChange, Comment toSetFrom)
        {
            toChange.Car = toSetFrom.Car;
            toChange.DateCreated = toSetFrom.DateCreated;
            toChange.Description = toSetFrom.Description;
            toChange.Title = toSetFrom.Title;
            toChange.Customer = toSetFrom.Customer;
        }

        public static async Task<Sale> GetSaleWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Sales.FindAsync(idToGet);
            }
        }

        public static async Task<IEnumerable<Sale>> GetAllSales()
        {
            using (var context = new DriveITContext())
            {
                return await context.Sales.Select(req => req).ToListAsync();
            }
        }

        public static async Task<int> CreateSale(Sale saleToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Sales.Add(saleToCreate);
                await context.SaveChangesAsync();
                return saleToCreate.Id;
            }
        }

        public static async Task<int> UpdateSale(int idToUpdate, Sale saleToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldSale = await context.Sales.FindAsync(idToUpdate);
                CopySaleProperties(oldSale, saleToReplaceWith);

                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> DeleteSale(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Sales.Remove(await context.Sales.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private static void CopySaleProperties(Sale toChange, Sale toSetFrom)
        {
            toChange.Car = toSetFrom.Car;
            toChange.Customer = toSetFrom.Customer;
            toChange.DateOfSale = toSetFrom.DateOfSale;
            toChange.Employee = toSetFrom.Employee;
            toChange.Price = toSetFrom.Price;
        }
    }
}