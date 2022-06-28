using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using System.Diagnostics;
using MyPath = System.IO.Path;
using Microsoft.Win32;
namespace LabelMaker.Accedian
{
	/// <summary>
	/// Interaction logic for AccedianView.xaml
	/// </summary>
	public partial class AccedianView : Window
	{

		public static AccedianView accedianview;
		public static string username = Environment.UserName;
		public static string directory = "C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Accedian\\Accedian Label Maker\\";
		// public static string directory = @"C:\Users\aghaznavi.SANDINTERN-01\source\";
		public static string save_file_name_path = directory + "AccedianInfo.txt";
		public static string ten_save_file_name_path = directory + "10Serial.txt";

		// Set a folder to save Accedian records
		public static string records_location = @"C:\LabelMaker\Records\Accedian\";
		//This file is the database that Bartender uses to read Accedian labels

		public AccedianView()
		{
			try { InitializeComponent(); }
			catch (Exception ex) { System.Windows.MessageBox.Show("AccedianView\n\n" + ex.ToString()); }
			Labels.fillComboBoxCountry(Country_comboBox);
			setRoutedCommands();
			// Create the folder for Accedian Records
			MainWindow.createDir(records_location);
		}

		/// <summary>
		/// Sets routedd commands such as Ctrl keys and F1
		/// </summary>
		private void setRoutedCommands()
		{
			RoutedCommand CtrlOpen = new RoutedCommand();
			RoutedCommand CtrlSave = new RoutedCommand();
			RoutedCommand F1About = new RoutedCommand();
			RoutedCommand CtrlNewLine = new RoutedCommand();

			CommandBindings.Add(new CommandBinding(CtrlSave, Save_MenuItem_Click));
			CommandBindings.Add(new CommandBinding(F1About, About_MenuItem_Click));
			//CommandBindings.Add(new CommandBinding(CtrlNewLine, AddLine_Button_Click));

			CtrlOpen.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
			CtrlSave.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
			F1About.InputGestures.Add(new KeyGesture(Key.F1));
			CtrlNewLine.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
		}

		/// <summary>
		/// Clear the output grid and reset the information stored in it
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clear_Button_Click(object sender, RoutedEventArgs e)
		{
			accedian_list = new List<AccedianInfo>();
			currentRow = 1;
			output_grid.ItemsSource = accedian_list;
		}



		#region print stuff

