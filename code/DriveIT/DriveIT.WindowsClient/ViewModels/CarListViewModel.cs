using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT_Windows_Client.ViewModels
{
    public class CarListViewModel : IViewModelBase
    {
        public IList<CarViewModel> CarViewModels { get; set; }

        public CarListViewModel(IList<CarDto> carDtos)
        {
            CarViewModels = new List<CarViewModel>();
            foreach (CarDto carDto in carDtos)
            {
                CarViewModels.Add(new CarViewModel(carDto));
            }
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
