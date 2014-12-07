using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.WindowsClient.ViewModels
{
    public class MainViewViewModel : IViewModelBase
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

        public MainViewViewModel()
        {
            TabIndex = 0;
        }

        private int _tabIndex;
        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                _tabIndex = value;
                NotifyPropertyChanged("TabIndex");
            }
        }
    }
}
