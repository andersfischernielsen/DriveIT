using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class EmployeeController
    {
        public EmployeeController()
        {

        }

        public async Task CreateEmployee(EmployeeDto employee)
        {
            await DriveITWebAPI.Create("employees", employee);
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
