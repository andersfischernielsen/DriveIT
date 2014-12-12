using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

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
            try
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
            catch (Exception)
            {
                
                Status = "Failed to save customer!";
            }
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateCustomer()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    var passwordViewModel = new PasswordCreationViewModel(_customerDto);
                    var window = new PasswordCreationView(passwordViewModel);
                    window.ShowDialog();
                    {
                        if (passwordViewModel.ProfileCreated)
                        {
                            CustomerId = Email;
                            Status = "Customer Created";
                            CustomerState = CustomerEnum.InSystem;
                        }
                        else
                        {
                            Status = "Did not create customer";
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                Status = "Failed to create customer!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateCustomer()
        {
            try
            {
                var customerController = new CustomerController();
                await customerController.UpdateCustomer(_customerDto);
                Status = "Customer Updated";
            }
            catch (Exception)
            {
                
                Status = "Failed to update customer!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteCustomer()
        {
            try
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
            catch (Exception)
            {
                
                Status = "Failed to delete customer!";
            }
        }
        #endregion CRUDS

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A method for notifying the view if any properties have been changed.
        /// <param name="info">The name of the property which has changed</param>
        /// </summary>
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
