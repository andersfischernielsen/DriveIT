using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT_Windows_Client.Controllers
{
    public class EmployeeController
    {
        public EmployeeController()
        {
            TestMethod();
        }

        private void TestMethod()
        {
            var t = ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
            try
            {
                CreateEmployee(t[0]);
            }
            catch (Exception)
            {
                CreateEmployee(new EmployeeDto()
                {
                    Username = "sexydude123",
                    FirstName = "Mr Handsome",
                    LastName = "Cake"
                });
            }
            Thread.Sleep(5000);
            t = ReadEmployeeList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + ReadEmployee(t[t.Count - 1].Id.Value).Result.FirstName);
            int id = t[0].Id.Value;
            CreateEmployee(new EmployeeDto()
            {
                Username = "sexydude123",
                FirstName = "Mr Not So Handsome",
                LastName = "Cookie"
            });
            Thread.Sleep(5000);
            t = ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + ReadEmployee(t[t.Count - 1].Id.Value).Result.FirstName);

            DeleteEmployee(t[0].Id.Value);
            Thread.Sleep(5000);
            t = ReadEmployeeList().Result;
            Console.WriteLine(t.Count);
        }

        public async void CreateEmployee(EmployeeDto employee)
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
        public async void UpdateEmployee(EmployeeDto employee)
        {
            await DriveITWebAPI.Update("employees", employee, employee.Id.Value);
        }
        public async void DeleteEmployee(EmployeeDto employee)
        {
            await DriveITWebAPI.Delete<EmployeeDto>("employees", employee.Id.Value);
        }
        public async void DeleteEmployee(int id)
        {
            await DriveITWebAPI.Delete<EmployeeDto>("employees", id);
        }
    }
}
