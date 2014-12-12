using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerListViewModel : IViewModelBase
    {
        public ObservableCollection<CustomerViewModel> CustomerViewModels { get; set; }

        public CustomerListViewModel(IEnumerable<CustomerDto> customerDtos)
        {
            CustomerViewModels = 
                new ObservableCollection<CustomerViewModel>(customerDtos
                .Select(customerDto => new CustomerViewModel(customerDto)));
        }

        public CustomerListViewModel()
        {
            CustomerViewModels = new ObservableCollection<CustomerViewModel>();
            ReadList();
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get { return _selectedCustomer; }

            set
            {
                _selectedCustomer = value;
                NotifyPropertyChanged("SelectedCustomer");
            }
        }

        private string _status = "";

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        #region CRUDS

        public async void ReadList()
        {
            try
            {
                var customerController = new CustomerController();
                foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
                {
                    CustomerViewModels.Add(new CustomerViewModel(customerDto));
                }
            }
            catch (Exception e)
            {

                Status = "Failed to retrieve data for customers!";
            }
        }
        public async void UpdateList()
        {
            try
            {
                CustomerViewModels.Clear();
                var customerController = new CustomerController();
                foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
                {
                    CustomerViewModels.Add(new CustomerViewModel(customerDto));
                }
            }
            catch (Exception e)
            {

                Status = "Failed to update the list of customers!";
            }
        }
        public void DeleteCustomer()
        {
            try
            {
                if (!String.IsNullOrEmpty(SelectedCustomer.CustomerId)) SelectedCustomer.DeleteCustomer();
                else
                {
                    CustomerViewModels.Remove(SelectedCustomer);
                    SelectedCustomer = null;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to delete the customer!";
            }
        }

        public void CreateNewCustomerWindow()
        {
            try
            {
                var newCustomer = new CustomerViewModel();
                var window = new EntityCustomerWindow { DataContext = newCustomer };
                CustomerViewModels.Add(newCustomer);
                window.Show();
            }
            catch (Exception e)
            {

                Status = "Failed to create window!";
            }
        }

        public void UpdateCustomerWindow()
        {
            try
            {
                CustomerViewModel customer = SelectedCustomer;
                var window = new EntityCustomerWindow { DataContext = customer };
                window.Show();
            }
            catch (Exception e)
            {

                Status = "Failed to update window!";
            }
        }

        #endregion CRUDS

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
