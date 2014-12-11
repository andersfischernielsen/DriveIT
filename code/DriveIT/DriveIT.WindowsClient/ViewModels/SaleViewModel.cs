using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class SaleViewModel : IViewModelBase
    {
        private SaleDto _saleDto;
        public enum SaleEnum
        {
            NotInSystem,
            InSystem
        }

        public int? SaleId
        {
            get
            {
                try
                {
                    return _saleDto.Id.Value;
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


        public SaleViewModel(SaleDto saleDto)
        {
            _saleDto = saleDto;
            SaleState = SaleEnum.InSystem;
            UpdateForeignKeyLists();
        }
        public SaleViewModel()
        {
            _saleDto = new SaleDto();
            SaleState = SaleEnum.NotInSystem;
            UpdateForeignKeyLists();
        }

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
        public decimal Price
        {
            get
            {
                return _saleDto.Price;
            }
            set
            {
                _saleDto.Price = value;
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
        public int CarId
        {
            get
            {
                return _saleDto.CarId;
            }
            set
            {
                _saleDto.CarId = value;
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
        public void SaveSale()
        {
            try
            {
                switch (SaleState)
                {
                    case SaleEnum.NotInSystem:
                        CreateSale();
                        Status = "Sale Saved";
                        break;
                    default:
                        UpdateSale();
                        Status = "Sale Saved";
                        break;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to save sale!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateSale()
        {
            var saleController = new SaleController();
            try
            {
                await saleController.CreateSale(_saleDto);
                Status = "Sale Created";
                SaleState = SaleEnum.InSystem;
            }
            catch (Exception e)
            {
                Status = "Failed to create sale!";
            }

        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateSale()
        {
            var saleController = new SaleController();
            try
            {
                await saleController.UpdateSale(_saleDto);
                Status = "Sale Updated";
            }
            catch (Exception e)
            {
                Status = "Failed to update sales!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteSale()
        {
            var saleController = new SaleController();
            try
            {
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
