using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class SaleListViewModel : IViewModelBase
    {
    public ObservableCollection<SaleViewModel> SaleViewModels { get; set; }

        public SaleListViewModel(IEnumerable<SaleDto> saleDtos)
        {
            SaleViewModels = new ObservableCollection<SaleViewModel>(
                saleDtos.Select(saleDto => new SaleViewModel(saleDto)));
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

        #region CRUDS
        public async void ReadList()
        {
            try
            {
                var saleController = new SaleController();
                foreach (SaleDto saleDto in await saleController.ReadSaleList())
                {
                    SaleViewModels.Add(new SaleViewModel(saleDto));
                }
                Status = "";
            }
            catch (Exception e)
            {

                Status = "Failed to retrieve data for sales!";
            }
        }
        public async void UpdateList()
        {
            try
            {
                SaleViewModels.Clear();
                var saleController = new SaleController();
                foreach (SaleDto saleDto in await saleController.ReadSaleList())
                {
                    SaleViewModels.Add(new SaleViewModel(saleDto));
                }
                Status = "";
            }
            catch (Exception e)
            {

                Status = "Failed to update the list!";
            }
        }

        public void DeleteSale()
        {
            try
            {
                if (SelectedSale.SaleId.HasValue) SelectedSale.DeleteSale();
                else
                {
                    SaleViewModels.Remove(SelectedSale);
                    SelectedSale = null;
                }
                Status = "";
            }
            catch (Exception e)
            {

                Status = "Failed to delete the sale!";
            }
        }

        public void CreateNewSaleWindow()
        {
            try
            {
                var newSale = new SaleViewModel();
                var window = new EntitySaleWindow { DataContext = newSale };
                SaleViewModels.Add(newSale);
                window.Show();
                Status = "";
            }
            catch (Exception e)
            {

                Status = "Failed to create window!";
            }
        }

        public void UpdateSaleWindow()
        {
            try
            {
                SaleViewModel sale = SelectedSale;
                var window = new EntitySaleWindow { DataContext = sale };
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
