using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
            EntitiesUC.Template = (ControlTemplate)EntitiesUC.FindResource("RequestForContactEntititesUCTemplate");
            PowerToolsBarUC.Template = (ControlTemplate)PowerToolsBarUC.FindResource("RequestForContactEntitiesPowerToolBarUC");
        }

        private void CarsClick(object sender, RoutedEventArgs e)
        {
            EntitiesUC.Template = (ControlTemplate)EntitiesUC.FindResource("CarEntitiesUCTemplate");
            PowerToolsBarUC.Template = (ControlTemplate)PowerToolsBarUC.FindResource("CarEntitiesPowerToolBarUC");
        }
    }
}
