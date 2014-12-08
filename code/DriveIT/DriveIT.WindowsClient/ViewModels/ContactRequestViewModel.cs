using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class ContactRequestViewModel : IViewModelBase
    {
        private ContactRequestDto _contactRequestDto;

        public enum ContactRequestEnum
        {
            NotInSystem,
            InSystem
        }

        public IList<string> CarToChooseFrom { get; set; }

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
            ContactRequestState = ContactRequestEnum.InSystem;
        }
        public ContactRequestViewModel()
        {
            _contactRequestDto = new ContactRequestDto();
            //Requested = DateTime.Now;
            ContactRequestState = ContactRequestEnum.NotInSystem;
        }

        private void InitiateForeignLists()
        {
            CarToChooseFrom = new CarController().ReadCarList().Result.OrderBy(i => i.Make).ToList().Select(i => "ID: " + i.Id + "Make: " + i.Make).ToList();
        }

        
        #region ATTRIBUTES
        private string _status = "";
        public string Status
        {
            get
            {
                try
                {
                    return _status;
                }
                catch (Exception)
                {

                    return null;
                }
            }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private ContactRequestEnum _actualContactRequestState;
        public ContactRequestEnum ContactRequestState
        {
            get { return _actualContactRequestState; }
            set
            {
                _actualContactRequestState = value;
                NotifyPropertyChanged("CreateUpdateButtonText");
            }
        }
        public string CreateUpdateButtonText
        {
            get
            {
                switch (ContactRequestState)
                {
                    case ContactRequestEnum.NotInSystem:
                        return "Create";
                    default:
                        return "Update";
                }
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
        public string CustomerId
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
        public string EmployeeId
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
        public void SaveContactRequest()
        {
            switch (ContactRequestState)
            {
                case ContactRequestEnum.NotInSystem:
                    CreateContactRequest();
                    break;
                default:
                    UpdateContactRequest();
                    break;
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateContactRequest()
        {
            var contactRequestController = new ContactRequestController();
            await contactRequestController.CreateContactRequest(_contactRequestDto);
            Status = "Contact Request Created";
            ContactRequestState = ContactRequestEnum.InSystem;
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateContactRequest()
        {
            var contactRequestController = new ContactRequestController();
            await contactRequestController.UpdateContactRequest(_contactRequestDto);
            Status = "Contact Request Updated";
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteContactRequest()
        {
            if (ContactRequestState != ContactRequestEnum.NotInSystem)
            {
                var contactRequestController = new ContactRequestController();
                await contactRequestController.DeleteContactRequest(_contactRequestDto);
                ContactRequestId = null;
                Status = "Contact Request Deleted";
                ContactRequestState = ContactRequestEnum.NotInSystem;
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
