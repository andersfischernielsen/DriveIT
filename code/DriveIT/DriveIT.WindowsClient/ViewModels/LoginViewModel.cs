﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DriveIT.WindowsClient.Controllers;
using DriveIT.WindowsClient.Views;

namespace DriveIT.WindowsClient.ViewModels
{
    public class LoginViewModel : IViewModelBase
    {
        public Action CloseAction { get; set; }

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



        public async void Login()
        {
            try
            {
                await DriveITWebAPI.Login(Username, Password);
                Status = "Login successful!";
                var window = new MainWindow();
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

        public void SkipLogin()
        {
            Status = "Login Skipped!";
            var window = new MainWindow();
            window.Show();
            CloseAction.Invoke();
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
