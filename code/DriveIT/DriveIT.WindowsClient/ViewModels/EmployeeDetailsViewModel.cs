using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;

namespace DriveIT.WindowsClient.ViewModels
{
    public class EmployeeDetailsViewModel : IViewModelBase
    {
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

        private EmployeeDto _employeeDto;
        public EmployeeDetailsViewModel(EmployeeDto employeeDto)
        {
            _employeeDto = employeeDto;

            GravatarLink = GravatarController.CreateGravatarLink(_employeeDto.Email);
        }

        public EmployeeDetailsViewModel()
        {
            _employeeDto = new EmployeeDto(){Email = ""};
            //LoggedInRole = Role.Employee;
            GravatarLink = GravatarController.CreateGravatarLink(_employeeDto.Email);
        }

         

        #region Attributes

        public static Role LoggedInRole;

         private string _gravatarLink;
         public string GravatarLink
         {
             get { return _gravatarLink; }
             set
             {
                 _gravatarLink = value;
                 NotifyPropertyChanged("GravatarLink");
             }
         }
        public string Email
        {
            get
            {
                return _employeeDto.Email;
            }
            set
            {
                _employeeDto.Email = value;
                NotifyPropertyChanged("Email");
            }
        }


        public string FirstName
        {
            get
            {
                return _employeeDto.FirstName;
            }
            set
            {
                _employeeDto.FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return _employeeDto.LastName;
            }
            set
            {
                _employeeDto.LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public string Phone
        {
            get
            {
                return _employeeDto.Phone;
            }
            set
            {
                _employeeDto.Phone = value;
                NotifyPropertyChanged("Phone");
            }
        }
        #endregion Attributes
    }
}
