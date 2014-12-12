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
            GenerateBestSellingEmployeeRank();

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

        public string JobTitle
        {
            get
            {
                return _employeeDto.JobTitle;
            }
            set
            {
                _employeeDto.JobTitle = value;
                NotifyPropertyChanged("JobTitle");
            }
        }

        private string _bestSellingEmployeeRank = "";
        public string BestSellingEmployee
        {
            get
            {
                return _bestSellingEmployeeRank;
            }
            set
            {
                _bestSellingEmployeeRank = value;
                NotifyPropertyChanged("BestSellingEmployee");
            }
        }

        /// <summary>
        /// Sums all sales on employees and get this employees index on the ordered list - and thereby getting the logged in employees rank.
        /// </summary>
        private async void GenerateBestSellingEmployeeRank()
        {
            var temp = await (new SaleController().ReadSaleList());
            var sells = temp
                        .GroupBy(a => a.EmployeeId)
                        .Select(a => new { Amount = a.Sum(b => b.Price), EmployeeId = a.Key })
                        .OrderByDescending(a => a.Amount)
                        .ToList();
            var thisEmployee = sells.Find(i => i.EmployeeId == _employeeDto.Id);
            BestSellingEmployee = "" + (sells.IndexOf(thisEmployee)+1);
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
