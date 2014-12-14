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
        /// <summary>
        /// The default constructor. Calling the UpdateList() method initially to load in data.
        /// </summary>
        public SaleListViewModel()
        {
            SaleViewModels = new ObservableCollection<SaleViewModel>();
            UpdateList();
        }
        /// <summary>
        /// Getters and setters for the attributes of a SaleDTO while notifying view.
        /// </summary>
        #region Properties

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

        private bool _canDeleteAndUpdate;

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

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
        /// <summary>
        /// Initializes the list of Sales and sets them to be editable.
        /// </summary>
        #region CRUDS
        public async void UpdateList()
        {
            try
            {
                Status = "";
                SaleViewModels.Clear();
                var saleController = new SaleController();
                foreach (SaleDto saleDto in await saleController.ReadSaleList())
                {
                    SaleViewModels.Add(new SaleViewModel(saleDto));
                }
                if (SaleViewModels.Count >= 1)
                {
                    SelectedSale = SaleViewModels[0];
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
                Status = "Failed to retrieve data for sales!";
            }
        }
        /// <summary>
        /// Deletes the selected Sale.
        /// </summary>
        public void DeleteSale()
        {
            try
            {
                if (SelectedSale.SaleId.HasValue) SelectedSale.DeleteSale();
                else
                {
                    SaleViewModels.Remove(SelectedSale);
                    if (SaleViewModels.Count >= 1)
                    {
                        SelectedSale = SaleViewModels[0];
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
            catch (Exception)
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
