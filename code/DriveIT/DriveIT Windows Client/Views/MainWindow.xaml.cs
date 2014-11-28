using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DriveIT_Windows_Client.Controllers;

namespace DriveIT_Windows_Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new CarController();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var entityWindow = new Views.EntityWindow();
            entityWindow.Show(); // works
        }

        private void RequestForContactButtonClick(object sender, RoutedEventArgs e)
        {
            ControlTemplate Temp = (ControlTemplate)FindResource("UserControlRequestForCar");
            EntitiesUC.Template = Temp;
        }

        private void CarsButtonClick(object sender, RoutedEventArgs e)
        {
            ControlTemplate Temp = (ControlTemplate)FindResource("UserControlCars");
            EntitiesUC.Template = Temp;
            Console.WriteLine();
        }
    }
}
