using Microsoft.Win32;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WinForms = System.Windows.Forms;


namespace LabelMaker
{
    public partial class Labels : Window
    {
        #region File menu

        ///<summary>
        ///Function that saves the label_list to a text file so BarTender can access it
        ///</summary>
        ///<param name="output">DataGrid on the Mainwindow</param>
        public static void Save_MenuItem_L() 
        {
            string info = getAllInfo();
            string mainFolder = SaveLocationProperty;
            string save_file_name = filename;
            string save_file_name_path = Path.Combine(mainFolder, save_file_name);
            File.WriteAllText(save_file_name_path, info);
            
        }

		/// <summary>
		/// Function that loads the label_list from a text file so user can see last saved label info
		/// </summary>
		public static string Load_MenuItem_L()
		{
			string mainFolder = SaveLocationProperty;
			string save_file_name = filename;
			string save_file_name_path = Path.Combine(mainFolder, save_file_name);
			string info = File.ReadAllText(save_file_name_path);
			return info;

		}

        //Change Location
        public static void ChangeLocation_MenuItem_L()
        {
            string msg1 = string.Format("Current  save / BT location is:\n{0}\n\nDo you want to change this locations?", SaveLocationProperty);
            MessageBoxResult res = MessageBox.Show(msg1, "Change save location", MessageBoxButton.YesNo, MessageBoxImage.Information);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    WinForms.FolderBrowserDialog fbd = new WinForms.FolderBrowserDialog();
                    fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                    fbd.ShowDialog();
                    if (string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        string msg2 = string.Format("Location not changed\n\nSve location is: \n{0}", SaveLocationProperty);
                        MessageBox.Show(msg2, "Save location", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        SaveLocationProperty = fbd.SelectedPath;
                        string msg3 = string.Format("New save / BT location is:\n{0}", SaveLocationProperty);
                        MessageBox.Show(msg3, "Confirm save location", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                default:
                    break;
            }
            fileLoc_statBar_rec.Content = SaveLocationProperty.ToString();
            //fileLoc_statBar_oo.Content = SaveLocationProperty.ToString();
            //mw.fileLocation_statusBarItem.Content = SaveLocationProperty.ToString();
            //bw.fileLocation_statusBarItem.Content = SaveLocationProperty.ToString();
        }
        #endregion
        
        #region Window stuff
        //Exit
        public static void Exit_MenuItem_L()
        {
            //Environment.Exit(0);
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Exit_MenuItem_L();
        }

        private void openMW_L(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            mw.Visibility = Visibility.Visible;
        }

        private void Rec_Window_L(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            mw.Visibility = Visibility.Visible;
        }
        #endregion

        #region Help menu
        public static void About_MenuItem_L()
        {
            /*About abt = new Test3.About();
            abt.Owner = this;
            abt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            abt.ShowDialog();*/
            MessageBox.Show("Sandstone Technologies \nLabel God \nVersion 3.4",
                    "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void HotKey_MenuItem_L()
        {
            MessageBox.Show("Ctrl+N\t\tAdd new line" +
                            "Ctrl+S\t\tSave file\n" +
                            "F1\t\tOpen about window\n",
                "Hot Key List", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}