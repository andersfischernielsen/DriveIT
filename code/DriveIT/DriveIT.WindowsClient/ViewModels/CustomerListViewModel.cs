using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerListViewModel : IViewModelBase
    {
        public ObservableCollection<CustomerViewModel> CustomerViewModels { get; set; }

        public CustomerListViewModel(IList<CustomerDto> customerDtos)
        {
            CustomerViewModels = new ObservableCollection<CustomerViewModel>();
            foreach (CustomerDto customerDto in customerDtos)
            {
                CustomerViewModels.Add(new CustomerViewModel(customerDto));
            }
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

        #region CRUD

        public async void ReadList()
        {
            var customerController = new CustomerController();
            foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
            {
                CustomerViewModels.Add(new CustomerViewModel(customerDto));
            }
        }
        public async void UpdateList()
        {
            CustomerViewModels.Clear();
            var customerController = new CustomerController();
            foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
            {
                CustomerViewModels.Add(new CustomerViewModel(customerDto));
            }
        }
        public void DeleteCustomer()
        {
            if (!String.IsNullOrEmpty(SelectedCustomer.CustomerId)) SelectedCustomer.DeleteCustomer();
            else
            {
                CustomerViewModels.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        public void CreateNewCustomerWindow()
        {
            var newCustomer = new CustomerViewModel();
            var window = new EntityCustomerWindow { DataContext = newCustomer };
            CustomerViewModels.Add(newCustomer);
            window.Show();
        }

        public void UpdateCustomerWindow()
        {
            CustomerViewModel customer = SelectedCustomer;
            var window = new EntityCustomerWindow { DataContext = customer };
            window.Show();
        }

        #endregion CRUD

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
