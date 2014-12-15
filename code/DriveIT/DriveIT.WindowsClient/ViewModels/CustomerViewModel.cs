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
        /// <summary>
        /// Enum for determining whether the Customer is in the system or not.
        /// </summary>
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
        /// <summary>
        /// Constructor for CustomerViewModel which updates the given CustomerDTO and updates the CustomerState.
        /// </summary>
        /// <param name="customerDto">The CustomerDTO to be updated</param>
        public CustomerViewModel(CustomerDto customerDto)
        {
            _customerDto = customerDto;
            CustomerState = CustomerEnum.InSystem;
            GravatarLink = GravatarController.CreateGravatarLink(_customerDto.Email);
        }
        /// <summary>
        /// Empty constructor which creates a new CustomerDTO.
        /// </summary>
        public CustomerViewModel()
        {
            _customerDto = new CustomerDto();
            CustomerState = CustomerEnum.NotInSystem;
        }
        /// <summary>
        /// Getters and setters for the attributes of a CustomerDTO while notifying view.
        /// </summary>
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
        /// <summary>
        /// Saves an Customer - updates the Customer if it exists, otherwise creates a new Customer.
        /// </summary>
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
        /// Creates a new CustomerController, creates an Customer from the API, and notifies the view.
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
        /// Updates the CustomerController, creates a Customer from the API, and notifies the view.
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
        /// Creates a CustomerController, deletes the Customer from the API, and notifies the view.
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
