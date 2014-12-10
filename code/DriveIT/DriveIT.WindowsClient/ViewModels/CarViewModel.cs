using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class CarViewModel : IViewModelBase
    {
        private CarDto _carDto;
        public enum CarStateEnum
        {
            Initial,
            ForSale,
            Advertised,
            Sold,
        }

        public static List<string> FueltypeStrings
        {
            get
            {
                // todo get from enum instead. VERY BAD CODE
                return new List<string>()
                {
                    FuelType.Gasoline.ToString(),
                    FuelType.Diesel.ToString(),
                    FuelType.Electric.ToString(),
                };
            }
            set { }
        }

        // todo ; til at notifie at alt er updated.

        public CarViewModel(CarDto carDto)
        {
            _carDto = carDto;
            CarState = CarStateEnum.ForSale;
            CreateImageViewModels();

        }
        public CarViewModel()
        {
            _carDto = new CarDto();
            Created = DateTime.Now;
            CreateImageViewModels();

            CarState = CarStateEnum.Initial;
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

        private CarStateEnum _actualCarState;
        public  CarStateEnum CarState
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
                switch (CarState)
                {
                    case CarStateEnum.Initial:
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
                return _carDto.Year ?? 0;
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
        public float TopSpeed
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
        public float NoughtTo100
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

        #region ImageGallery
        public List<ImageViewModel> ImageGallery { get; set; }
        private ImageViewModel _selectedImageViewModel;
        public ImageViewModel SelectedImageViewModel
        {
            get { return _selectedImageViewModel; }
            set
            {
                _selectedImageViewModel = value;
                NotifyPropertyChanged("SelectedImageViewModel");
            }
        }

        private void CreateImageViewModels()
        {
            ImageGallery = new List<ImageViewModel>();
            if (_carDto.ImagePaths != null)
            {
                foreach (var imagePath in _carDto.ImagePaths)
                {
                    ImageGallery.Add(new ImageViewModel(imagePath));
                }
            }
            else
            {
                ImageGallery.Add(new ImageViewModel());
            }
            SelectedImageViewModel = ImageGallery[0];
            ImageAmtString = "Image 1 of " + ImageGallery.Count;
        }

        private string _imageAmtString;
        public string ImageAmtString
        {
            get { return _imageAmtString; }
            set
            {
                _imageAmtString = value;
                NotifyPropertyChanged("ImageAmtString");
            }
        }

        public void NextImage()
        {
            int currentIndex = ImageGallery.IndexOf(SelectedImageViewModel);
            int nextIndex = currentIndex+1;
            if (nextIndex == ImageGallery.Count)
            {
                nextIndex = 0;
            }
            SelectedImageViewModel = ImageGallery[nextIndex];
            ImageAmtString = "Image " + (ImageGallery.IndexOf(SelectedImageViewModel) + 1) + " of " + ImageGallery.Count;
        }
        public void PreviousImage()
        {
            int currentIndex = ImageGallery.IndexOf(SelectedImageViewModel);
            int nextIndex = currentIndex-1;
            if (nextIndex <= -1)
            {
                nextIndex = ImageGallery.Count-1;
            }
            SelectedImageViewModel = ImageGallery[nextIndex];
            ImageAmtString = "Image " + (ImageGallery.IndexOf(SelectedImageViewModel)+1) + " of " + ImageGallery.Count;
        }
        public void DeleteImage()
        {
            if (ImageGallery.Count == 1)
            {
                ImageGallery[0] = new ImageViewModel();
                SelectedImageViewModel = ImageGallery[0];
            }
            else
            {
                ImageGallery.Remove(SelectedImageViewModel);
                PreviousImage();
            }
        }
        public void AddImage()
        {
            if (!SelectedImageViewModel.IsEmpty())
            {
                ImageGallery.Add(new ImageViewModel());
                SelectedImageViewModel = ImageGallery[ImageGallery.Count - 1];
                ImageAmtString = "Image " + ImageGallery.Count + " of " + ImageGallery.Count;
            }
        }

        #endregion ImageGallery

        public async void ImportCarQueryData()
        {
            Status = "Importing...";
            try
            {
                _carDto = await CarQuery.CarQuery.FillCarData(_carDto);
                NotifyPropertyChanged(String.Empty);
                Status = "CarQuery Data imported";
            }
            catch (Exception)
            {
                Status = "Failed to import from CarQuery";
            }
            
        }

        #region CRUDS

        public void SaveCar()
        {
            CreateImagePathStrings();
            switch (CarState)
            {
                case CarStateEnum.Initial:
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
            UploadImages();
            var carController = new CarController();
            await carController.CreateCar(_carDto);
            Status = "Car Created";
            CarState = CarStateEnum.ForSale;
        }

        private async Task UploadImages()
        {
            _carDto.ImagePaths = await ImageController.UploadImages(_carDto);
            CreateImageViewModels();
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateCar()
        {
            await UploadImages();
            var carController = new CarController();
            await carController.UpdateCar(_carDto);
            Status = "Car Updated";
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteCar()
        {
            if (CarState != CarStateEnum.Initial)
            {
                var carController = new CarController();
                await carController.DeleteCar(_carDto);
                CarId = null;
                Status = "Car Deleted";
                CarState = CarStateEnum.Initial;
            }
        }

        public void CreateImagePathStrings()
        {
            _carDto.ImagePaths = ImageGallery.Select(i => i.ImagePath).ToList();
            for (int i = 0; i < _carDto.ImagePaths.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(_carDto.ImagePaths[i])) _carDto.ImagePaths.RemoveAt(i);
            }
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
