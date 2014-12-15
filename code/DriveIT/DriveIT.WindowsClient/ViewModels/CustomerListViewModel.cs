using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerListViewModel : IViewModelBase
    {
        public ObservableCollection<CustomerViewModel> CustomerViewModels { get; set; }

        /// <summary>
        /// The default constructor. Calling the UpdateList() method initially to load in data.
        /// </summary>
        public CustomerListViewModel()
        {
            CustomerViewModels = new ObservableCollection<CustomerViewModel>();
            UpdateList();
        }
        /// <summary>
        /// Getters and setters for the attributes of a CustomerDTO while notifying view.
        /// </summary>
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
        /// <summary>
        /// Initializes the list of Customers and sets them to be editable.
        /// </summary>
        #region CRUDS
        public async void UpdateList()
        {
            try
            {
                Status = "";
                CustomerViewModels.Clear();
                var customerController = new CustomerController();
                CustomerViewModels = new ObservableCollection<CustomerViewModel>((await customerController.ReadCustomerList()).Select(customer => new CustomerViewModel(customer)));
                if (CustomerViewModels.Count >= 1)
                {
                    SelectedCustomer = CustomerViewModels[0];
                    CanDeleteAndUpdate = true;
                }
                else
                {
                    CanDeleteAndUpdate = false;
                }
                NotifyPropertyChanged("");
            }
            catch (Exception)
            {
                CanDeleteAndUpdate = false;
                Status = "Failed to update the list of customers!";
            }
        }
        /// <summary>
        /// Deletes the selected Customer.
        /// </summary>
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
        /// <summary>
        /// Creates a window to create a new Customer.
        /// </summary>
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
        /// <summary>
        /// Opens the CustomerWindow with the information of the selected Customer.
        /// </summary>
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
