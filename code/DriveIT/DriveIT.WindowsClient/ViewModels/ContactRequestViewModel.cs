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
                try
                {
                    return _contactRequestDto.Id;
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
                CustomerIdsList = (await new CustomerController().ReadCustomerList()).Select(i => i.Email).ToList();
                EmployeeIdsList = (await new EmployeeController().ReadEmployeeList()).Select(i => i.Email).ToList();
            }
            catch (Exception)
            {
                CustomerIdsList = new List<string>();
                EmployeeIdsList = new List<string>();
            }
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

        public static List<string> CustomerIdsList { get; set; } 
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
                        CreateContactRequest();
                        break;
                    default:
                        UpdateContactRequest();
                        break;
                }
            }
            catch (Exception e)
            {

                Status = "Failed to save contact request!";
            }
        }
        /// <summary>
        /// Gets called from the view
        /// </summary>
        public async void CreateContactRequest()
        {
            try
            {
                var contactRequestController = new ContactRequestController();
                var contactRequestInDB = await contactRequestController.CreateContactRequest(_contactRequestDto);

                _contactRequestDto = contactRequestInDB;
                NotifyPropertyChanged("");

                Status = "Contact Request Created";
                ContactRequestState = ContactRequestEnum.InSystem;
            }
            catch (Exception e)
            {
                
                Status = "Failed to create contact request!";
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
            catch (Exception e)
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
                    ContactRequestState = ContactRequestEnum.NotInSystem;
                }
            }
            catch (Exception e)
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
                //Dunno if above should be moved or be there at all!
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
