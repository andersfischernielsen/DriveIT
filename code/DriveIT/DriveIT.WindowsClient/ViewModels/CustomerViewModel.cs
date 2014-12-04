using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerViewModel : IViewModelBase
    {
        private CustomerDto _customerDto;

        public int? CustomerId
        {
            get
            {
                try
                {
                    return _customerDto.Id.Value;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            set
            {
                _customerDto.Id = value;
                NotifyPropertyChanged("CustomerId");
            }
        }

        public CustomerViewModel(CustomerDto customerDto)
        {
            _customerDto = customerDto;
        }
        public CustomerViewModel()
        {
            
        }

        #region ATTRIBUTES
        
        public string FirstName
        {
            get
            {
                return _customerDto.FirstName;
            }
            set
            {
                _customerDto.FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return _customerDto.LastName;
            }
            set
            {
                _customerDto.LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public string Phone
        {
            get
            {
                return _customerDto.Phone;
            }
            set
            {
                _customerDto.Phone = value;
                NotifyPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get
            {
                return _customerDto.Email;
            }
            set
            {
                _customerDto.Email = value;
                NotifyPropertyChanged("Email");
            }
        }
        #endregion ATTRIBUTES

        #region CRUDS
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateCustomer()
        {
            var customerController = new CustomerController();
            customerController.CreateCustomer(_customerDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateCustomer()
        {
            var customerController = new CustomerController();
            customerController.UpdateCustomer(_customerDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteCustomer()
        {
            var customerController = new CustomerController();
            customerController.DeleteCustomer(_customerDto);
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
