using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class PasswordCreationViewModel : IViewModelBase
    {
        private CustomerDto _customerDto;
        private EmployeeDto _employeeDto;

        public bool ProfileCreated { get; set; }

        public PasswordCreationViewModel()
        {
            Roletypes = new[] { Role.Customer, Role.Employee, Role.Administrator};
        }

        public PasswordCreationViewModel(CustomerDto customerDto)
        {
            ProfileCreated = false;
            Roletypes = new[] {Role.Customer};

            _customerDto = customerDto;

            SelectedRole = Role.Customer;
            UsernameString = customerDto.Email;
        }

        public PasswordCreationViewModel(EmployeeDto employeeDto)
        {
            ProfileCreated = false;
            Roletypes = new[] {Role.Employee, Role.Administrator};
            
            _employeeDto = employeeDto;

            SelectedRole = Role.Employee;
            UsernameString = employeeDto.Email;
        }

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

        #region Properties

        public Action CloseAction { get; set; }

        private bool _canCreateProfile = false;

        public bool CanCreateProfile
        {
            get { return _canCreateProfile; }
            set
            {
                _canCreateProfile = value;
                NotifyPropertyChanged("CanCreateProfile");
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

        private bool _isRoleLocked;

        public bool IsRoleLocked
        {
            get { return _isRoleLocked; }
            set
            {
                _isRoleLocked = value;
                NotifyPropertyChanged("IsRoleLocked");
            }
        }

        private Role _selectedRole;

        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                NotifyPropertyChanged("SelectedRole");
                CheckPassword();
            }
        }

        public Role[] Roletypes { get; set; }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
                CheckPassword();
            }
        }

        private string _confirmationPassword;

        public string ConfirmationPassword
        {
            get { return _confirmationPassword; }
            set
            {
                _confirmationPassword = value;
                NotifyPropertyChanged("ConfirmationPassword");
                CheckPassword();
            }
        }

        private string _usernameString = "Username: ";

        public string UsernameString
        {
            get { return _usernameString; }
            set
            {
                _usernameString = "Username: " + value;
                NotifyPropertyChanged("UsernameString");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Dont know how to do this better
        /// </summary>
        private void CheckPassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                return;
            }
            if (Password.Length < 6 || Password.Length > 100)
            {
                Status = "Password must be between 6 and 100 characters.";
                CanCreateProfile = false;
                return;
            }
            if (!Password.Any(char.IsDigit))
            {
                Status = "Password must contain a digit";
                CanCreateProfile = false;
                return;
            }
            if (!Password.Any(char.IsUpper) || !Password.Any(char.IsLower))
            {
                Status = "Password must contain an upper case and lowercase letter";
                CanCreateProfile = false;
                return;
            }
            if (!Password.Equals(ConfirmationPassword))
            {
                Status = "Password and confirmation password must be equal";
                CanCreateProfile = false;
                return;
            }
            if (SelectedRole != Role.Administrator && SelectedRole != Role.Employee && SelectedRole != Role.Customer)
            {
                Status = "A Role must be choosen.";
                CanCreateProfile = false;
                return;
            }
            Status = "Password OK";
            CanCreateProfile = true;
        }

        public async void CreateProfile()
        {
            if (CanCreateProfile)
            {
                switch (SelectedRole)
                {
                    case Role.Customer:
                    {
                        try
                        {
                            var customerController = new CustomerController();
                            await customerController.CreateCustomer(_customerDto, Password);
                            ProfileCreated = true;
                        }
                        catch (Exception)
                        {
                            Status = "Failed to create profile";
                        }
                        break;
                    }
                    default:
                    {
                        try
                        {
                            var employeeController = new EmployeeController();
                            await employeeController.CreateEmployee(_employeeDto, Password, SelectedRole);
                            ProfileCreated = true;
                        }
                        catch (Exception)
                        {
                            Status = "Failed to create profile";
                        }
                        break;
                    }
                }
                CloseAction.Invoke();
            }
        }

        #endregion Methods
    }
}