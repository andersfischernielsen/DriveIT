using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT_Windows_Client.Controllers;
using DriveIT_Windows_Client.ViewModels;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CarViewModel : IViewModelBase
    {
        private CarDto _carDto;

        public int CarId
        {
            get
            {
                return _carDto.Id.GetValueOrDefault();
            }
            set
            {
                _carDto.Id = value;
                NotifyPropertyChanged("CarId");
            }
        }
        public string CarModel
        {
            get
            {
                return _carDto.Model;
            }
            set
            {
                _carDto.Model = value;
                NotifyPropertyChanged("CarModel");
            }
        }
        public string CarMake
        {
            get
            {
                return _carDto.Make;
            }
            set
            {
                _carDto.Make = value;
                NotifyPropertyChanged("CarMake");
            }
        }

        
        public CarViewModel(CarDto carDto)
        {
            _carDto = carDto;
        }
        public CarViewModel()
        {
            
        }

        #region CRUDS
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateCar()
        {
            var carController = new CarController();
            carController.CreateCar(new CarDto()
            {
                Id = CarId,
                Model = CarModel,
                Make = CarMake
            });
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateCar()
        {
            var carController = new CarController();
            carController.UpdateCar(_carDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteCar()
        {
            var carController = new CarController();
            carController.DeleteCar(_carDto);
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
