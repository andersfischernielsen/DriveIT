using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    /// <summary>
    /// This class is a ViewModel, following the MVVM pattern. Furthermore it also functions as a "Adaptor Pattern"
    /// </summary>
    public class SaleViewModel : IViewModelBase
    {
        private SaleDto _saleDto;
        /// <summary>
        /// Enum for determining whether the Sale is in the system or not.
        /// </summary>
        public enum SaleEnum
        {
            NotInSystem,
            InSystem
        }
        /// <summary>
        /// Gets the nullable int value of SaleId, sets it, and notifies the view that the property is changed.
        /// </summary>
        public int? SaleId
        {
            get
            {
                try
                {
                    return _saleDto.Id;
                }
                catch (Exception)
                {

                    return null;
                }
                
            }
            set
            {
                _saleDto.Id = value;
                NotifyPropertyChanged("SaleId");
            }
        }
        /// <summary>
        /// Constructor for SaleViewModel which updates the given SaleDTO and updates the SaleState.
        /// </summary>
        /// <param name="saleDto">The SaleDTO to be updated</param>
        public SaleViewModel(SaleDto saleDto)
        {
            _saleDto = saleDto;
            SaleState = SaleEnum.InSystem;
            UpdateForeignKeyLists();
        }
        /// <summary>
        /// Empty constructor which creates a new Sale DTO.
        /// </summary>
        public SaleViewModel()
        {
            _saleDto = new SaleDto();
            SaleState = SaleEnum.NotInSystem;
            Sold = DateTime.Now;
            UpdateForeignKeyLists();
        }
        /// <summary>
        /// Updates the list of foreign keys.
        /// </summary>
        public async void UpdateForeignKeyLists()
        {
            try
            {
                CustomerIdsList = (await new CustomerController().ReadCustomerList()).Select(i => i.Email).ToList();
                EmployeeIdsList = (await new EmployeeController().ReadEmployeeList()).Select(i => i.Email).ToList();
            }
            catch (Exception)
            {
                CustomerIdsList = new List<string>();
                EmployeeIdsList = new List<string>();
            }
        }
        /// <summary>
        /// Getters and setters for the attributes of a SaleDTO.
        /// </summary>
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

        private SaleEnum _actualSaleState;
        public SaleEnum SaleState
        {
            get { return _actualSaleState; }
            set
            {
                _actualSaleState = value;
                NotifyPropertyChanged("CreateUpdateButtonText");
            }
        }
        public string CreateUpdateButtonText
        {
            get
            {
                switch (SaleState)
                {
                    case SaleEnum.NotInSystem:
                        return "Create";
                    default:
                        return "Update";
                }
            }
        }
        public decimal? Price
        {
            get
            {
                if (_saleDto.Price == 0) return null;
                return _saleDto.Price;
            }
            set
            {
                _saleDto.Price = value.GetValueOrDefault();
                NotifyPropertyChanged("Price");
            }
        }
        public DateTime Sold
        {
            get
            {
                return _saleDto.Sold;
            }
            set
            {
                _saleDto.Sold = value;
                NotifyPropertyChanged("Sold");
            }
        }
        public int? CarId
        {
            get
            {
                if (_saleDto.CarId == 0) return null;
                return _saleDto.CarId;
            }
            set
            {
                _saleDto.CarId = value.GetValueOrDefault();
                NotifyPropertyChanged("CarId");
            }
        }
        public static List<string> CustomerIdsList { get; set; }
        public string CustomerId
        {
            get
            {
                return _saleDto.CustomerId;
            }
            set
            {
                _saleDto.CustomerId = value;
                NotifyPropertyChanged("CustomerId");
            }
        }
        public static List<string> EmployeeIdsList { get; set; }
        public string EmployeeId
        {
            get
            {
                return _saleDto.EmployeeId;
            }
            set
            {
                _saleDto.EmployeeId = value;
                NotifyPropertyChanged("EmployeeId");
            }
        }
        #endregion Attributes

        #region CRUDS
        /// <summary>
        /// Saves a Sale - updates the Sale if it exists, otherwise creates a new Sale.
        /// </summary>
        public void SaveSale()
        {
            try
            {
                switch (SaleState)
                {
                    case SaleEnum.NotInSystem:
                        CreateSale();
                        break;
                    default:
                        UpdateSale();
                        break;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to save sale!";
            }
        }
        /// <summary>
        /// Creates a new SaleController, creates a Sale from the API, and notifies the view.
        /// </summary>
        public async void CreateSale()
        {
            try
            {
                var saleController = new SaleController();
                var saleInDB = await saleController.CreateSale(_saleDto);

                _saleDto = saleInDB;
                NotifyPropertyChanged("");

                Status = "Sale Created";
                SaleState = SaleEnum.InSystem;
            }
            catch (Exception e)
            {
                Status = "Failed to create sale!";
            }

        }
        /// <summary>
        /// Updates a SaleController, creates a Sale from the API, and notifies the view.
        /// </summary>
        public async void UpdateSale()
        {
            try
            {
                var saleController = new SaleController();
                await saleController.UpdateSale(_saleDto);
                Status = "Sale Updated";
            }
            catch (Exception e)
            {
                Status = "Failed to update sales!";
            }
        }
        /// <summary>
        /// Deletes a SaleController, creates a Sale from the API, and notifies the view.
        /// </summary>
        public async void DeleteSale()
        {
            try
            {
                var saleController = new SaleController();
                if (SaleState != SaleEnum.NotInSystem)
                {
                    await saleController.DeleteSale(_saleDto);
                    SaleId = null;
                    Status = "Sale Deleted";
                    SaleState = SaleEnum.NotInSystem;
                }
            }
            catch (Exception)
            {

                Status = "Failed to delete sale!";
            }
            
        }
        #endregion CRUDS
        /// <summary>
        /// A method for notifying the view if any properties have been changed.
        /// </summary>
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
