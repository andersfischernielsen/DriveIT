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

        private string _status = "";

        public string Status
        {
            get { return _status; } 
            set { 
                    _status = value; 
                    NotifyPropertyChanged("Status"); 
                }
        }

        #region CRUDS

        public async void ReadList()
        {
            try
            {
                var contactRequestController = new ContactRequestController();
                foreach (ContactRequestDto contactRequestDto in await contactRequestController.ReadContactRequests())
                {
                    ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public async void UpdateList()
        {
            try
            {
                ContactRequestViewModels.Clear();
                var contactRequestController = new ContactRequestController();
                foreach (ContactRequestDto contactRequestDto in await contactRequestController.ReadContactRequests())
                {
                    ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void DeleteContactRequest()
        {
            try
            {
                if (SelectedRequest.ContactRequestId.HasValue) SelectedRequest.DeleteContactRequest();
                else
                {
                    ContactRequestViewModels.Remove(SelectedRequest);
                    SelectedRequest = null;
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void CreateNewContactRequestWindow()
        {
            try
            {
                var newContactRequest = new ContactRequestViewModel();
                var window = new EntityContactRequestWindow { DataContext = newContactRequest };
                ContactRequestViewModels.Add(newContactRequest);
                window.Show();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public void UpdateContactRequestWindow()
        {
            try
            {
                ContactRequestViewModel contactRequest = SelectedRequest;
                var window = new EntityContactRequestWindow { DataContext = contactRequest };
                window.Show();
            }
            catch (Exception e)
            {
                
                throw;
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
