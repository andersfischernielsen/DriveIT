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

        /// <summary>
        /// Creates a Employee DTO object in the API.
        /// </summary>
        /// <param name="employee">An Employee DTO</param>
        /// <param name="password">The desired password for the employee</param>
        /// /// <param name="role">The desired role for the employee (either Administrator or Employee)</param>
        /// <returns>Returns the newly created Employee DTO from the database</returns>
        public async Task CreateEmployee(EmployeeDto employee, string password, Role role)
        {
            if (role != Role.Administrator || role != Role.Employee) throw new ArgumentException("Cannot be Customer.", "role");
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
        /// <summary>
        /// Reads a specific Employee DTO object from the API.
        /// </summary>
        /// <param name="email">The email of the desired Employee DTO</param>
        /// <returns>Returns the Employee with the respective email from the database</returns>
        public async Task<EmployeeDto> ReadEmployee(string email)
        {
            string search = "?email=" + email;
            var employeeToReturn = await DriveITWebAPI.Read<EmployeeDto>("employees/" + search);
            return employeeToReturn;
        }
        /// <summary>
        /// Reads the list of Employee DTO objects from the API.
        /// </summary>
        /// <returns>Returns the list of Employee DTO's from the database</returns>
        public async Task<IList<EmployeeDto>> ReadEmployeeList()
        {
            var employees = await DriveITWebAPI.ReadList<EmployeeDto>("employees");
            return employees;
        }
        /// <summary>
        /// Updates the Employee DTO sent to the API.
        /// </summary>
        /// <param name="employee">The Employee DTO to be updated</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task UpdateEmployee(EmployeeDto employee)
        {
            string search = "?email=" + employee.Email;
            await DriveITWebAPI.Update("employees/" + search, employee);
        }
        /// <summary>
        /// Deletes the selected Employee DTO from the API.
        /// </summary>
        /// <param name="employee">The Employee DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteEmployee(EmployeeDto employee)
        {
            string search = "?email=" + employee.Email;
            await DriveITWebAPI.Delete<EmployeeDto>("employees/" + search);
        }
        /// <summary>
        /// Deletes the selected Employee DTO from the API with the given id.
        /// </summary>
        /// <param name="email">The email of the Employee DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteEmployee(string email)
        {
            string search = "?email=" + email;
            await DriveITWebAPI.Delete<EmployeeDto>("employees/" + search);
        }
    }
}
