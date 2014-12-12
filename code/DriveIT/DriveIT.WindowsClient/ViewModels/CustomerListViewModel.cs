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

        public CustomerListViewModel()
        {
            CustomerViewModels = new ObservableCollection<CustomerViewModel>();
            UpdateList();
        }

        #region Properties

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
        private bool _canDeleteAndUpdate;

        public bool CanDeleteAndUpdate
        {
            get { return _canDeleteAndUpdate; }
            set
            {
                _canDeleteAndUpdate = value;
                NotifyPropertyChanged("CanDeleteAndUpdate");
            }
        }
        #endregion Properties

        #region CRUDS
        public async void UpdateList()
        {
            try
            {
                Status = "";
                CustomerViewModels.Clear();
                var customerController = new CustomerController();
                foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
                {
                    CustomerViewModels.Add(new CustomerViewModel(customerDto));
                }
                if (CustomerViewModels.Count >= 1)
                {
                    SelectedCustomer = CustomerViewModels[0];
                    CanDeleteAndUpdate = true;
                }
                else
                {
                    CanDeleteAndUpdate = false;
                }
            }
            catch (Exception)
            {
                CanDeleteAndUpdate = false;
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
                    if (CustomerViewModels.Count >= 1)
                    {
                        SelectedCustomer = CustomerViewModels[0];
                        CanDeleteAndUpdate = true;
                    }
                    else
                    {
                        CanDeleteAndUpdate = false;
                    }
                }
                Status = "";
            }
            catch (Exception)
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
                Status = "";
            }
            catch (Exception)
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
                Status = "";
            }
            catch (Exception)
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
