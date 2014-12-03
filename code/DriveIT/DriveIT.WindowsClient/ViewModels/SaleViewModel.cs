using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class SaleViewModel : IViewModelBase
    {
        private SaleDto _saleDto;

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
        }
        public SaleViewModel()
        {
            
        }

        #region Attributes
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
        public int CustomerId
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
        public int EmployeeId
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
        /// Gets called from the view
        /// </summary>
        public void CreateSale()
        {
            var saleController = new SaleController();
            saleController.CreateSale(_saleDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateSale()
        {
            var saleController = new SaleController();
            saleController.UpdateSale(_saleDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteSale()
        {
            var saleController = new SaleController();
            saleController.DeleteSale(_saleDto);
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
