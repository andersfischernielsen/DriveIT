using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Navigation;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;

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
    }
}
