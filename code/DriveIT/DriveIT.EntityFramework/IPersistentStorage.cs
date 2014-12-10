using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        Task<Car> GetCarWithId(int idToGet, DriveITContext optionalContext = null);

        Task<List<Car>> GetAllCars(DriveITContext optionalContext = null);

        Task<int> CreateCar(Car carToCreate, DriveITContext optionalContext = null);

        Task UpdateCar(int idToUpdate, Car carToReplaceWith,
            DriveITContext optionalContext = null);

        Task<int> DeleteCar(int id, DriveITContext optionalContext = null);

        Task<Employee> GetEmployeeWithId(string idToGet);

        Task<List<Employee>> GetAllEmployees();

        Task UpdateEmployee(string idToUpdate, Employee employeeToReplaceWith);

        Task<int> DeleteEmployee(string idToDelete);

        Task<Customer> GetCustomerWithId(string idToGet);

        Task<List<Customer>> GetAllCustomers();

        Task UpdateCustomer(string idToUpdate, Customer customerToReplaceWith);

        Task<int> DeleteCustomer(string idToDelete);

        Task<ContactRequest> GetContactRequestWithId(int idToGet);

        Task<List<ContactRequest>> GetAllContactRequests();

        Task<int> CreateContactRequest(ContactRequest contactRequestToCreate);

        Task UpdateContactRequest(int idToUpdate,
            ContactRequest contactRequestToReplaceWith);

        Task<int> DeleteContactRequest(int idToDelete);

        Task<Comment> GetCommentWithId(int idToGet);

        Task<List<Comment>> GetAllCommentsForCar(int carId);

        Task<int> CreateComment(Comment commentToCreate);

        Task UpdateComment(int idToUpdate, Comment commentToReplaceWith);

        Task<int> DeleteComment(int idToDelete);

        Task<Sale> GetSaleWithId(int idToGet);

        Task<List<Sale>> GetAllSales();

        Task<int> CreateSale(Sale saleToCreate);

        Task UpdateSale(int idToUpdate, Sale saleToReplaceWith);

        Task<int> DeleteSale(int idToDelete);

        Task<List<ImagePath>> GetImagePathsForCar(int carId);

        Task<int> CreateImagePath(ImagePath imagePath);

        Task RemoveImagePath(int idToDelete);
    }
}
