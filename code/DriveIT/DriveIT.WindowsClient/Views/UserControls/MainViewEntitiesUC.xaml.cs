using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DriveIT.WindowsClient.ViewModels;

namespace DriveIT.WindowsClient
{
	/// <summary>
	/// Interaction logic for MainViewEntitiesUC.xaml
	/// </summary>
	public partial class MainViewEntitiesUC : UserControl
	{
		public MainViewEntitiesUC()
		{
			this.InitializeComponent();
            var temp = (ControlTemplate)FindResource("CarEntitiesUCTemplate");
		    var temp1 = FindName("CarEntitiesUCTemplate");
		    Console.WriteLine("hej");
            //todo find out how to reference datagrid inside template http://stackoverflow.com/questions/19116327/accessing-a-control-inside-a-controltemplate
		}
	}
}