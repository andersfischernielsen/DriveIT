﻿using System;
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

        public EmployeeListViewModel(IEnumerable<EmployeeDto> employeeDtos)
        {
            EmployeeViewModels = new ObservableCollection<EmployeeViewModel>(
                employeeDtos
                .Select(employeeDto => new EmployeeViewModel(employeeDto)));
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
        public async void ReadList()
        {
            try
            {
                var employeeController = new EmployeeController();
                foreach (EmployeeDto employeeDto in await employeeController.ReadEmployeeList())
                {
                    EmployeeViewModels.Add(new EmployeeViewModel(employeeDto));
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public async void UpdateList()
        {
            try
            {
                EmployeeViewModels.Clear();
                var employeeController = new EmployeeController();
                foreach (EmployeeDto employeeDtoDto in await employeeController.ReadEmployeeList())
                {
                    EmployeeViewModels.Add(new EmployeeViewModel(employeeDtoDto));
                }
            }
            catch (Exception e)
            {
                
                throw;
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
                    SelectedEmployee = null;
                }
            }
            catch (Exception e)
            {
                
                throw;
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
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void UpdateEmployeeWindow()
        {
            try
            {
                EmployeeViewModel employee = SelectedEmployee;
                var window = new EntityEmployeeWindow { DataContext = employee };
                window.Show();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        #endregion CRUDS
    }
}
