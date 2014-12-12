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

        /// <summary>
        /// The default constructor. Calling the UpdateList() method initially to load in data.
        /// </summary>
        public CarListViewModel()
        {
            CarViewModels = new ObservableCollection<CarViewModel>();
            UpdateList();
        }

        #region Properties
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
        
        private bool _canDeleteAndUpdate;
        /// <summary>
        /// Binds to the isEnabled property of the "See info" and "Delete" buttons in the main view. Notifies the view when changes happen
        /// </summary>
        public bool CanDeleteAndUpdate
        {
            get { return _canDeleteAndUpdate; }
            set
            {
                _canDeleteAndUpdate = value;
                NotifyPropertyChanged("CanDeleteAndUpdate");
            }
        }

        #endregion Properties

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A method for notifying the view if any properties have been changed.
        /// <param name="info">The name of the property which has changed</param>
        /// </summary>
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
                CarViewModels.Clear();
                var carController = new CarController();
                foreach (CarDto carDto in await carController.ReadCarList())
                {
                    CarViewModels.Add(new CarViewModel(carDto));
                }
                if (CarViewModels.Count >= 1)
                {
                    SelectedCar = CarViewModels[0];
                    CanDeleteAndUpdate = true;
                }
                else
                {
                    CanDeleteAndUpdate = false;
                }
            }
            catch (Exception)
            {
                CanDeleteAndUpdate = false;
                Status = "Failed to retrieve data for cars!";
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
                    if (CarViewModels.Count >= 1)
                    {
                        SelectedCar = CarViewModels[0];
                        CanDeleteAndUpdate = true;
                    }
                    else
                    {
                        CanDeleteAndUpdate = false;
                    }
                }
                Status = "";
            }
            catch (Exception)
            {
                
                Status = "Failed to delete the car!";
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
                Status = "";
            }
            catch (Exception)
            {
                
                Status = "Failed to create window!";
            }
        }

        public void UpdateCarWindow()
        {
            try
            {
                CarViewModel car = SelectedCar;
                var window = new EntityCarWindow { DataContext = car };
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
