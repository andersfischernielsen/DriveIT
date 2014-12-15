using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

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

        public int? ContactRequestId
        {
            get
            {
                    return _contactRequestDto.Id;
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
            UpdateForeignKeyLists();
        }
        public ContactRequestViewModel()
        {
            _contactRequestDto = new ContactRequestDto();
            Requested = DateTime.Now;
            ContactRequestState = ContactRequestEnum.NotInSystem;
            UpdateForeignKeyLists();
        }

        public async void UpdateForeignKeyLists()
        {
            try
            {
                _contactRequestDto =
                    await new ContactRequestController().ReadContactRequest(ContactRequestId.GetValueOrDefault());
            }
            catch (Exception)
            {
                Status = "Could not get the contact request from the DB";
            }
            try
            {
                EmployeeIdsList = (await new EmployeeController().ReadEmployeeList()).Select(i => i.Email).ToList();
            }
            catch (Exception)
            {
                Status = "Could not get list of employees.";
                EmployeeIdsList = new List<string>();
            }
        }


        
        #region ATTRIBUTES

        private bool _canUpdate = true;

        public bool CanUpdate
        {
            get
            {
                return _canUpdate;
            }
            set
            {
                _canUpdate = value;
                NotifyPropertyChanged("CanUpdate");
            }
        }

        private string _status = "";
        public string Status
        {
            get
            {
                    return _status;
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
        public static List<string> EmployeeIdsList { get; set; } 
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
            try
            {
                switch (ContactRequestState)
                {
                    case ContactRequestEnum.NotInSystem:
                        break;
                    default:
                        UpdateContactRequest();
                        break;
                }
            }
            catch (Exception)
            {

                Status = "Failed to save contact request!";
            }
        }

        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void UpdateContactRequest()
        {
            try
            {
                var contactRequestController = new ContactRequestController();
                await contactRequestController.UpdateContactRequest(_contactRequestDto);
                Status = "Contact Request Updated";
            }
            catch (Exception)
            {
                
                Status = "Failed to update contact request!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void DeleteContactRequest()
        {
            try
            {
                if (ContactRequestState != ContactRequestEnum.NotInSystem)
                {
                    var contactRequestController = new ContactRequestController();
                    await contactRequestController.DeleteContactRequest(_contactRequestDto);
                    ContactRequestId = null;
                    Status = "Contact Request Deleted";
                    CanUpdate = false;
                    ContactRequestState = ContactRequestEnum.NotInSystem;
                }
            }
            catch (Exception)
            {
                Status = "Failed to delete contact request!";
            }
        }

        public void CreateSaleFromRequest()
        {
            try
            {
                var newSale = new SaleViewModel();
                // This must be done to ensure that the Sale is in the correct SaleState.
                newSale.CarId = CarId;
                newSale.CustomerId = CustomerId;
                newSale.EmployeeId = EmployeeId;
                var window = new EntitySaleWindow { DataContext = newSale };
                window.Show();
                Status = "Created Sale from Contact Request";

            }
            catch (Exception)
            {
                
                Status = "Failed to create sale from request!";
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
