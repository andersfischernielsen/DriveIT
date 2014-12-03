using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace DriveIT.WindowsClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var entityWindow = new EntityCarWindow();
            entityWindow.Show(); // works
        }

        private void RequestsForContactsClick(object sender, RoutedEventArgs e)
        {
            EntitiesTabControl.SelectedIndex = 0;
            PowerToolsbarTabControl.SelectedIndex = 0;
        }

        private void CarsClick(object sender, RoutedEventArgs e)
        {
            EntitiesTabControl.SelectedIndex = 1;
            PowerToolsbarTabControl.SelectedIndex = 1;
        }

        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            EntitiesTabControl.SelectedIndex = 2;
            PowerToolsbarTabControl.SelectedIndex = 2;
        }

        private void CustomersClick(object sender, RoutedEventArgs e)
        {
            EntitiesTabControl.SelectedIndex = 3;
            PowerToolsbarTabControl.SelectedIndex = 3;
        }

        private void EmployeesClick(object sender, RoutedEventArgs e)
        {
            EntitiesTabControl.SelectedIndex = 4;
            PowerToolsbarTabControl.SelectedIndex = 4;
        }
    }
}
