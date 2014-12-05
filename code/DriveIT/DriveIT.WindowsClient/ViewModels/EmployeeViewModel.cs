using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeViewModel : IViewModelBase
    {
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
        }

        public EmployeeViewModel()
        {

        }


        #region Attributes
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

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.CreateEmployee(_employeeDto);
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.UpdateEmployee(_employeeDto);
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteEmployee()
        {
            var employeeController = new EmployeeController();
            employeeController.DeleteEmployee(_employeeDto);
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
