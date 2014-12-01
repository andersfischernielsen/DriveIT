using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT_Windows_Client.ViewModels
{
    public class CarViewModel : IViewModelBase
    {
        private CarDto _carDto;

        public CarViewModel(CarDto carDto)
        {
            _carDto = carDto;
        }

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
