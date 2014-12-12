using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.EntityFramework.Entities;

namespace DriveIT.EntityFramework
{
    public class EntityStorage : IPersistentStorage
    {
        #region Car
        public async Task<Car> GetCarWithId(int idToGet)
        {
            //Dispose the context as soon as we are done with it, so we don't end up with a massive 
            //object when running.
            using (var context = new DriveITContext())
            {
                //Retrieve a car with the given ID and include its ImagePaths stored in the ImagePaths DbSet.
                return await context.Cars.Include(car => car.ImagePaths).SingleOrDefaultAsync(car => car.Id == idToGet);
            }
        }

        public async Task<List<Car>> GetAllCars()
        {
            using (var context = new DriveITContext())
            {
                //Retrieve all cars and their ImagePaths from the ImagePaths DbSet.
                return await context.Cars.Include(car => car.ImagePaths).ToListAsync();
            }
        }

        public async Task<int> CreateCar(Car carToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Add(carToCreate);
                await context.SaveChangesAsync();
                return carToCreate.Id;
            }
        }

        public async Task UpdateCar(int idToUpdate, Car carToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                //Delete the old images for the car.
                //This Done by finding ImagePaths for a given carId.
                context.ImagePaths
                    .RemoveRange(context.ImagePaths
                        .Where(imagePath => imagePath.CarId == idToUpdate));

                //Find the old car with the Id to update.
                var oldCar = await context.Cars.FindAsync(idToUpdate);
                //Copy all properties. DriveITContext has some functionality (OnModelCreating) 
                //behind the scenes to make sure that the new ImagePaths are copied properly as well.
                CopyCarProperties(oldCar, carToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteCar(int id)
        {
            using (var context = new DriveITContext())
            {
                //Remove the car (if found) from the DbSet.
                var toRemove = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
                context.Cars.Remove(toRemove);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// A method for replacing the properties of one Car entity with the properties of another Car entity.
        /// This copies the reference (or the value if primitive) to the properties of the new Car.
        /// </summary>
        /// <param name="toChange">The Car entity whose properties will be replaced.</param>
        /// <param name="toSetFrom">The Car entity whose properties will be used for replacing.</param>
        private void CopyCarProperties(Car toChange, Car toSetFrom)
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
            toChange.Transmission = toSetFrom.Transmission;
            toChange.Year = toSetFrom.Year;
            toChange.TopSpeed = toSetFrom.TopSpeed;
            toChange.NoughtTo100 = toSetFrom.NoughtTo100;
            toChange.ImagePaths = toSetFrom.ImagePaths;
        }

        #endregion

        #region Employee
        public async Task<Employee> GetEmployeeWithId(string idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Employees.SingleOrDefaultAsync(x => x.Id == idToGet);
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            using (var context = new DriveITContext())
            {
                return await context.Employees.ToListAsync();
            }
        }

        public async Task UpdateEmployee(string idToUpdate, Employee employeeToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldEmployee = await context.Employees.SingleOrDefaultAsync(x => x.Id == idToUpdate);
                CopyEmployeeProperties(oldEmployee, employeeToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteEmployee(string idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Users.Remove(await context.Employees.SingleAsync(x => x.Id == idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private void CopyEmployeeProperties(Employee toChange, Employee toSetFrom)
        {
            toChange.Email = toSetFrom.Email;
            toChange.FirstName = toSetFrom.FirstName;
            toChange.LastName = toSetFrom.LastName;
            toChange.PhoneNumber = toSetFrom.PhoneNumber;
            toChange.JobTitle = toSetFrom.JobTitle;
        }
        #endregion
        #region Customer
        public async Task<Customer> GetCustomerWithId(string idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Customers.SingleOrDefaultAsync(x => x.Id == idToGet);
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            using (var context = new DriveITContext())
            {
                return await context.Customers.ToListAsync();
            }
        }

        public async Task UpdateCustomer(string idToUpdate, Customer customerToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldCustomer = await context.Customers.SingleAsync(x => x.Id == idToUpdate);
                CopyCustomerProperties(oldCustomer, customerToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteCustomer(string idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Users.Remove(await context.Customers.SingleAsync(x => x.Id == idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private void CopyCustomerProperties(Customer toChange, Customer toSetFrom)
        {
            toChange.Email = toSetFrom.Email;
            toChange.FirstName = toSetFrom.FirstName;
            toChange.LastName = toSetFrom.LastName;
            toChange.PhoneNumber = toSetFrom.PhoneNumber;
        }
        #endregion
        #region ContactRequest
        public async Task<ContactRequest> GetContactRequestWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.ContactRequests.FindAsync(idToGet);
            }
        }

        public async Task<List<ContactRequest>> GetAllContactRequests()
        {
            using (var context = new DriveITContext())
            {
                return await context.ContactRequests.ToListAsync();
            }
        }

        public async Task<int> CreateContactRequest(ContactRequest contactRequestToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.ContactRequests.Add(contactRequestToCreate);
                await context.SaveChangesAsync();
                return contactRequestToCreate.Id;
            }
        }

        public async Task UpdateContactRequest(int idToUpdate, ContactRequest contactRequestToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldRequest = await context.ContactRequests.FindAsync(idToUpdate);
                CopyContactRequestProperties(oldRequest, contactRequestToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteContactRequest(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.ContactRequests.Remove(await context.ContactRequests.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }
        private void CopyContactRequestProperties(ContactRequest toChange, ContactRequest toSetFrom)
        {
            toChange.CarId = toSetFrom.CarId;
            toChange.Created = toSetFrom.Created;
            toChange.CustomerId = toSetFrom.CustomerId;
            toChange.EmployeeId = toSetFrom.EmployeeId;
        }
        #endregion
        #region Comment
        public async Task<Comment> GetCommentWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Comments.FindAsync(idToGet);
            }
        }

        public async Task<List<Comment>> GetAllCommentsForCar(int carId)
        {
            using (var context = new DriveITContext())
            {
                return await context.Comments.Where(c => c.CarId == carId).ToListAsync();
            }
        }

        public async Task<int> CreateComment(Comment commentToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Comments.Add(commentToCreate);
                await context.SaveChangesAsync();
                return commentToCreate.Id;
            }
        }

        public async Task UpdateComment(int idToUpdate, Comment commentToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldComment = await context.Comments.FindAsync(idToUpdate);
                CopyCommentProperties(oldComment, commentToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteComment(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Comments.Remove(await context.Comments.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
            }
        }

        private void CopyCommentProperties(Comment toChange, Comment toSetFrom)
        {
            toChange.CarId = toSetFrom.CarId;
            toChange.DateCreated = toSetFrom.DateCreated;
            toChange.Description = toSetFrom.Description;
            toChange.Title = toSetFrom.Title;
            toChange.CustomerId = toSetFrom.CustomerId;
        }
        #endregion
        #region Sale
        public async Task<Sale> GetSaleWithId(int idToGet, DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                return await optionalContext.Sales.FindAsync(idToGet);
            }
        }

        public async Task<List<Sale>> GetAllSales(DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                return await optionalContext.Sales.ToListAsync();
            }
        }

        public async Task<Sale> GetSaleByCarId(int carId, DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                return await optionalContext.Sales.SingleOrDefaultAsync(sale => sale.CarId == carId);
            }
        }

        public async Task<int> CreateSale(Sale saleToCreate, DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                optionalContext.Sales.Add(saleToCreate);
                await optionalContext.SaveChangesAsync();
                return saleToCreate.Id;
            }
        }

        public async Task UpdateSale(int idToUpdate, Sale saleToReplaceWith, DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                var oldSale = await optionalContext.Sales.FindAsync(idToUpdate);
                CopySaleProperties(oldSale, saleToReplaceWith);

                await optionalContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteSale(int idToDelete, DriveITContext optionalContext = null)
        {
            //If the optional DriveITContext is null, then instantiate a new context and use that.
            //This is done for testing purposes, where a mocked DriveITContext is injected.
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                optionalContext.Sales.Remove(await optionalContext.Sales.FindAsync(idToDelete));
                return await optionalContext.SaveChangesAsync();
            }
        }

        private void CopySaleProperties(Sale toChange, Sale toSetFrom)
        {
            toChange.CarId = toSetFrom.CarId;
            toChange.CustomerId = toSetFrom.CustomerId;
            toChange.DateOfSale = toSetFrom.DateOfSale;
            toChange.EmployeeId = toSetFrom.EmployeeId;
            toChange.Price = toSetFrom.Price;
        }
        #endregion
    }
}