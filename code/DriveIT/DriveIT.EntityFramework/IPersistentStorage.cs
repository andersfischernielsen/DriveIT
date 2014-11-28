using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        Task<Car> GetCarWithId(int idToGet);
        Task<IEnumerable<Car>> GetAllCars();
        Task<int> CreateCar(Car carToCreate);
        Task<int> UpdateCar(int idToUpdate, Car carToReplaceWith);
        Task<int> DeleteCar(int id);

        Task<Employee> GetEmployeeWithId(int idToGet);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<int> CreateEmployee(Employee employeeToCreate);
        Task<int> UpdateEmployee(int idToUpdate, Employee employeeToReplaceWith);
        Task<int> DeleteEmployee(int idToDelete);

        Task<Customer> GetCustomerWithId(int idToGet);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<int> CreateCustomer(Customer customerToCreate);
        Task<int> UpdateCustomer(int idToUpdate, Customer customerRequestToReplaceWith);
        Task<int> DeleteCustomer(int idToDelete);

        Task<ContactRequest> GetContactRequestWithId(int idToGet);
        Task<IEnumerable<ContactRequest>> GetAllContactRequests();
        Task<int> CreateContactRequest(ContactRequest contactRequestToCreate);
        Task<int> UpdateContactRequest(int idToUpdate, ContactRequest contactRequestToReplaceWith);
        Task<int> DeleteContactRequest(int idToDelete);

        Task<Comment> GetCommentWithId(int idToGet);
        Task<IEnumerable<Comment>> GetAllCommentsForCar(int carId);
        Task<int> CreateComment(Comment commentToCreate);
        Task<int> UpdateComment(int idToUpdate, Comment commentToReplaceWith);
        Task<int> DeleteComment(int idToDelete);

        Task<Sale> GetSaleWithId(int idToGet);
        Task<IEnumerable<Sale>> GetAllSales();
        Task<int> CreateSale(Sale saleToCreate);
        Task<int> UpdateSale(int idToUpdate, Sale saleToReplaceWith);
        Task<int> DeleteSale(int idToDelete);
    }
}
