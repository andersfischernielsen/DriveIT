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
        /// <summary>
        /// Getters and setters for the attributes of a ContactRequestDTO while notifying view.
        /// </summary>
        #region Properties
        public ContactRequestListViewModel()
        {
            ContactRequestViewModels = new ObservableCollection<ContactRequestViewModel>();
            UpdateList();
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

        #region CRUDS
        /// <summary>
        /// Initializes the list of ContactRequests and sets them to be editable.
        /// </summary>
        public async void UpdateList()
        {
            try
            {
                Status = "";
                ContactRequestViewModels.Clear();
                var contactRequestController = new ContactRequestController();
                foreach (ContactRequestDto contactRequestDto in await contactRequestController.ReadContactRequests())
                {
                    ContactRequestViewModels.Add(new ContactRequestViewModel(contactRequestDto));
                }
                if (ContactRequestViewModels.Count >= 1)
                {
                    SelectedRequest = ContactRequestViewModels[0];
                    CanDeleteAndUpdate = true;
                }
                else
                {
                    CanDeleteAndUpdate = false;
                }
            }
            catch (Exception)
            {
                Status = "Failed to retrieve contact requests!";
                CanDeleteAndUpdate = false;
            }
        }
        /// <summary>
        /// Deletes the selected ContactRequest.
        /// </summary>
        public void DeleteContactRequest()
        {
            try
            {
                if (SelectedRequest.ContactRequestId.HasValue) SelectedRequest.DeleteContactRequest();
                else
                {
                    ContactRequestViewModels.Remove(SelectedRequest);
                    if (ContactRequestViewModels.Count >= 1)
                    {
                        SelectedRequest = ContactRequestViewModels[0];
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
                Status = "Failed to delete the contact request!";
            }
        }
        /// <summary>
        /// Creates a new ContactRequest window to create ContactRequests.
        /// </summary>
        public void CreateNewContactRequestWindow()
        {
            try
            {
                var newContactRequest = new ContactRequestViewModel();
                var window = new EntityContactRequestWindow { DataContext = newContactRequest };
                ContactRequestViewModels.Add(newContactRequest);
                window.Show();
                Status = "";
            }
            catch (Exception)
            {
                Status = "Failed to create window!";
            }
        }
        /// <summary>
        /// Opens the ContactRequestWindow with the information of the selected ContactRequest.
        /// </summary>
        public void UpdateContactRequestWindow()
        {
            try
            {
                ContactRequestViewModel contactRequest = SelectedRequest;
                var window = new EntityContactRequestWindow { DataContext = contactRequest };
                window.Show();
                Status = "";
            }
            catch (Exception)
            {

                Status = "Failed to update window!";
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
