﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT_Windows_Client.Controllers
{
    public class CustomerController
    {
        public CustomerController()
        {
        }

        public async void CreateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Create("customers", customer);
        }

        public async Task<CustomerDto> ReadCustomer(int id)
        {
            var customerToReturn = await DriveITWebAPI.Read<CustomerDto>("customers/" + id);
            return customerToReturn;
        }

        public async Task<IList<CustomerDto>> ReadCustomerList()
        {
            var customers = await DriveITWebAPI.ReadList<CustomerDto>("customers");
            return customers;
        }
        public async void UpdateCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Update("customers", customer, customer.Id.Value);
        }

        public async void DeleteCustomer(CustomerDto customer)
        {
            await DriveITWebAPI.Delete<CustomerDto>("customers", customer.Id.Value);
        }

        public async void DeleteCustomer(int id)
        {
            await DriveITWebAPI.Delete<CustomerDto>("customers", id);
        }
    }
}