using System;
using System.ComponentModel;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    /// <summary>
    /// The LoginViewModel is the Viewmodel in the MVVM pattern for the Login view.
    /// It contains the logic of sending usernames and passwords to the API and logging ind.
    /// </summary>
    public class LoginViewModel : IViewModelBase
    {
        public Action CloseAction { get; set; }

        /// <summary>
        /// The empty constructor, setting all property strings to "";
        /// </summary>
        public LoginViewModel()
        {
            Status = "";
            Username = "";
            Password = "";
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        /// <summary>
        /// Tries to login with the given Username and Password. Changes the status to reflect the process.
        /// </summary>
        public async void Login()
        {
            try
            {
                Status = "Logging in...";
                await DriveITWebAPI.Login(Username, Password);
                Status = "Login successful!";
                var window = new MainWindow(await new EmployeeController().ReadEmployee(Username));
                window.Show();
                CloseAction.Invoke();
            }
            catch (Exception)
            {
                Status = "Username or password was invalid. Try again...";
                Password = "";
                Username = "";
            }
        }

        /// <summary>
        /// This method skips login and is used for debugging, it uses the administrator user admin@driveit.dk.
        /// </summary>
        public async void SkipLogin()
        {
            try
            {
                Status = "Trying to skip login...";
                await DriveITWebAPI.Login("admin@driveit.dk", "4dmin_Password");
                Status = "Login skipped!";
                var window = new MainWindow(await new EmployeeController().ReadEmployee("admin@driveit.dk"));
                window.Show();
                CloseAction.Invoke();
            }
            catch (Exception)
            {
                Status = "Username or password was invalid. Try again...";
                Password = "";
                Username = "";
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
