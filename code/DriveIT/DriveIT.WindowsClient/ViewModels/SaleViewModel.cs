using System;
using System.ComponentModel;
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
        }
        public SaleViewModel()
        {
            _saleDto = new SaleDto();
            SaleState = SaleEnum.NotInSystem;
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
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateSale()
        {
            var saleController = new SaleController();
            await saleController.CreateSale(_saleDto);
            Status = "Sale Created";
            SaleState = SaleEnum.InSystem;
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateSale()
        {
            var saleController = new SaleController();
            await saleController.UpdateSale(_saleDto);
            Status = "Sale Updated";
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteSale()
        {
            if (SaleState != SaleEnum.NotInSystem)
            {
                var saleController = new SaleController();
                await saleController.DeleteSale(_saleDto);
                SaleId = null;
                Status = "Sale Deleted";
                SaleState = SaleEnum.NotInSystem;
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
