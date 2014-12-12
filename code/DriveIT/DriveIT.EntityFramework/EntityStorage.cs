using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.EntityFramework.Entities;

namespace DriveIT.EntityFramework
{
    /// <summary>
    /// EntityStorage is a class for communicating with and underlying SQL database. 
    /// The class enables CRUD functionality of the Entities found in the DriveIT.Entities project.
    /// The documentation for other methods than the Car-related methods has been omitted due to 
    /// the similarity of their functionality and time pressure.
    /// </summary>
    public class EntityStorage : IPersistentStorage
    {
        #region Car
        /// <summary>
        /// Retrieve a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for fetching the entity.
        /// </summary>
        /// <param name="idToGet">The ID of the wanted Car.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Car entity with the specific ID.</returns>
        public async Task<Car> GetCarWithId(int idToGet, DriveITContext optionalContext = null)
        {
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                return await optionalContext.Cars.Include(car => car.ImagePaths).SingleOrDefaultAsync(car => car.Id == idToGet);
            }
        }

        /// <summary>
        /// Retrieve a List of all Car entities. 
        /// Optionally use a specified DriveITContext for fetching the entities.
        /// </summary>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entities.</param>
        /// <returns>A List of all Car entities.</returns>
        public async Task<List<Car>> GetAllCars(DriveITContext optionalContext = null)
        {
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                return await optionalContext.Cars.Include(car => car.ImagePaths).ToListAsync();
            }
        }

        /// <summary>
        /// Save a Car entity.
        /// Optionally use a specified DriveITContext for saving the entity.
        /// </summary>
        /// <param name="carToCreate">The Car to save.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for saving the entity.</param>
        /// <returns>An integer of entities changed in the database.</returns>
        public async Task<int> CreateCar(Car carToCreate, DriveITContext optionalContext = null)
        {
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                optionalContext.Cars.Add(carToCreate);
                //optionalContext.ImagePaths.AddRange(carToCreate.ImagePaths);
                await optionalContext.SaveChangesAsync();
                return carToCreate.Id;
            }
        }

        /// <summary>
        /// Update a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for updating the entity.
        /// </summary>
        /// <param name="idToUpdate">The ID of the wanted Car.</param>
        /// <param name="carToReplaceWith">The Car entity to use for updating (by overwriting).</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Car entity with the specific ID.</returns>
        public async Task UpdateCar(int idToUpdate, Car carToReplaceWith, DriveITContext optionalContext = null)
        {
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                optionalContext.ImagePaths
                    .RemoveRange(
                        optionalContext.ImagePaths
                        .Where(imagePath => imagePath.CarId == idToUpdate));
                var oldCar = await optionalContext.Cars.FindAsync(idToUpdate);
                CopyCarProperties(oldCar, carToReplaceWith);

                await optionalContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for deleting the entity.
        /// </summary>
        /// <param name="id">The ID of the Car to delete.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Car entity with the specific ID.</returns>
        public async Task<int> DeleteCar(int id, DriveITContext optionalContext = null)
        {
            if (optionalContext == null) optionalContext = new DriveITContext();

            using (optionalContext)
            {
                var toRemove = await optionalContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
                optionalContext.Cars.Remove(toRemove);
                return await optionalContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// A method for replacing the properties of one Car entity with the properties of another Car entity.
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
        public async Task<Sale> GetSaleWithId(int idToGet)
        {
            using (var context = new DriveITContext())
            {
                return await context.Sales.FindAsync(idToGet);
            }
        }

        public async Task<List<Sale>> GetAllSales()
        {
            using (var context = new DriveITContext())
            {
                return await context.Sales.ToListAsync();
            }
        }

        public async Task<Sale> GetSaleByCarId(int carId)
        {
            using (var context = new DriveITContext())
            {
                return await context.Sales.SingleOrDefaultAsync(sale => sale.CarId == carId);
            }
        }

        public async Task<int> CreateSale(Sale saleToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Sales.Add(saleToCreate);
                await context.SaveChangesAsync();
                return saleToCreate.Id;
            }
        }

        public async Task UpdateSale(int idToUpdate, Sale saleToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                var oldSale = await context.Sales.FindAsync(idToUpdate);
                CopySaleProperties(oldSale, saleToReplaceWith);

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteSale(int idToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Sales.Remove(await context.Sales.FindAsync(idToDelete));
                return await context.SaveChangesAsync();
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