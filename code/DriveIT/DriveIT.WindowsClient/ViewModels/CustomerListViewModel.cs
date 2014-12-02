using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

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

        #region CRUD

        public async void ReadList()
        {
            var customerController = new CustomerController();
            foreach (CustomerDto customerDto in await customerController.ReadCustomerList())
            {
                CustomerViewModels.Add(new CustomerViewModel(customerDto));
            }
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
