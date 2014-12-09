using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

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
                NotifyPropertyChanged("ImagePath");
            }
        }
        #endregion Properties
        public void ChooseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
                //ImagePath = File.ReadAllText(@openFileDialog.FileName);
            }


        }
    }
}
