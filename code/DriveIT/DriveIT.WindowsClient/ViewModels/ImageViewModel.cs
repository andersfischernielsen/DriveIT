using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.WindowsClient.ViewModels
{
    public class ImageViewModel : IViewModelBase
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

        public ImageViewModel()
        {
        }

        public ImageViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }

        #region Properties
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                NotifyPropertyChanged("Status");
            }
        }
        //private Image _image;
        //public Image Image
        //{
        //    get { return _image; }
        //    set
        //    {
        //        _image = value;
        //        NotifyPropertyChanged("Image");
        //    }
        //}
        #endregion Properties
    }
}
