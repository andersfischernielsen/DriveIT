using System;
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

        /// <summary>
        /// The default constructor. Calling the UpdateList() method initially to load in data.
        /// </summary>
        public EmployeeListViewModel()
        {
            EmployeeViewModels = new ObservableCollection<EmployeeViewModel>();
            SetCorrectDeletePermission();
            UpdateList();
        }
        /// <summary>
        /// A method for insuring users with the role Administator are allowed to delete other users.
        /// </summary>
        private void SetCorrectDeletePermission()
        {
            if (EmployeeDetailsViewModel.LoggedInRole == Role.Administrator) CanDelete = true;
            else CanDelete = false;
        }
        /// <summary>
        /// Getters and setters for the attributes of a SaleDTO while notifying view.
        /// </summary>  
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
        /// <summary>
        /// Initializes the list of Employees and sets them to be editable.
        /// </summary>
        public async void UpdateList()
        {
            try
            {
                Status = "";
                EmployeeViewModels.Clear();
                var employeeController = new EmployeeController();
                EmployeeViewModels = new ObservableCollection<EmployeeViewModel>((await employeeController.ReadEmployeeList()).Select(employee => new EmployeeViewModel(employee)));
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
                NotifyPropertyChanged("");
            }
            catch (Exception)
            {
                CanUpdate = false;
                CanDelete = false;
                Status = "Failed to update the list!";
            }
        }
        /// <summary>
        /// Deletes the selected Employee.
        /// </summary>
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
        /// <summary>
        /// Creates a window to create a new Employee.
        /// </summary>
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
        /// <summary>
        /// Opens the EmployeeWindow with the information of the selected Employee.
        /// </summary>
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
