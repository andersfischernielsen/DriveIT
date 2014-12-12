using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;

namespace DriveIT.WindowsClient.Views
{
    /// <summary>
    /// Interaction logic for PasswordCreationView.xaml
    /// </summary>
    public partial class PasswordCreationView : Window
    {
        public PasswordCreationView()
        {
            InitializeComponent();
            PasswordCreationViewModel vm = new PasswordCreationViewModel(new CustomerDto(){Email = "DesignViewEMAIL IF THIS COMES UP - FIX IT!"}); // this creates an instance of the ViewModel
            DataContext = vm; // this sets the newly created ViewModel as the DataContext for the View
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }
        public PasswordCreationView(PasswordCreationViewModel vm)
        {
            InitializeComponent();
            DataContext = vm; // this sets ViewModel as the DataContext for the View
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(() => this.Close());
        }
    }
}
