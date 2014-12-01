using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT_Windows_Client.Controllers;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CarListViewModel : IViewModelBase
    {
        public ObservableCollection<CarViewModel> CarViewModels { get; set; }

        public CarListViewModel(IList<CarDto> carDtos)
        {
            CarViewModels = new ObservableCollection<CarViewModel>();
            foreach (CarDto carDto in carDtos)
            {
                CarViewModels.Add(new CarViewModel(carDto));
            }
        }
        public CarListViewModel()
        {
            CarViewModels = new ObservableCollection<CarViewModel>();
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
            var carController = new CarController();
            foreach (CarDto carDto in await carController.ReadCarList())
            {
                CarViewModels.Add(new CarViewModel(carDto));
            }
        }
        #endregion CRUD
    }
}
