using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeViewModel : IViewModelBase
    {
        public enum EmployeeStateEnum
        {
            NotInSystem,
            InSystem
        }
        private EmployeeDto _employeeDto;

        public int? EmployeeId
        {
            get
            {
                try
                {
                    return _employeeDto.Id.Value;
                }
                catch (Exception)
                {

                    return null;
                }

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
        }

        public EmployeeViewModel()
        {
            _employeeDto = new EmployeeDto();
            EmployeeState = EmployeeStateEnum.NotInSystem;
        }


        #region Attributes
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
                    case EmployeeStateEnum.InSystem:
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
                return _employeeDto.Username;
            }
            set
            {
                _employeeDto.Username = value;
                NotifyPropertyChanged("Username");
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

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.CreateEmployee(_employeeDto);
            Status = "Employee Created";
            EmployeeState = EmployeeStateEnum.InSystem;
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.UpdateEmployee(_employeeDto);
            Status = "Employee Updated";
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.DeleteEmployee(_employeeDto);
            EmployeeId = null;
            Status = "Employee Deleted";
            EmployeeState = EmployeeStateEnum.NotInSystem;
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
