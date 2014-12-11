using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

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

        public ContactRequestListViewModel(IEnumerable<ContactRequestDto> contactRequestDtos)
        {
            ContactRequestViewModels = new ObservableCollection<ContactRequestViewModel>(
                contactRequestDtos
                .Select(contactRequest => new ContactRequestViewModel(contactRequest)));
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

        public async void UpdateList()
        {
            ContactRequestViewModels.Clear();
            var contactRequestController = new ContactRequestController();
            foreach (ContactRequestDto contactRequestDto in await contactRequestController.ReadContactRequests())
            {
                ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
            }
        }

        public void DeleteContactRequest()
        {
            if (SelectedRequest.ContactRequestId.HasValue) SelectedRequest.DeleteContactRequest();
            else
            {
                ContactRequestViewModels.Remove(SelectedRequest);
                SelectedRequest = null;
            }
        }

        public void CreateNewContactRequestWindow()
        {
            var newContactRequest = new ContactRequestViewModel();
            var window = new EntityContactRequestWindow {DataContext = newContactRequest};
            ContactRequestViewModels.Add(newContactRequest);
            window.Show();
        }

        public void UpdateContactRequestWindow()
        {
            ContactRequestViewModel contactRequest = SelectedRequest;
            var window = new EntityContactRequestWindow {DataContext = contactRequest};
            window.Show();
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
