using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public class EntityStorage : IPersistentStorage
    {

        public Car GetCarWithId(int id)
        {
            using (var context = new DriveITContext())
            {
                return context.Cars.Select(x => x).FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            using (var context = new DriveITContext())
            {
                return context.Cars.Select(cars => cars).ToList();
            }
        }

        public async Task<int> CreateCar(Car carToCreate)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Add(carToCreate);
                await context.SaveChangesAsync();
                return carToCreate.Id;
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }

        public async void UpdateCar(int idToUpdate, Car carToReplaceWith)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.AddOrUpdate(x => x.Id, carToReplaceWith);
                await context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }

        public async void DeleteCar(int id)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Remove(await context.Cars.FindAsync(id));
            }
        }

        public Employee GetEmployeeWithId(int idToGet)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateEmployee(Employee employeeToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEmployee(int idToUpdate, Employee employeeToReplaceWith)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEmployee(int idToDelete)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetCustomerWithId(int idToGet)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateCustomer(Customer customerToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCustomer(int idToUpdate, Customer customerRequestToReplaceWith)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer(int idToDelete)
        {
            throw new System.NotImplementedException();
        }

        public ContactRequest GetContactRequestWithId(int idToGet)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ContactRequest> GetAllContactRequests()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateContactRequest(ContactRequest contactRequestToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateContactRequest(int idToUpdate, ContactRequest contactRequestToReplaceWith)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteContactRequest(int idToDelete)
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

        public Task<int> CreateComment(Comment commentToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateComment(int idToUpdate, Comment commentToReplaceWith)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteComment(int idToDelete)
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

        public Task<int> CreateSale(Sale commentToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSale(int idToUpdate, Sale saleToReplaceWith)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSale(Sale saleToDelete)
        {
            throw new System.NotImplementedException();
        }

        public async void DeleteCar(Car carToDelete)
        {
            using (var context = new DriveITContext())
            {
                context.Cars.Remove(carToDelete);
                await context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }
    }
}