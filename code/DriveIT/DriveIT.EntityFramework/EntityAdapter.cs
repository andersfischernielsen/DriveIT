using System.Collections.Generic;
using System.Linq;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public class EntityAdapter : IPersistentStorage
    {

        public Car GetCarWithId(int id)
        {
            using (var context = new EntityContext())
            {
                return context.Cars.Select(x => x).FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            using (var context = new EntityContext())
            {
                return context.Cars.Select(cars => cars).ToList();
            }
        }

        public async void CreateCar(Car carToCreate)
        {
            using (var context = new EntityContext())
            {
                context.Cars.Add(carToCreate);
                await context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }

        public async void DeleteCar(Car carToDelete)
        {
            using (var context = new EntityContext())
            {
                context.Cars.Remove(carToDelete);
                await context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }

        public Employee GetEmployeeWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            throw new System.NotImplementedException();
        }

        public void CreateEmployee(Employee employeeToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEmployee(Employee employeeToDelete)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetCustomerWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCustomer(Customer customerToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer(Customer customerToDelete)
        {
            throw new System.NotImplementedException();
        }

        public ContactRequest GetContactRequestWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ContactRequest> GetAllContactRequests()
        {
            throw new System.NotImplementedException();
        }

        public void CreateContactRequest(ContactRequest contactRequestToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteContactRequest(ContactRequest contactRequestToDelete)
        {
            throw new System.NotImplementedException();
        }

        public Comment GetCommentWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Comment> GetAllCommentsForCar(Car car)
        {
            throw new System.NotImplementedException();
        }

        public void CreateComment(Comment commentToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteComment(Comment commentToDelete)
        {
            throw new System.NotImplementedException();
        }

        public Sale GetSaleWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Sale> GetAllSales()
        {
            throw new System.NotImplementedException();
        }

        public void CreateSale(Sale commentToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSale(Sale saleToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}