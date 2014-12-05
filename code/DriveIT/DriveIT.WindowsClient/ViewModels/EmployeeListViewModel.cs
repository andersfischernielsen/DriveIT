using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

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
        #endregion CRUD
    }
}
