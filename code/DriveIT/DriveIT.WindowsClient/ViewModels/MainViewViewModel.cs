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
        /// <summary>
        /// Getters and setters for the attributes of a SaleDTO while notifying view.
        /// </summary>
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
        /// <summary>
        /// Constructor for MainViewViewModel, that intitiates TabIndex at 0, causing the program to start on the first tab.
        /// </summary>
        public MainViewViewModel()
        {
            TabIndex = 0;
        }

        private int _tabIndex;
        /// <summary>
        /// Gets and sets the TabIndex depending on the selected tab, and notifies the view.
        /// </summary>
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
