using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerViewModel : IViewModelBase
    {
        private CustomerDto _customerDto;

        public enum CustomerEnum
        {
            NotInSystem,
            InSystem
        }

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
            CustomerState = CustomerEnum.InSystem;
        }
        public CustomerViewModel()
        {
            _customerDto = new CustomerDto();
            CustomerState = CustomerEnum.NotInSystem;
        }

        #region ATTRIBUTES
        private string _status = "";
        public string Status
        {
            get
            {
                try
                {
                    return _status;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }



        private CustomerEnum _actualCustomerState;
        public CustomerEnum CustomerState
        {
            get { return _actualCustomerState; }
            set
            {
                _actualCustomerState = value;
                NotifyPropertyChanged("CreateUpdateButtonText");
            }
        }
        public string CreateUpdateButtonText
        {
            get
            {
                switch (CustomerState)
                {
                    case CustomerEnum.NotInSystem:
                        return "Create";
                    default:
                        return "Update";
                }
            }
        }
        public string Username
        {
            get
            {
                return _customerDto.Username;
            }
            set
            {
                _customerDto.Username = value;
                NotifyPropertyChanged("Username");
            }
        }
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
        public void SaveCustomer()
        {
            switch (CustomerState)
            {
                case CustomerEnum.NotInSystem:
                    CreateCustomer();
                    break;
                default:
                    UpdateCustomer();
                    break;
            }
        }
        
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateCustomer()
        {
            var customerController = new CustomerController();
            await customerController.CreateCustomer(_customerDto);
            Status = "Customer Created";
            CustomerState = CustomerEnum.InSystem;
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateCustomer()
        {
            var customerController = new CustomerController();
            await customerController.UpdateCustomer(_customerDto);
            Status = "Customer Updated";
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteCustomer()
        {
            if (CustomerState != CustomerEnum.NotInSystem)
            {
                var customerController = new CustomerController();
                await customerController.DeleteCustomer(_customerDto);
                CustomerId = null;
                Status = "Customer Deleted";
                CustomerState = CustomerEnum.NotInSystem;
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
