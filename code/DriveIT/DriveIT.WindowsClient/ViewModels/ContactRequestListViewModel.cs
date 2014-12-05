using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class ContactRequestListViewModel : IViewModelBase
    {
        public ObservableCollection<ContactRequestViewModel> ContactRequestViewModels { get; set; }
        private ContactRequestViewModel _selectedRequest;
        public ContactRequestViewModel SelectedRequest
        {
            get { return _selectedRequest; }

            set
            {
                _selectedRequest = value;
                NotifyPropertyChanged("SelectedRequest");
            }
        }

        public ContactRequestListViewModel(IList<ContactRequestDto> contactRequestDtos)
        {
            ContactRequestViewModels = new ObservableCollection<ContactRequestViewModel>();
            foreach (ContactRequestDto contactRequestDto in contactRequestDtos)
            {
                ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
            }
        }
        public ContactRequestListViewModel()
        {
            ContactRequestViewModels = new ObservableCollection<ContactRequestViewModel>();
            ReadList();
        }

        #region CRUD

        public async void ReadList()
        {
            var contactRequestController = new ContactRequestController();
            foreach (ContactRequestDto contactRequestDto in await contactRequestController.ReadContactRequests())
            {
                ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
            }
        }

        #endregion CRUD

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
