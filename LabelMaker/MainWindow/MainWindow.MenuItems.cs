using LabelMaker.Accedian;
using System;
using System.Windows;

namespace LabelMaker
{
    public partial class MainWindow : Window
    {
		public static MainWindow mainwindow;

        #region File menu
        private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Labels.Save_MenuItem_L();
        }

		private void Load_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			output_grid.ItemsSource = null;
			output_grid.ItemsSource = Labels.storeAllInfo(Labels.Load_MenuItem_L());
		}

        private void ChangeLocation_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Labels.ChangeLocation_MenuItem_L();
        }

        //Exit

        private void Exit_MenuItem_Click(object sender, EventArgs e)
        {
            Labels.Exit_MenuItem_L();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Labels.Exit_MenuItem_L();
        }

		private void Accedian_View_Click(object sender, RoutedEventArgs e)
		{

			string username = Environment.UserName;
			if (AccedianView.accedianview != null)
			{
				AccedianView.accedianview.Show();
			}
			else
			{
				AccedianView.accedianview = new AccedianView();
				AccedianView.accedianview.Show();
			}
			mainwindow = this;
			this.Hide();
		}
		#endregion

		#region Help menu
		private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Labels.About_MenuItem_L();
        }

        private void HotKey_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Labels.HotKey_MenuItem_L();
        }
        #endregion
    }
}
