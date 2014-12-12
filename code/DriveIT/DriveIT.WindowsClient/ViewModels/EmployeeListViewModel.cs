using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeListViewModel : IViewModelBase
    {
    public ObservableCollection<EmployeeViewModel> EmployeeViewModels { get; set; }


        public EmployeeListViewModel()
        {
            EmployeeViewModels = new ObservableCollection<EmployeeViewModel>();
            SetCorrectDeletePermission();
            UpdateList();
        }

        private void SetCorrectDeletePermission()
        {
            if (EmployeeDetailsViewModel.LoggedInRole == Role.Administrator) CanDelete = true;
            else CanDelete = false;
        }

        #region Properties

        private EmployeeViewModel _selectedEmployee;
        public EmployeeViewModel SelectedEmployee
        {
            get { return _selectedEmployee; }

            set
            {
                _selectedEmployee = value;
                NotifyPropertyChanged("SelectedEmployee");
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

        private bool _canDelete;
        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                _canDelete = value;
                NotifyPropertyChanged("CanDelete");
            }
        }

        private bool _canUpdate;
        public bool CanUpdate
        {
            get { return _canUpdate; }
            set
            {
                _canUpdate = value;
                NotifyPropertyChanged("CanUpdate");
            }
        }

        #endregion Properties

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

        #region CRUDS
        public async void UpdateList()
        {
            try
            {
                Status = "";
                EmployeeViewModels.Clear();
                var employeeController = new EmployeeController();
                foreach (EmployeeDto employeeDtoDto in await employeeController.ReadEmployeeList())
                {
                    EmployeeViewModels.Add(new EmployeeViewModel(employeeDtoDto));
                }
                if (EmployeeViewModels.Count >= 1)
                {
                    SelectedEmployee = EmployeeViewModels[0];
                    SetCorrectDeletePermission();
                    CanUpdate = true;
                }
                else
                {
                    CanUpdate = false;
                    CanDelete = false;
                }
            }
            catch (Exception)
            {
                CanUpdate = false;
                CanDelete = false;
                Status = "Failed to update the list!";
            }
        }

        public void DeleteEmployee()
        {
            try
            {
                if (!String.IsNullOrEmpty(SelectedEmployee.EmployeeId)) SelectedEmployee.DeleteEmployee();
                else
                {
                    EmployeeViewModels.Remove(SelectedEmployee);
                    if (EmployeeViewModels.Count >= 1)
                    {
                        SelectedEmployee = EmployeeViewModels[0];
                        SetCorrectDeletePermission();
                        CanUpdate = true;
                    }
                    else
                    {
                        CanUpdate = false;
                        CanDelete = false;
                    }
                }
                Status = "";
            }
            catch (Exception)
            {

                Status = "Failed to delete the employee!";
            }
        }

        public void CreateNewEmployeeWindow()
        {
            try
            {
                var newEmployee = new EmployeeViewModel();
                var window = new EntityEmployeeWindow { DataContext = newEmployee };
                EmployeeViewModels.Add(newEmployee);
                window.Show();
                Status = "";
            }
            catch (Exception)
            {
                Status = "Failed to create window!";
            }
        }

        public void UpdateEmployeeWindow()
        {
            try
            {
                EmployeeViewModel employee = SelectedEmployee;
                var window = new EntityEmployeeWindow { DataContext = employee };
                window.Show();
                Status = "";
            }
            catch (Exception)
            {
                Status = "Failed to update window!";
            }
        }

        #endregion CRUDS
    }
}
