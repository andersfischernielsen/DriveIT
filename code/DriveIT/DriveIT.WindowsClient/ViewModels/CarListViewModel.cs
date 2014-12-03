using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

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

        public async void UpdateList()
        {
            CarViewModels.Clear();
            var carController = new CarController();
            foreach (CarDto carDto in await carController.ReadCarList())
            {
                CarViewModels.Add(new CarViewModel(carDto));
            }
        }

        public void CreateNewCarWindow()
        {
            CarViewModel newCar = new CarViewModel(new CarDto());
            var window = new EntityCarWindow();
            window.DataContext = newCar;
            CarViewModels.Add(newCar);
            window.Show();
        }
        #endregion CRUD
    }
}
