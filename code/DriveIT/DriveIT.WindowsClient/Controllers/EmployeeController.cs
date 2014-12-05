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
        public async Task<EmployeeDto> ReadEmployee(int id)
        {
            var employeeToReturn = await DriveITWebAPI.Read<EmployeeDto>("employees/" + id);
            return employeeToReturn;
        }
        public async Task<IList<EmployeeDto>> ReadEmployeeList()
        {
            var employees = await DriveITWebAPI.ReadList<EmployeeDto>("employees");
            return employees;
        }
        public async Task UpdateEmployee(EmployeeDto employee)
        {
            await DriveITWebAPI.Update("employees", employee, employee.Id.Value);
        }
        public async Task DeleteEmployee(EmployeeDto employee)
        {
            await DriveITWebAPI.Delete<EmployeeDto>("employees", employee.Id.Value);
        }
        public async void DeleteEmployee(int id)
        {
            await DriveITWebAPI.Delete<EmployeeDto>("employees", id);
        }
    }
}
