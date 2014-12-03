using System;
using System.ComponentModel;
using System.Windows;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CarViewModel : IViewModelBase
    {
        private CarDto _carDto;

        public enum CarState
        {
            Initial,
            ForSale,
            Advertised,
            Sold,
        }

        public CarViewModel(CarDto carDto)
        {
            _carDto = carDto;
            _carState = CarState.ForSale;
        }
        public CarViewModel()
        {
            _carDto = new CarDto();
        }


        #region Attributes
        private string _status = "";
        public string Status
        {
            get
            {
                try
                {
                    return _status;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }



        private CarState _actualCarState = CarState.Initial;
        public  CarState _carState
        {
            get { return _actualCarState; }
            set
            {
                _actualCarState = value;
                NotifyPropertyChanged("CreateUpdateButtonText");
            }
        }
        public string CreateUpdateButtonText
        {
            get
            {
                switch (_carState)
                {
                    case CarState.Initial:
                        return "Create";
                    default:
                        return "Update";
                }
            }
        }
        public int? CarId
        {
            get
            {
                try
                {
                    return _carDto.Id.Value;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            set
            {
                _carDto.Id = value;
                NotifyPropertyChanged("CarId");
            }
        }

        public string CarModel
        {
            get { return _carDto.Model; }
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

        public int Year
        {
            get
            {
                return _carDto.Year;
            }
            set
            {
                _carDto.Year = value;
                NotifyPropertyChanged("Year");
            }
        }
        public decimal Price
        {
            get
            {
                return _carDto.Price;
            }
            set
            {
                _carDto.Price = value;
                NotifyPropertyChanged("Price");
            }
        }
        public DateTime Created
        {
            get
            {
                return _carDto.Created;
            }
            set
            {
                _carDto.Created = value;
                NotifyPropertyChanged("Created");
            }
        }
        public bool Sold
        {
            get
            {
                return _carDto.Sold;
            }
            set
            {
                _carDto.Sold = value;
                NotifyPropertyChanged("Sold");
            }
        }
        public float Mileage
        {
            get
            {
                return _carDto.Mileage;
            }
            set
            {
                _carDto.Mileage = value;
                NotifyPropertyChanged("Mileage");
            }
        }
        public string Color
        {
            get
            {
                return _carDto.Color;
            }
            set
            {
                _carDto.Color = value;
                NotifyPropertyChanged("Color");
            }
        }
        public int DistanceDriven
        {
            get
            {
                return _carDto.DistanceDriven;
            }
            set
            {
                _carDto.DistanceDriven = value;
                NotifyPropertyChanged("DistanceDriven");
            }
        }
        public FuelType Fuel
        {
            get
            {
                return _carDto.Fuel;
            }
            set
            {
                _carDto.Fuel = value;
                NotifyPropertyChanged("Fuel");
            }
        }
        public string Drive
        {
            get
            {
                return _carDto.Drive;
            }
            set
            {
                _carDto.Drive = value;
                NotifyPropertyChanged("Drive");
            }
        }
        public string Transmission
        {
            get
            {
                return _carDto.Transmission;
            }
            set
            {
                _carDto.Transmission = value;
                NotifyPropertyChanged("Transmission");
            }
        }
        public int TopSpeed
        {
            get
            {
                return _carDto.TopSpeed;
            }
            set
            {
                _carDto.TopSpeed = value;
                NotifyPropertyChanged("TopSpeed");
            }
        }
        public int NoughtTo100
        {
            get
            {
                return _carDto.NoughtTo100;
            }
            set
            {
                _carDto.NoughtTo100 = value;
                NotifyPropertyChanged("NoughtTo100");
            }
        }
        #endregion Attributes

        #region CRUDS

        public void SaveCar()
        {
            switch (_carState)
            {
                case CarState.Initial:
                    CreateCar();
                    break;
                default: 
                    UpdateCar();
                    break;
            }            
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateCar()
        {
            var carController = new CarController();
            _carDto.Created = DateTime.Now;
            await carController.CreateCar(_carDto);
            Status = "Car Created";
            _carState = CarState.ForSale;
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateCar()
        {
            var carController = new CarController();
            carController.UpdateCar(_carDto);
            Status = "Car Updated";
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteCar()
        {
            var carController = new CarController();
            carController.DeleteCar(_carDto);
            _carDto.Id = null;
            Status = "Car Deleted";
            _carState = CarState.Initial;
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
