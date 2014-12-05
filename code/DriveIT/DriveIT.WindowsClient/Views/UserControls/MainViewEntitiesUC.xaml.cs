using System;
using System.Windows.Controls;

namespace DriveIT.WindowsClient.Views.UserControls
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