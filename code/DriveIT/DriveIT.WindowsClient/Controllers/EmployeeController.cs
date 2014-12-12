using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    /// <summary>
    /// A controller which creates the strings to CRUD Employees in the DriveITWebAPI class
    /// </summary>
    public class EmployeeController
    {
        public EmployeeController()
        {

        }

        public async Task CreateEmployee(EmployeeDto employee, string password, Role role)
        {
            if (role == Role.Customer) throw new ArgumentException("Cannot be Customer.", "role");
            var registerModel = new RegisterViewModel
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.Phone,
                ConfirmPhoneNumber = employee.Phone,
                Password = password,
                ConfirmPassword = password,
                Role = role
            };
            await DriveITWebAPI.Create("account/register", registerModel);
        }   
        public async Task<EmployeeDto> ReadEmployee(string email)
        {
            string search = "?id=" + email;
            var employeeToReturn = await DriveITWebAPI.Read<EmployeeDto>("employees/" + search);
            return employeeToReturn;
        }
        public async Task<IList<EmployeeDto>> ReadEmployeeList()
        {
            var employees = await DriveITWebAPI.ReadList<EmployeeDto>("employees");
            return employees;
        }
        public async Task UpdateEmployee(EmployeeDto employee)
        {
            string search = "?id=" + employee.Email;
            await DriveITWebAPI.Update("employees/" + search, employee);
        }
        public async Task DeleteEmployee(EmployeeDto employee)
        {
            string search = "?id=" + employee.Email;
            await DriveITWebAPI.Delete<EmployeeDto>("employees/" + search);
        }
        public async void DeleteEmployee(int email)
        {
            string search = "?id=" + email;
            await DriveITWebAPI.Delete<EmployeeDto>("employees/" + search);
        }
    }
}
