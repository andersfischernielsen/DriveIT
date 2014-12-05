using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeListViewModel : IViewModelBase
    {
    public ObservableCollection<EmployeeViewModel> EmployeeViewModels { get; set; }

        public EmployeeListViewModel(IList<EmployeeDto> employeeDtos)
        {
            EmployeeViewModels = new ObservableCollection<EmployeeViewModel>();
            foreach (EmployeeDto employeeDto in employeeDtos)
            {
                EmployeeViewModels.Add(new EmployeeViewModel(employeeDto));
            }
        }
        public EmployeeListViewModel()
        {
            EmployeeViewModels = new ObservableCollection<EmployeeViewModel>();
            ReadList();
        }

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

        #region CRUD
        public async void ReadList()
        {
            var employeeController = new EmployeeController();
            foreach (EmployeeDto employeeDto in await employeeController.ReadEmployeeList())
            {
                EmployeeViewModels.Add(new EmployeeViewModel(employeeDto));
            }
        }

        public async void UpdateList()
        {
            EmployeeViewModels.Clear();
            var employeeController = new EmployeeController();
            foreach (EmployeeDto employeeDtoDto in await employeeController.ReadEmployeeList())
            {
                EmployeeViewModels.Add(new EmployeeViewModel(employeeDtoDto));
            }
        }

        public void DeleteEmployee()
        {
            if(SelectedEmployee.EmployeeId.HasValue) SelectedEmployee.DeleteEmployee();
            else
            {
                EmployeeViewModels.Remove(SelectedEmployee);
                SelectedEmployee = null;
            }
        }

        public void CreateNewEmployeeWindow()
        {
            EmployeeViewModel newEmployee = new EmployeeViewModel();
            var window = new EntityEmployeeWindow {DataContext = newEmployee};
            EmployeeViewModels.Add(newEmployee);
            window.Show();
        }

        public void UpdateEmployeeWindow()
        {
            EmployeeViewModel employee = SelectedEmployee;
            var window = new EntityEmployeeWindow {DataContext = employee};
            window.Show();
        }

        #endregion CRUD
    }
}
