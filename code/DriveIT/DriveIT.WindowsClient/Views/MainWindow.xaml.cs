using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;
using System.Windows;

namespace DriveIT.WindowsClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(EmployeeDto loggedInEmployee)
        {
            InitializeComponent();
            EmployeeDetailsGrid.DataContext = new EmployeeDetailsViewModel(loggedInEmployee);
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void CloseWindowPopUp(object sender, CancelEventArgs e)
        {
            if (Visibility != Visibility.Visible) return;

            var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
