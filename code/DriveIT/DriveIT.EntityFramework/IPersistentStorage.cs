using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        Task<Car> GetCarWithId(int id);
        IEnumerable<Car> GetAllCars();
        void CreateCar(Car carToCreate);
        void DeleteCar(Car carToDelete);

        Employee GetEmployeeWithId(int id);
        IEnumerable<Employee> GetAllEmployees();
        void CreateEmployee(Employee employeeToCreate);
        void DeleteEmployee(Employee employeeToDelete);

        Customer GetCustomerWithId(int id);
        IEnumerable<Customer> GetAllCustomers();
        void CreateCustomer(Customer customerToCreate);
        void DeleteCustomer(Customer customerToDelete);

        ContactRequest GetContactRequestWithId(int id);
        IEnumerable<ContactRequest> GetAllContactRequests();
        void CreateContactRequest(ContactRequest contactRequestToCreate);
        void DeleteContactRequest(ContactRequest contactRequestToDelete);

        Comment GetCommentWithId(int id);
        IEnumerable<Comment> GetAllCommentsForCar(Car car);
        void CreateComment(Comment commentToCreate);
        void DeleteComment(Comment commentToDelete);

        Sale GetSaleWithId(int id);
        IEnumerable<Sale> GetAllSales();
        void CreateSale(Sale commentToCreate);
        void DeleteSale(Sale saleToDelete);
    }
}
