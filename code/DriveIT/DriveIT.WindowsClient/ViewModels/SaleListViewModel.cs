using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class SaleListViewModel : IViewModelBase
    {
    public ObservableCollection<SaleViewModel> SaleViewModels { get; set; }

        public SaleListViewModel(IList<SaleDto> saleDtos)
        {
            SaleViewModels = new ObservableCollection<SaleViewModel>();
            foreach (SaleDto saleDto in saleDtos)
            {
                SaleViewModels.Add(new SaleViewModel(saleDto));
            }
        }
        public SaleListViewModel()
        {
            SaleViewModels = new ObservableCollection<SaleViewModel>();
            ReadList();
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

        #region CRUD
        public async void ReadList()
        {
            var saleController = new SaleController();
            foreach (SaleDto saleDto in await saleController.ReadSaleList())
            {
                SaleViewModels.Add(new SaleViewModel(saleDto));
            }
        }
        #endregion CRUD
    }
}
