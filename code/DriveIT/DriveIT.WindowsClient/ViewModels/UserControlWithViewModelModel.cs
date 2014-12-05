using System;
using System.ComponentModel;

namespace DriveIT.WindowsClient.ViewModels
{
	public class UserControlWithViewModelModel : INotifyPropertyChanged
	{
		public UserControlWithViewModelModel()
		{
			
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