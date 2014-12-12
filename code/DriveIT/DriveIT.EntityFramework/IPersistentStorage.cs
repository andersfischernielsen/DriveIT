using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.EntityFramework.Entities;

namespace DriveIT.EntityFramework
{
    /// <summary>
    /// IPersistentStorage is a class for communicating with and underlying SQL database. 
    /// The class enables CRUD functionality of the Entities found in the DriveIT.Entities project.
    /// The documentation for other methods than the Car-related methods has been omitted due to 
    /// the similarity of their functionality and time pressure.
    /// </summary>
    public interface IPersistentStorage
    {
        #region Car

        /// <summary>
        /// Retrieve a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for fetching the entity.
        /// </summary>
        /// <param name="idToGet">The ID of the wanted Car.</param>
        /// <returns>A Car entity with the specific ID.</returns>
        Task<Car> GetCarWithId(int idToGet);

        /// <summary>
        /// Retrieve a List of all Car entities. 
        /// Optionally use a specified DriveITContext for fetching the entities.
        /// </summary>
        /// <returns>A List of all Car entities.</returns>
        Task<List<Car>> GetAllCars();

        /// <summary>
        /// Save a Car entity.
        /// Optionally use a specified DriveITContext for saving the entity.
        /// </summary>
        /// <param name="carToCreate">The Car to save.</param>
        /// <returns>An integer of entities changed in the database.</returns>
        Task<int> CreateCar(Car carToCreate);

        /// <summary>
        /// Update a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for updating the entity.
        /// </summary>
        /// <param name="idToUpdate">The ID of the wanted Car.</param>
        /// <param name="carToReplaceWith">The Car entity to use for updating (by overwriting).</param>
        /// <returns>A Car entity with the specific ID.</returns>
        Task UpdateCar(int idToUpdate, Car carToReplaceWith);

        /// <summary>
        /// Delete a Car entity with a specific ID. 
        /// Optionally use a specified DriveITContext for deleting the entity.
        /// </summary>
        /// <param name="id">The ID of the Car to delete.</param>
        /// <returns>A Car entity with the specific ID.</returns>
        Task<int> DeleteCar(int id);

        #endregion
        #region Employee
        Task<Employee> GetEmployeeWithId(string idToGet);

        Task<List<Employee>> GetAllEmployees();

        Task UpdateEmployee(string idToUpdate, Employee employeeToReplaceWith);

        Task<int> DeleteEmployee(string idToDelete);
        #endregion
        #region Customer
        Task<Customer> GetCustomerWithId(string idToGet);

        Task<List<Customer>> GetAllCustomers();

        Task UpdateCustomer(string idToUpdate, Customer customerToReplaceWith);

        Task<int> DeleteCustomer(string idToDelete);
        #endregion
        #region ContactRequest
        Task<ContactRequest> GetContactRequestWithId(int idToGet);

        Task<List<ContactRequest>> GetAllContactRequests();

        Task<int> CreateContactRequest(ContactRequest contactRequestToCreate);

        Task UpdateContactRequest(int idToUpdate,
            ContactRequest contactRequestToReplaceWith);

        Task<int> DeleteContactRequest(int idToDelete);
        #endregion
        #region Comment
        Task<Comment> GetCommentWithId(int idToGet);

        Task<List<Comment>> GetAllCommentsForCar(int carId);

        Task<int> CreateComment(Comment commentToCreate);

        Task UpdateComment(int idToUpdate, Comment commentToReplaceWith);

        Task<int> DeleteComment(int idToDelete);
        #endregion
        #region Sale

        /// <summary>
        /// Retrieve a Sale entity with a specific ID. 
        /// Optionally use a specified DriveITContext for fetching the entity.
        /// </summary>
        /// <param name="idToGet">The ID of the wanted Sale.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Sale entity with the specific ID.</returns>
        Task<Sale> GetSaleWithId(int idToGet, DriveITContext optionalContext = null);

        /// <summary>
        /// Retrieve a List of all Sale entities. 
        /// Optionally use a specified DriveITContext for fetching the entities.
        /// </summary>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entities.</param>
        /// <returns>A List of all Sale entities.</returns>
        Task<List<Sale>> GetAllSales(DriveITContext optionalContext = null);

        /// <summary>
        /// Get a Sale entity with a specific Car ID. 
        /// Optionally use a specified DriveITContext for updating the entity.
        /// </summary>
        /// <param name="carId">The ID of the Car of the wanted Sale.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Sale entity with the specific Car ID.</returns>
        Task<Sale> GetSaleByCarId(int carId, DriveITContext optionalContext = null);

        Task<int> CreateSale(Sale saleToCreate, DriveITContext optionalContext = null);

        /// <summary>
        /// Update a Sale entity with a specific ID. 
        /// Optionally use a specified DriveITContext for updating the entity.
        /// </summary>
        /// <param name="idToUpdate">The ID of the wanted Sale.</param>
        /// <param name="saleToReplaceWith">The Sale entity to use for updating (by overwriting).</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Sale entity with the specific ID.</returns>
        Task UpdateSale(int idToUpdate, Sale saleToReplaceWith, DriveITContext optionalContext = null);

        /// <summary>
        /// Delete a Sale entity with a specific ID. 
        /// Optionally use a specified DriveITContext for deleting the entity.
        /// </summary>
        /// <param name="idToDelete">The ID of the Sale to delete.</param>
        /// <param name="optionalContext">An optional DriveITContext to use for getting the entity.</param>
        /// <returns>A Sale entity with the specific ID.</returns>
        Task<int> DeleteSale(int idToDelete, DriveITContext optionalContext = null);
        #endregion
    }
}
