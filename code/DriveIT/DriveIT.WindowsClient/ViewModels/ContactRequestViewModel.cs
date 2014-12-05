using System;
using System.ComponentModel;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class ContactRequestViewModel : IViewModelBase
    {
        private ContactRequestDto _contactRequestDto;

        public int? ContactRequestId
        {
            get
            {
                try
                {
                    return _contactRequestDto.Id.Value;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            set
            {
                _contactRequestDto.Id = value;
                NotifyPropertyChanged("ContactRequestId");
            }
        }

        public ContactRequestViewModel(ContactRequestDto contactRequestDto)
        {
            _contactRequestDto = contactRequestDto;
        }
        public ContactRequestViewModel()
        {
        }

        
        #region ATTRIBUTES
        public int RequestId
        {
            get
            {
                return _contactRequestDto.Id ?? 0;
            }
            set
            {
                _contactRequestDto.Id = value;
                NotifyPropertyChanged("RequestId");
            }
        }
        public DateTime Requested
        {
            get
            {
                return _contactRequestDto.Requested;
            }
            set
            {
                _contactRequestDto.Requested = value;
                NotifyPropertyChanged("Requested");
            }
        }
        public int CustomerId
        {
            get
            {
                return _contactRequestDto.CustomerId;
            }
            set
            {
                _contactRequestDto.CustomerId = value;
                NotifyPropertyChanged("CustomerId");
            }
        }
        public int CarId
        {
            get
            {
                return _contactRequestDto.CarId;
            }
            set
            {
                _contactRequestDto.CarId = value;
                NotifyPropertyChanged("CarId");
            }
        }
        public int? EmployeeId
        {
            get
            {
                return _contactRequestDto.EmployeeId;
            }
            set
            {
                _contactRequestDto.EmployeeId = value;
                NotifyPropertyChanged("EmployeeId");
            }
        }
        #endregion ATTRIBUTES

        #region CRUDS
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void CreateContactRequest()
        {
            var contactRequestController = new ContactRequestController();
            contactRequestController.CreateContactRequest(_contactRequestDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void UpdateContactRequest()
        {
            var contactRequestController = new ContactRequestController();
            contactRequestController.UpdateContactRequest(_contactRequestDto);
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public void DeleteContactRequest()
        {
            var contactRequestController = new ContactRequestController();
            contactRequestController.DeleteContactRequest(_contactRequestDto);
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
