﻿using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CustomerViewModel : IViewModelBase
    {
        private readonly CustomerDto _customerDto;

        public enum CustomerEnum
        {
            NotInSystem,
            InSystem
        }

        public string CustomerId
        {
            get
            {
                return _customerDto.Id;
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
            GravatarLink = GravatarController.CreateGravatarLink(_customerDto.Email);
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

        private string _gravatarLink;
        public string GravatarLink
        {
            get { return _gravatarLink; }
            set
            {
                _gravatarLink = value;
                NotifyPropertyChanged("GravatarLink");
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
                GravatarLink = GravatarController.CreateGravatarLink(_customerDto.Email);
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
            throw new NotImplementedException("Fix password thingy");
            var customerController = new CustomerController();
            //await customerController.CreateCustomer(_customerDto, password);
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
