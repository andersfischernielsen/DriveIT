using System.Collections.Generic;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        Car GetCarWithId(int id);
        IEnumerable<Car> GetAllCars();
        void CreateCar(Car carToCreate);
        void UpdateCar(int idToUpdate, Car carToReplaceWith);
        void DeleteCar(int id);

        Employee GetEmployeeWithId(int idToGet);
        IEnumerable<Employee> GetAllEmployees();
        void CreateEmployee(Employee employeeToCreate);
        void UpdateEmployee(int idToUpdate, Employee employeeToReplaceWith);
        void DeleteEmployee(int idToDelete);

        Customer GetCustomerWithId(int idToGet);
        IEnumerable<Customer> GetAllCustomers();
        void CreateCustomer(Customer customerToCreate);
        void UpdateCustomer(int idToUpdate, Customer customerRequestToReplaceWith);
        void DeleteCustomer(int idToDelete);

        ContactRequest GetContactRequestWithId(int idToGet);
        IEnumerable<ContactRequest> GetAllContactRequests();
        void CreateContactRequest(ContactRequest contactRequestToCreate);
        void UpdateContactRequest(int idToUpdate, ContactRequest contactRequestToReplaceWith);
        void DeleteContactRequest(int idToDelete);

        Comment GetCommentWithId(int id);
        IEnumerable<Comment> GetAllCommentsForCar(Car car);
        void CreateComment(Comment commentToCreate);
        void UpdateComment(int idToUpdate, Comment commentToReplaceWith);
        void DeleteComment(int idToDelete);

        Sale GetSaleWithId(int id);
        IEnumerable<Sale> GetAllSales();
        void CreateSale(Sale commentToCreate);
        void UpdateSale(int idToUpdate, Sale saleToReplaceWith);
        void DeleteSale(Sale saleToDelete);
    }
}
