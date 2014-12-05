using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

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

        private SaleViewModel _selectedSale;
        public SaleViewModel SelectedSale
        {
            get { return _selectedSale; }

            set
            {
                _selectedSale = value;
                NotifyPropertyChanged("SelectedSale");
            }
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
        public async void UpdateList()
        {
            SaleViewModels.Clear();
            var saleController = new SaleController();
            foreach (SaleDto saleDto in await saleController.ReadSaleList())
            {
                SaleViewModels.Add(new SaleViewModel(saleDto));
            }
        }

        public void DeleteSale()
        {
            if (SelectedSale.SaleId.HasValue) SelectedSale.DeleteSale();
            else
            {
                SaleViewModels.Remove(SelectedSale);
                SelectedSale = null;
            }
        }

        public void CreateNewSaleWindow()
        {
            var newSale = new SaleViewModel();
            var window = new EntitySaleWindow { DataContext = newSale };
            SaleViewModels.Add(newSale);
            window.Show();
        }

        public void UpdateSaleWindow()
        {
            SaleViewModel sale = SelectedSale;
            var window = new EntitySaleWindow { DataContext = sale };
            window.Show();
        }
        #endregion CRUD
    }
}
