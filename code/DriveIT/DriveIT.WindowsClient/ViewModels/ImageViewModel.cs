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
            ImagePaths = new List<string>();
        }

        public ImageViewModel(string imagePath)
        {
            ImagePaths = new List<string>();
            ImagePaths.Add(imagePath);
            ImagePath = imagePath;
        }

        #region Properties
        public List<string> ImagePaths { get; set; }

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
        public async void ChooseFile()
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