		private void Print_SFPP_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("SFPPLabel.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_SFP_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("SFP1Label.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_QSFPLR_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("QSFPLR4Label.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_QSFPSR_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("QSFPSR4Label.btbat");
			printPanel.IsEnabled = true;
		}
		private void Print_QSFPER_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("QSFPER4Label.btbat");
			printPanel.IsEnabled = true;
		}
		private void Print_SMALL_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("SmallLabel.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_BIG_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("BigLabel.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_10_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("10Labels.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_R4_BIG_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("BigLabelR4.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_R4_SFPP_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("SFPPLabelR4.btbat");
			printPanel.IsEnabled = true;
		}

		private void Print_Recode_Sheet_button_click(object sender, RoutedEventArgs e)
		{
			printPanel.IsEnabled = false;
			Save_MenuItem_L();
			printFunction("RecodeSheet.btbat");
			printPanel.IsEnabled = true;
		}

		/// <summary>
		/// Looks for batch file using 
		/// </summary>
		/// <param name="printFile">The batch file that is being called to be printed</param>
		public static void printFunction(string printFile)
		{
			ProcessStartInfo print = new ProcessStartInfo();
			print.FileName = MyPath.Combine(directory, printFile);
			System.Windows.MessageBox.Show(string.Format("File to print is: {0}", print.FileName));
			print.UseShellExecute = true;
			print.RedirectStandardOutput = false;
			Process.Start(print);
			System.Windows.MessageBox.Show("Label is being printed...\n\nPlease Wait Juanito!");

		}
		#endregion

		#region Save Output
		/// <summary>
		/// Save the information in the output grid to two different files:
		/// AccedianInfo.txt - Stores all the information of the parts
		/// 10Serial.txt - Stores all the serial numbers of the parts
		/// </summary>
		public static void Save_MenuItem_L()
		{
			string info = getAllInfo();
			string serial = getAllSerials();
			string partAndSerial = getPartNumberAndSerial();
			//File.WriteAllText(save_file_name_path, info);
			File.WriteAllText(ten_save_file_name_path, serial);

			// Save the record into the accedian folder
			File.WriteAllText(
					MyPath.Combine(records_location, partAndSerial + "_AccedianLabelInfo.txt"),
					info
				);

		}

		/// <summary>
		/// Get only the serial numbers in rows of 10 from the Accedian List.
		/// This will be used by 10Labels.btbat batch file to get the 10 labels.
		/// </summary>
		/// <returns>The serial numbers in rows of 10 from the Accedian List. </returns>
		public static string getAllSerials()
		{
			string serials = "";

			// Create the Column Header
			string One = "1", Two = "2", Three = "3", Four = "4", Five = "5", Six = "6", Seven = "7", Eight = "8", Nine = "9", Ten = "10";

			serials += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Environment.NewLine);

			// Default values to be blank until assigned.
			One = ""; Two = ""; Three = ""; Four = ""; Five = ""; Six = ""; Seven = ""; Eight = ""; Nine = ""; Ten = "";

			AccedianInfo[] accedian_array = accedian_list.ToArray();
			int columns = accedian_array.Length;
			int x = 1;
			for (int i = 0; i < columns; i++)
			{
				switch (x)
				{
					#region Serial check
					case 1:
						One = accedian_array[i].SN;
						x++;
						break;
					case 2:
						Two = accedian_array[i].SN;
						x++;
						break;
					case 3:
						Three = accedian_array[i].SN;
						x++;
						break;
					case 4:
						Four = accedian_array[i].SN;
						x++;
						break;
					case 5:
						Five = accedian_array[i].SN;
						x++;
						break;
					case 6:
						Six = accedian_array[i].SN;
						x++;
						break;
					case 7:
						Seven = accedian_array[i].SN;
						x++;
						break;
					case 8:
						Eight = accedian_array[i].SN;
						x++;
						break;
					case 9:
						Nine = accedian_array[i].SN;
						x++;
						break;
					default:
						Ten = accedian_array[i].SN;
						x++;
						serials += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Environment.NewLine);
						//Reset Values until assigned again.
						One = ""; Two = ""; Three = ""; Four = ""; Five = ""; Six = ""; Seven = ""; Eight = ""; Nine = ""; Ten = "";
						break;
						#endregion
				}
				if (i + 1 >= columns && x != 11)
				{
					serials += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Environment.NewLine);
				}
				if (x == 11)
				{
					x = 1;
				}
			}
			return serials;
		}

		public static string getPartNumberAndSerial()
        {
			return (accedian_list[0].SN.Length > 0) ?
				(accedian_list[0].PN + "_" + accedian_list[0].SN) : accedian_list[0].PN;
        }

		/// <summary>
		/// Save the Info from the output grid in the following format: Line Number, Part Number, Serial Number, Part Description, Label Description
		/// The first row is the header of the text file.
		/// </summary>
		/// <returns></returns>
		public static string getAllInfo()
		{
			string Info = "";
			string LN = "Line", PN = "Part", SN = "Serial", PDES = "Part Description", LDES = "Label Description", CNT = "Country";
			Info += string.Format("{0},{1},{2},{3},{4},{5}{6}", LN, PN, SN, PDES, LDES, CNT, Environment.NewLine);
			AccedianInfo[] accedian_array = accedian_list.ToArray();
			for (int i = 1; i < currentRow && i <= accedian_array.Length; i++)
			{
				LN = accedian_array[i - 1].LN.ToString();
				PN = accedian_array[i - 1].PN;
				SN = accedian_array[i - 1].SN;
				PDES = accedian_array[i - 1].PartDes;
				LDES = accedian_array[i - 1].LabelDes;
				CNT = accedian_array[i - 1].Country;

				Info += string.Format("{0},{1},{2},{3},{4},{5}{6}", LN, PN, SN, PDES, LDES, CNT, Environment.NewLine);
			}
			return Info;
		}

		#endregion

		/// <summary>
		/// Load the info from the last saved file and store it in the output grid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Load_Output_Click(object sender, RoutedEventArgs e)
		{
			output_grid.ItemsSource = null;
			string Info = File.ReadAllText(save_file_name_path);
			Info = Info.Remove(0, 55);
			Info = Info.Replace(System.Environment.NewLine, ",");
			string[] info = Info.Split(',');
			for (int i = 0; i < info.Length - 1; i += 6)
			{
				int LN = currentRow;
				string PN = info[i + 1];
				string SN = info[i + 2];
				string PDES = info[i + 3];
				string LDES = info[i + 4];
				string CNT = info[i + 5];
				string OLD = info[i + 6];

				accedian_list.Add(new AccedianInfo(LN, SN, PN, PDES, LDES, CNT, OLD));
				currentRow += 1;
			}
			output_grid.ItemsSource = accedian_list;
		}

		private void AccedianViewInitialized(object sender, EventArgs e)
		{

		}

		#region edit buttons
		//edit buttons open requested label templates
		//user has to be sands
		private void Edit_SFPP_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\SFP+ Private Label_Accedian.btw";
			Process.Start(file);
		}

		private void Edit_SFP_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\SFP Private Label_Accedian_1label.btw";
			Process.Start(file);
		}

		private void Edit_QSFPLR_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\QSFP Private Label_Accedian - LR4.btw";
			Process.Start(file);
		}

		private void Edit_QSFPSR_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\QSFP Private Label_Accedian - SR4.btw";
			Process.Start(file);
		}

		private void Edit_QSFPER_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\QSFP Private Label_Accedian - ER4.btw";
			Process.Start(file);
		}

		private void Edit_SMALL_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\Small Accedian labels.btw";
			Process.Start(file);
		}

		private void Edit_BIG_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\Big Accedian labels_Sheet.btw";
			Process.Start(file);
		}

		private void Edit_10_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\Accedian 10 Lables.btw";
			Process.Start(file);
		}

		private void Edit_R4_BIG_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\Big Accedian labels_rev.04.btw";
			Process.Start(file);
		}

		private void Edit_R4_SFPP_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\SFPP_Accedian_Label rev.04.btw";
			Process.Start(file);
		}

		private void Edit_RecodeSheet_button_click(object sender, RoutedEventArgs e)
		{
			string file = @"C:\Users\sands\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Accedian\Accedian Label Maker\Recode Sheet.btw";
			Process.Start(file);
		}
		#endregion

		private void Search_textBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			SandstormMongoDriver driver = new SandstormMongoDriver(SandstormMongoDriver.CONNECTION_STRING);
			var accedianParts = driver.GetAccedianParts(Search_textBox.Text);
			input_grid.ItemsSource = accedianParts;
		}

		private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
		{

		}

		private void Load_MenuItem_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() != true)
			{
				return;
			}
			string fileName = openFileDialog.FileName;
			StreamReader fileReader = new StreamReader(fileName);
			// Consume the first line with headers
			var header_line = fileReader.ReadLine();
			List<AccedianInfo> partEntries = new List<AccedianInfo>();
			// Readlines until the end of the file
			output_grid.Items.Refresh();
			while (fileReader.Peek() >= 0)
			{
				try
				{
					String line = fileReader.ReadLine();
					String[] pieces = line.Split(',');
					AccedianInfo currentPart = new AccedianInfo(
							Int32.Parse(pieces[0]),
							pieces[2],
							pieces[1],
							pieces[3],
							pieces[4],
							pieces[5]
						);

					partEntries.Add(currentPart);
				} catch (Exception exception)
                {
					MessageBox.Show("File has the wrong format.\n\n" + exception.Message);
					return;
                }
			}
			accedian_list = partEntries;
			output_grid.ItemsSource = accedian_list;
			output_grid.Items.Refresh();
		}
	}
}
