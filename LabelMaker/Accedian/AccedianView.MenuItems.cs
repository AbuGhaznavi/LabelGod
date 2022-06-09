using LabelMaker.Region_Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using Renci.SshNet;
using MongoDB.Bson;
namespace LabelMaker.Accedian
{
	public partial class AccedianView : Window
	{
		public static int currentRow = 1;
		public static List<AccedianInfo> accedian_list = new List<AccedianInfo>();

		#region File menu
		/// <summary>
		/// Call the Function located in AccedianView.xaml.cs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			Save_MenuItem_L();
		}

		private void ChangeLocation_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			Labels.ChangeLocation_MenuItem_L();
		}


		private void ChangeDirectory_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.mainwindow.Show();
			this.Hide();
		}

		//Exit
		private void Exit_MenuItem_Click(object sender, EventArgs e)
		{
			Labels.Exit_MenuItem_L();
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

		private void HowToUse_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			string filename = "C:\\Users\\" + username +  "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Versions\\How to Use Label Maker Accedian View.pdf";
			System.Diagnostics.Process.Start(filename);
		}
        #endregion

        #region Load Accedian Database

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private string sql = null;
        string connstring = String.Format("Server={0};" + "User Id={1};Database={2}; Integrated Security = True;", "US-SANDSQL-01.picstelecom.com", "jjohnson@PICSTELECOM.COM", "postgres");


		public class AccedianPart
		{
			public string PartNumber { get; set; }
			public string PartDescription { get; set; }
			public string LabelDescription { get; set; }

			public AccedianPart(string _pn, string _pdesc, string _ldesc)
            {
				this.PartNumber = _pn;
				this.PartDescription = _pdesc;
				this.LabelDescription = _ldesc;
            }

		}

		private void Database_Load()
        {
            
            try
            {
				SandstormMongoDriver driver = new SandstormMongoDriver(SandstormMongoDriver.CONNECTION_STRING);
				
				// Set up the intiial column names
				DataView dataGridView = new DataView();
				List<AccedianPart> accedianParts = driver.GetAccedianParts();
				


				input_grid.ItemsSource = accedianParts;
                // input_grid.ItemsSource = dt.DefaultView;

				
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Fail", MessageBoxButton.OK);
                
            }
        }

		private void Load_AccedianItem()
		{
			//username of the account logged in
			string username = Environment.UserName;
			
			string[] pieces = username.Split('.');
			string valid_name = username;
			string selected = "";
			if (pieces.Length > 1)
            {
				valid_name = pieces[0];
            }
			string domain = valid_name + "." + Environment.UserDomainName;
			string username_location = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\AccedianExcel.xlsx";
			string domain_location = "C:\\Users\\" + domain + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\AccedianExcel.xlsx";
			string valid_location = "C:\\Users\\" + valid_name + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\AccedianExcel.xlsx";

			if (!File.Exists(domain_location))
			{
				selected = domain_location;
				Console.WriteLine("File exists!");
			}
			else if (File.Exists(username_location))
			{
				selected = username_location;
				Console.WriteLine("File exists!");
			}
			else if (!File.Exists(valid_location))
			{
				selected = valid_location;
				Console.WriteLine("File exists!");
			}



			Microsoft.Office.Interop.Excel.Application xlApp;
			Microsoft.Office.Interop.Excel.Workbook xlBook;
			Microsoft.Office.Interop.Excel.Range xlRange;
			Microsoft.Office.Interop.Excel.Worksheet xlSheet;
			DataTable dt = new DataTable();
			try
			{
				xlApp = new Microsoft.Office.Interop.Excel.Application();
				xlBook = xlApp.Workbooks.Open("C:\\Users\\" + selected + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\AccedianExcel.xlsx");
				xlSheet = xlBook.Worksheets["Sheet1"];
				xlRange = xlSheet.UsedRange;
				DataRow row = null;
				//Iteration through the rows and columns. Can optimize. Chose not to.
				for (int i = 1; i <= xlRange.Rows.Count; i++)
				{
					if (i != 1)
						row = dt.NewRow();
					for (int j = 1; j <= xlRange.Columns.Count; j++)
					{
						if (i == 1)
							dt.Columns.Add(xlRange.Cells[1, j].value);
						else
							row[j - 1] = xlRange.Cells[i, j].value;
					}
					if (row != null)
						dt.Rows.Add(row);
				}
				input_grid.ItemsSource = dt.DefaultView;

				//Close and Release the excel application to stop the process from running in the background. 
				xlBook.Close(true);
				xlApp.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Loads the Accedian Part Number and Description from AccedianExcel.xlsx file into the input grid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Load_AccedianItem_Click(object sender, RoutedEventArgs e)
		{
			
            ExcelLoad = false;
            Database_Load();
		}

        bool ExcelLoad = false;
        private void LoadExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelLoad = true;
            Load_AccedianItem();
        }

        #endregion

        #region Output Buttons
        /// <summary>
        /// Reads the part number, part and label description, and the quantity of labels desired. 
        /// Leaves serial number field blank, and adds the rest of the information as many times as desired to the output field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddQuantity_Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				output_grid.ItemsSource = null;
				string PN_s = Part_textBox.Text;
				string SN_s = "";
				string PDes_s = PartDesc_textBox.Text;
				string LDes_s = LabelDesc_textBox.Text;
                string CNT_s = Country_comboBox.Text;
                string OLD_s = OldPart_textBox.Text;
				int Quantity = Int32.Parse(Quantity_textBox.Text);

				for (int i = 0; i < Quantity; i++)
				{
					accedian_list.Add(new AccedianInfo(currentRow, SN_s, PN_s, PDes_s, LDes_s, CNT_s, OLD_s));
					currentRow++;
				}
				output_grid.ItemsSource = accedian_list;
			}
			catch (Exception)
			{
				MessageBox.Show("Error adding Quantity. Please check your inputs.");
			}
		}

		/// <summary>
		/// Reads the first and last serials, then takes the last 4 digits of both those serials into a substring
		/// Uses the substring to add consecutive serials with the part number, part and label description to the output grid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addConsecSN_Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string PN_s = Part_textBox.Text;
				string PDes_s = PartDesc_textBox.Text;
				string LDes_s = LabelDesc_textBox.Text;
				string FirstSN = SerialCons1_textBox.Text;
				string LastSN = SerialCons2_textBox.Text;
				string baseSN = FirstSN.Substring(0, FirstSN.Length - 4);
				string FirstSN_4 = FirstSN.Substring(FirstSN.Length - 4);
				string LastSN_4 = LastSN.Substring(LastSN.Length - 4);
				int FirstSN_4_int = Convert.ToInt16(FirstSN_4);
				int LastSN_4_int = Convert.ToInt16(LastSN_4);
				int diff = LastSN_4_int - FirstSN_4_int + 1;

				for (int i = FirstSN_4_int; i <= LastSN_4_int; i++)
				{
					string i_str = i.ToString().PadLeft(4, '0');
					Serial_textBox.Text = baseSN + i_str;
					AddSerial_Button_Click(sender, e);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Error adding consecutive serial numbers. Please check your inputs.");
			}
		}

		/// <summary>
		/// Adds only one serial number to the output grid with the corressponding part number, label and part description.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddSerial_Button_Click(object sender, RoutedEventArgs e)
		{
			output_grid.ItemsSource = null;
			string PN_s = Part_textBox.Text;
			string SN_s = Serial_textBox.Text;
			string PDes_s = PartDesc_textBox.Text;
			string LDes_s = LabelDesc_textBox.Text;
            string CNT_s = Country_comboBox.Text;
            string OLD_s = OldPart_textBox.Text;

			accedian_list.Add(new AccedianInfo(currentRow, SN_s, PN_s, PDes_s, LDes_s, CNT_s, OLD_s));
			currentRow++;
			Serial_textBox.Text = "";
			Serial_textBox.Focus();

			output_grid.ItemsSource = accedian_list;
			
		}
		#endregion

		#region Select Accedian Part
		private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// Ensure row was clicked and not empty space
			DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
			if (row == null) return;

			DataRowView rowview = input_grid.SelectedItem as DataRowView;

			int rowIndex = input_grid.Items.IndexOf(input_grid.SelectedCells[0].Item);
            if (ExcelLoad == true)
            {
                for (int i = 0; i < input_grid.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        Part_textBox.Text = rowview[i].ToString();
                    }
                    else if (i == 1)
                    {
                        PartDesc_textBox.Text = rowview[i].ToString();
                    }
                    else if (i == 2)
                    {
                        LabelDesc_textBox.Text = rowview[i].ToString();
                    }
                }
            }
            else
            {
				AccedianPart selectedPart = (AccedianPart)input_grid.SelectedItem;
				//for (int i = 0; i < input_grid.Columns.Count; i++)
				//{
				//    if (i == 1)
				//    {
				//        Part_textBox.Text = rowview[i].ToString();
				//    }
				//    else if (i == 2)
				//    {
				//        PartDesc_textBox.Text = rowview[i].ToString();
				//    }
				//    else if (i == 3)
				//    {
				//        LabelDesc_textBox.Text = rowview[i].ToString();
				//    }
				//}
				Part_textBox.Text = selectedPart.PartNumber;
				PartDesc_textBox.Text = selectedPart.PartDescription;
				LabelDesc_textBox.Text = selectedPart.LabelDescription;
            }
		}
		#endregion

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Labels.Exit_MenuItem_L();
		}
	}
}

