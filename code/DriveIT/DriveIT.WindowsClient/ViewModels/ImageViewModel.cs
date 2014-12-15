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
    /// <summary>
    /// The ImageViewmodel represents the filepath/url to an image, and the functionality to choose an image on your pc.
    /// </summary>
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

        /// <summary>
        /// Empty constructer
        /// </summary>
        public ImageViewModel()
        {
        }

        /// <summary>
        /// Constructor taken a string which should represent a filepath/url to an image.
        /// </summary>
        /// <param name="imagePath"></param>
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
        /// <summary>
        /// Creates a openfiledialog and waits for the user to either pick an image or close it.
        /// </summary>
        public void ChooseFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif";
                if (openFileDialog.ShowDialog() == true)
                {
                    ImagePath = openFileDialog.FileName;
                    //ImagePath = File.ReadAllText(@openFileDialog.FileName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if the imagepath is empty or whitespace.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (string.IsNullOrWhiteSpace(ImagePath)) return true;
            return false;
        }
    }
}
