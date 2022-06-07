using LabelMaker.Accedian;
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

namespace LabelMaker.Region_Select
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary
	public partial class RegionSelect : Window
	{
		// true for UK, false for US
		public static bool UK;

		public RegionSelect()
		{
			InitializeComponent();
		}

		string username = Environment.UserName;

		private void Accedian_View_Click(object sender, RoutedEventArgs e)
		{
			Labels.mainfilepathandsubfolder = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\Accedian Label Maker";
			AccedianView accedian = new AccedianView();
			accedian.Show();
			this.Close();
		}

		private void Rochester_Click(object sender, RoutedEventArgs e)
		{

			Labels.mainfilepathandsubfolder = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Labels US";
			MainWindow main = new MainWindow();
			UK = false;
			main.Show();
			this.Close();
		}

		private void UK_Click(object sender, RoutedEventArgs e)
		{
			Labels.mainfilepathandsubfolder = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Labels UK";
			MainWindow main = new MainWindow();
			UK = true;
			main.Show();
			this.Close();
		}

		private void HK_Click(object sender, RoutedEventArgs e)
		{
			Labels.mainfilepathandsubfolder = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Labels HK";
			MainWindow main = new MainWindow();
			main.Show();
			this.Close();
		}

		//Handles closing of regionselect window
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//to do
		}
	}
}
