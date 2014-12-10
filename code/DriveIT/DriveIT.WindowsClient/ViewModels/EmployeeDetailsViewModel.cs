using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;

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
            GravatarLink = "http://www.gravatar.com/avatar/" + CreateMD5(_employeeDto.Email);
        }
        public EmployeeDetailsViewModel()
        {
            _employeeDto = new EmployeeDto(){Email = ""};
            GravatarLink = "http://www.gravatar.com/avatar/" + CreateMD5(_employeeDto.Email);
        }

         public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        #region Attributes

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
