using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    /// <summary>
    /// The Viewmodel for the CarEntityView following the MVVM design pattern by Microsoft.
    /// This Viewmodel also works as an adapter design pattern for the CarDto object. This allows easy integration with the API which sends and recieves carDtos
    /// </summary>
    public class CarViewModel : IViewModelBase
    {
        private CarDto _carDto;

        /// <summary>
        /// A state enum, which is used to decide which methods are available and control the flow.
        /// </summary>
        public enum CarStateEnum
        {
            Initial,
            ForSale,
            Advertised,
            Sold,
        }

        /// <summary>
        /// An array of the fueltypes used to create the items in a combobox
        /// </summary>
        public static FuelType[] FueltypeStrings
        {
            get
            {
                return (FuelType[]) Enum.GetValues(typeof(FuelType));
            }
        }

        /// <summary>
        /// The constructor used for when a carDto already exists. Thsi constructor assumes the object exists in the database.
        /// Also intialise the Imagegallery.
        /// </summary>
        /// <param name="carDto">A Car</param>
        public CarViewModel(CarDto carDto)
        {
            _carDto = carDto;
            CarState = CarStateEnum.ForSale;
            CreateImageViewModels();

        }
        /// <summary>
        /// The empty constructor for when a cardto does not exist.
        /// Also intialise the Imagegallery.
        /// </summary>
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
                    return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private CarStateEnum _actualCarState;
        public CarStateEnum CarState
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
                return _carDto.Id;
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
        public int? Year
        {
            get { return _carDto.Year; }
            set
            {
                _carDto.Year = value;
                NotifyPropertyChanged("Year");
            }
        }
        public decimal? Price
        {
            get
            {
                if (_carDto.Price == 0) return null;
                return _carDto.Price;
            }
            set
            {
                _carDto.Price = value.GetValueOrDefault();
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
        public float? Mileage
        {
            get
            {
                if (Math.Abs(_carDto.Mileage) < 1) return null;
                return _carDto.Mileage;
            }
            set
            {
                _carDto.Mileage = value.GetValueOrDefault();
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
        public int? DistanceDriven
        {
            get
            {
                if (_carDto.DistanceDriven == 0) return null;
                return _carDto.DistanceDriven;
            }
            set
            {
                _carDto.DistanceDriven = value.GetValueOrDefault();
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
        public float? TopSpeed
        {
            get
            {
                if (Math.Abs(_carDto.TopSpeed) < 1) return null;
                return _carDto.TopSpeed;
            }
            set
            {
                _carDto.TopSpeed = value.GetValueOrDefault();
                NotifyPropertyChanged("TopSpeed");
            }
        }
        public float? NoughtTo100
        {
            get
            {
                if (Math.Abs(_carDto.NoughtTo100) < 1) return null;
                return _carDto.NoughtTo100;
            }
            set
            {
                _carDto.NoughtTo100 = value.GetValueOrDefault();
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
        /// <summary>
        /// Creates ImageViewModels based on the ImagePaths in _carDto. If none exists add a single ImageViewModel
        /// </summary>
        private void CreateImageViewModels()
        {
            ImageGallery = new List<ImageViewModel>();
            if (_carDto.ImagePaths != null && _carDto.ImagePaths.Any())
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
        /// <summary>
        /// The string describing how many images there are and which one is currently showing.
        /// E.g "Image 1 of 3".
        /// </summary>
        public string ImageAmtString
        {
            get { return _imageAmtString; }
            set
            {
                _imageAmtString = value;
                NotifyPropertyChanged("ImageAmtString");
            }
        }

        /// <summary>
        /// Sets the selectedImage to the next in the image gallery. Works like a caroussel so when using Next at the last image, the first image becomes the Selected Image.
        /// </summary>
        public void NextImage()
        {
            int currentIndex = ImageGallery.IndexOf(SelectedImageViewModel);
            int nextIndex = currentIndex + 1;
            if (nextIndex == ImageGallery.Count)
            {
                nextIndex = 0;
            }
            SelectedImageViewModel = ImageGallery[nextIndex];
            ImageAmtString = "Image " + (ImageGallery.IndexOf(SelectedImageViewModel) + 1) + " of " + ImageGallery.Count;
        }
        /// <summary>
        /// Sets the selectedImage to the previous in the image gallery. Works like a caroussel so when using Previous at the first image, the last image becomes the Selected Image.
        /// </summary>
        public void PreviousImage()
        {
            int currentIndex = ImageGallery.IndexOf(SelectedImageViewModel);
            int nextIndex = currentIndex - 1;
            if (nextIndex <= -1)
            {
                nextIndex = ImageGallery.Count - 1;
            }
            SelectedImageViewModel = ImageGallery[nextIndex];
            ImageAmtString = "Image " + (ImageGallery.IndexOf(SelectedImageViewModel) + 1) + " of " + ImageGallery.Count;
        }
        /// <summary>
        /// Removes the selectedImageviewmodel and goes to the previous picture, unless only 1 image exists in the imageGallery, in that case, the imageviewmodel is just reset.
        /// </summary>
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
        /// <summary>
        /// Adds a new Imageviewmodel to the ImageGallery unless the current imageviewmodel is empty.
        /// </summary>
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

        /// <summary>
        /// Based on the information in the _carDto, the carQuery controller inputs new information to the carDto.
        /// Information already filled into the _carDto will not be overriden.
        /// </summary>
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
        /// <summary>
        /// Based on the carstate either Create the var or Update it. If neither CarModel, CarMake nor Price is set the status is updated.
        /// </summary>
        public async void SaveCar()
        {
            if (string.IsNullOrWhiteSpace(CarModel) || string.IsNullOrWhiteSpace(CarMake) || Price == 0)
            {
                Status = "Model, Make and Price must be set.";
                return;
            }
            try
            {
                CreateImagePathStrings();
                switch (CarState)
                {
                    case CarStateEnum.Initial:
                        await CreateCar();
                        break;
                    default:
                        await UpdateCar();
                        break;
                }
            }
            catch (Exception)
            {
                Status = "Failed to save car!";
            }
        }
        /// <summary>
        /// Creates the given car and update the carDto with the created cardto in the database(to find the ID).
        /// Changes the carstate to CarStateEnum.ForSale
        /// </summary>
        public async Task CreateCar()
        {
            try
            {
                Status = "Trying to create car...";
                var carController = new CarController();
                _carDto = await carController.CreateCar(_carDto);

                await UploadImages();
                await carController.UpdateCar(_carDto);

                NotifyPropertyChanged("");

                Status = "Car Created";
                CarState = CarStateEnum.ForSale;
            }
            catch (Exception)
            {
                
                Status = "Failed to create car!";
            }
        }

        /// <summary>
        /// Updates the images and information about the _carDto
        /// </summary>
        public async Task UpdateCar()
        {
            try
            {
                Status = "Trying to update car...";
                await UploadImages();
                var carController = new CarController();
                await carController.UpdateCar(_carDto);
                Status = "Car Updated";
            }
            catch (Exception)
            {

                Status = "Failed to update car!";
            }
        }

        /// <summary>
        /// Uploads the images at the imagespath of the _carDto.
        /// </summary>
        /// <returns></returns>
        private async Task UploadImages()
        {
            try
            {
                var newPaths = new List<string>();
                foreach (var imagePath in _carDto.ImagePaths)
                {
                    var uri = new Uri(imagePath);
                    if (uri.IsFile)
                    {
                        newPaths.Add(await ImageController.UploadImage(_carDto.Id.GetValueOrDefault(), imagePath));
                    }
                    else
                    {
                        newPaths.Add(imagePath);
                    }
                }
                _carDto.ImagePaths = newPaths;
                CreateImageViewModels();
            }
            catch (Exception)
            {
                Status = "Failed to upload image!";
            }
        }
        
        /// <summary>
        /// Gets called from the view. Deletes the car at the webApi, with the id _carDto.Id
        /// </summary>
        public async void DeleteCar()
        {
            try
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
            catch (Exception)
            {
                
                Status = "Failed to delete car!";
            }
        }

        /// <summary>
        /// Takes the imagepaths(strings) in the imageviewmodels of imageGallery and moves them into _cardto's ImagePaths if they are not null or empty.
        /// </summary>
        public void CreateImagePathStrings()
        {
            try
            {
                _carDto.ImagePaths = ImageGallery
                .Where(i => !string.IsNullOrWhiteSpace(i.ImagePath))
                .Select(i => i.ImagePath)
                .ToList();
            }
            catch (Exception)
            {
                Status = "Failed to create imagepaths!";
                throw;
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
