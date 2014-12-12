using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeViewModel : IViewModelBase
    {
        public enum EmployeeStateEnum
        {
            NotInSystem,
            InSystem
        }
        private readonly EmployeeDto _employeeDto;

        public string EmployeeId
        {
            get
            {
                return _employeeDto.Id;
            }
            set
            {
                _employeeDto.Id = value;
                NotifyPropertyChanged("EmployeeId");
            }
        }
        


        public EmployeeViewModel(EmployeeDto employeeDto)
        {
            _employeeDto = employeeDto;
            EmployeeState = EmployeeStateEnum.InSystem;
            GravatarLink = GravatarController.CreateGravatarLink(_employeeDto.Email);

            if (EmployeeDetailsViewModel.LoggedInRole == Role.Administrator) CanDeleteAndUpdate = true;
            else CanDeleteAndUpdate = false;
        }

        public EmployeeViewModel()
        {
            _employeeDto = new EmployeeDto();
            EmployeeState = EmployeeStateEnum.NotInSystem;

            if (EmployeeDetailsViewModel.LoggedInRole == Role.Administrator) CanDeleteAndUpdate = true;
            else CanDeleteAndUpdate = false;
        }


        #region Attributes
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

        private EmployeeStateEnum _actualEmployeeState = EmployeeStateEnum.InSystem;
        public EmployeeStateEnum EmployeeState
        {
            get { return _actualEmployeeState; }
            set
            {
                _actualEmployeeState = value;
                NotifyPropertyChanged("CreateUpdateButtonText");
            }
        }
        public string CreateUpdateButtonText
        {
            get
            {
                switch (EmployeeState)
                {
                    case EmployeeStateEnum.NotInSystem:
                        return "Create";
                    default:
                        return "Update";
                }
            }
        }

        public string Email
        {
            get
            {
                return _employeeDto.Email;
            }
            set
            {
                _employeeDto.Email = value;
                GravatarLink = GravatarController.CreateGravatarLink(_employeeDto.Email);
                NotifyPropertyChanged("Email");
            }
        }

        public string JobTitle
        {
            get
            {
                return _employeeDto.JobTitle;
            }
            set
            {
                _employeeDto.JobTitle = value;
                NotifyPropertyChanged("JobTitle");
            }
        }

        public string FirstName
        {
            get
            {
                return _employeeDto.FirstName;
            }
            set
            {
                _employeeDto.FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return _employeeDto.LastName;
            }
            set
            {
                _employeeDto.LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public string Phone
        {
            get
            {
                return _employeeDto.Phone;
            }
            set
            {
                _employeeDto.Phone = value;
                NotifyPropertyChanged("Phone");
            }
        }



        #endregion Attributes

        #region CRUDS

        public void SaveEmployee()
        {
            try
            {
                switch (EmployeeState)
                {
                    case EmployeeStateEnum.NotInSystem:
                        CreateEmployee();
                        break;
                    default:
                        UpdateEmployee();
                        break;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to save employee!";
            }
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateEmployee()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    var passwordViewModel = new PasswordCreationViewModel(_employeeDto);
                    var window = new PasswordCreationView(passwordViewModel);
                    window.ShowDialog();
                    {
                        if (passwordViewModel.ProfileCreated)
                        {
                            EmployeeId = Email;
                            Status = "Employee created";
                            EmployeeState = EmployeeStateEnum.InSystem;
                        }
                        else
                        {
                            Status = "Did not create employee";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Status = "Failed to create employee!";
            }
            
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateEmployee()
        {
            try
            {
                var employeeController = new EmployeeController();
                await employeeController.UpdateEmployee(_employeeDto);
                Status = "Employee Updated";
            }
            catch (Exception e)
            {

                Status = "Failed to update employee!";
            }
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteEmployee()
        {
            try
            {
                if (EmployeeState != EmployeeStateEnum.NotInSystem)
                {
                    var employeeController = new EmployeeController();
                    await employeeController.DeleteEmployee(_employeeDto);
                    EmployeeId = null;
                    Status = "Employee Deleted";
                    EmployeeState = EmployeeStateEnum.NotInSystem;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to delete employee!";
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
