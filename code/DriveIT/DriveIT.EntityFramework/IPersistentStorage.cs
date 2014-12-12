using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        #region Car
        Task<Car> GetCarWithId(int idToGet, DriveITContext optionalContext = null);

        Task<List<Car>> GetAllCars(DriveITContext optionalContext = null);

        Task<int> CreateCar(Car carToCreate, DriveITContext optionalContext = null);

        Task UpdateCar(int idToUpdate, Car carToReplaceWith,
            DriveITContext optionalContext = null);

        Task<int> DeleteCar(int id, DriveITContext optionalContext = null);
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
        Task<Sale> GetSaleWithId(int idToGet);

        Task<List<Sale>> GetAllSales();

        Task<Sale> GetSaleByCarId(int carId);

        Task<int> CreateSale(Sale saleToCreate);

        Task UpdateSale(int idToUpdate, Sale saleToReplaceWith);

        Task<int> DeleteSale(int idToDelete);
        #endregion
    }
}
