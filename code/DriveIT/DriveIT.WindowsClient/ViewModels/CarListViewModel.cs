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
    public class CarListViewModel : IViewModelBase
    {
        public ObservableCollection<CarViewModel> CarViewModels { get; set; }
        private CarViewModel _selectedCar;
        public CarViewModel SelectedCar
        {
            get { return _selectedCar; }

            set
            {
                _selectedCar = value;
                NotifyPropertyChanged("SelectedCar");
            }
        }

        public CarListViewModel(IEnumerable<CarDto> carDtos)
        {
            CarViewModels = new ObservableCollection<CarViewModel>(
                carDtos
                .Select(carDto => new CarViewModel(carDto)));
        }
        public CarListViewModel()
        {
            CarViewModels = new ObservableCollection<CarViewModel>();
            ReadList();
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
                var carController = new CarController();
                foreach (CarDto carDto in await carController.ReadCarList())
                {
                    CarViewModels.Add(new CarViewModel(carDto));
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
                CarViewModels.Clear();
                var carController = new CarController();
                foreach (CarDto carDto in await carController.ReadCarList())
                {
                    CarViewModels.Add(new CarViewModel(carDto));
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void DeleteCar()
        {
            try
            {
                if (SelectedCar.CarId.HasValue) SelectedCar.DeleteCar();
                else
                {
                    CarViewModels.Remove(SelectedCar);
                    SelectedCar = null;
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void CreateNewCarWindow()
        {
            try
            {
                var newCar = new CarViewModel();
                var window = new EntityCarWindow { DataContext = newCar };
                CarViewModels.Add(newCar);
                window.Show();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void UpdateCarWindow()
        {
            try
            {
                CarViewModel car = SelectedCar;
                var window = new EntityCarWindow { DataContext = car };
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
