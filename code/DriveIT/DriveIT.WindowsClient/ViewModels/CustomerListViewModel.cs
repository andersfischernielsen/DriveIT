using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;
using DriveIT_Windows_Client.Controllers;

namespace DriveIT_Windows_Client.ViewModels
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
            var carController = new CarController();
            foreach (CarDto carDto in await carController.ReadCarList())
            {
                CustomerViewModels.Add(new CarViewModel(carDto));
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
