using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MyPath = System.IO.Path;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using Forms = System.Windows.Forms;
using LabelMaker.Accedian;
using System.Data;

namespace LabelMaker
{
    public partial class Labels : Window
    {
        
        public static bool autoSave = false;
        public static int currentRow = 1;
        public static List<LabelInfo> label_list = new List<LabelInfo>();
        public static string mainfilepathandsubfolder = ""; 
        public readonly static string filename = "LabelInfo.txt"; //This file is the database that Bartender uses to create labels
        public static MainWindow mw = new MainWindow();
		public static AccedianView acc = new AccedianView();
        #region Status Bar Variables
        public static StatusBarItem fileLoc_statBar_rec;
        public static StatusBarItem fileLoc_statBar_oo;
        public static StatusBarItem fileSav_statBar_rec;
        public static StatusBarItem fileSav_statBar_oo;
        public static MenuItem as_select_rec;
        public static MenuItem as_select_oo;
        public static StatusBarItem as_statBar_rec;
        public static StatusBarItem as_statBar_oo;
        #endregion

        public static string SaveLocationProperty
        {
            get
            {
                return mainfilepathandsubfolder;
            }
            set
            {
                mainfilepathandsubfolder = value;
                updateLocation_statusBar(value);
            }
        }
        
        public static void updateLocation_statusBar(string value)
        {
            mw.fileLocation_statusBarItem.Content = value;
        }

        /// <summary>
        /// /// Adds part info entered into the output grid and label list
        /// </summary>
        /// <param name="Part_tb">text box for part number</param>
        /// <param name="Serial_tb">text box for serial number</param>
        /// <param name="Desc">description of part</param>
        /// <param name="Rev_tb">revision of part</param>
        /// <param name="Country_cb">country of origin of part</param>
        /// <param name="output_g">output grid that info will get added to</param>
        public static List<LabelInfo> AddLine_Button_L(TextBox Part_tb, TextBox Serial_tb, string Desc, TextBox Rev_tb, ComboBox Country_cb, TextBox OldPart_tb, ComboBox Breakout_cb, ComboBox Second_cb, DataGrid output_g, ComboBox RateType_cb)
        {
            
            //gets part info that was entered
            string LN_s    = currentRow.ToString();
            string PN_s    = Part_tb.Text;
            string SN_s    = Serial_tb.Text;
            string DES_s   = Desc;
            string REV_s   = Rev_tb.Text;
            string CNT_s   = Country_cb.Text;
            string OldPN_s = OldPart_tb.Text;
            string BRK_s = Breakout_cb.Text;
            string SE_s = Second_cb.Text;
            string RT_s = RateType_cb.Text;

            //if revision is blank, replace with a space
            if (REV_s == "")
                REV_s = " ";

            //adds current part info to the label list
            label_list.Add(new LabelInfo(currentRow, PN_s, SN_s, DES_s, REV_s, CNT_s, OldPN_s, BRK_s, SE_s, RT_s));

            Labels.currentRow++;

            Serial_tb.Text = "";
            Serial_tb.Focus();

            return label_list;
        }


        public static List<LabelInfo> addConsecSN_Button_L(TextBox SerialCons1_tb, TextBox SerialCons2_tb, TextBox Part_tb, TextBox Serial_tb, string Desc, TextBox Rev_tb, ComboBox Country_cb, TextBox OldPart_tb, ComboBox Breakout_cb, ComboBox Second_cb, DataGrid output_g, ComboBox RateType_cb )
        {
            //string PN = PartCons_tb.Text;
            string FirstSN = SerialCons1_tb.Text;
            string LastSN = SerialCons2_tb.Text;

            string baseSN = FirstSN.Substring(0, FirstSN.Length - 4);
            string FirstSN_4 = FirstSN.Substring(FirstSN.Length - 4);
            string LastSN_4 = LastSN.Substring(LastSN.Length - 4);
            int FirstSN_4_int = Convert.ToInt16(FirstSN_4);
            int LastSN_4_int = Convert.ToInt16(LastSN_4);
            int diff = LastSN_4_int - FirstSN_4_int + 1;
            //Part_tb.Text = PN;
            for (int i = FirstSN_4_int; i <= LastSN_4_int; i++)
            {
                string i_str = i.ToString().PadLeft(4, '0');
                Serial_tb.Text = baseSN + i_str;
                AddLine_Button_L(Part_tb, Serial_tb, Desc, Rev_tb, Country_cb, OldPart_tb, Breakout_cb, Second_cb, output_g, RateType_cb);
            }
            return label_list;
        }



		public static List<LabelInfo> clear_content()
        {
            label_list = new List<LabelInfo>();
            Labels.currentRow = 1;
            return label_list;
            
        }

        /// <summary>
        /// Gets all of the info for all parts from the label_list and returns it in a string
        /// </summary>
        /// <returns>string with all of the part's descriptions</returns>
        public static string getAllInfo()
        {
            string Info = "";
            string LN = "Line", PN = "Part", SN = "Serial", DES = "Description", REV = "Revision";
            string CNT = "Country", OldPN = "Old Part", BRK = "Breakout Quantity", SE = "Second End";
            Info += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", LN, PN, SN, DES, REV, CNT, OldPN, BRK, SE, Environment.NewLine);
			LabelInfo[] label_array = label_list.ToArray();
			for (int i = 1; i < currentRow && i <= label_array.Length; i++)
            {
                LN    = label_array[i - 1].LN.ToString();
                PN    = label_array[i - 1].PN;
                SN    = label_array[i - 1].SN;
                DES   = label_array[i - 1].DES;
                REV   = label_array[i - 1].REV;
                CNT   = label_array[i - 1].CNT;
                OldPN = label_array[i - 1].OLD_PN;
                BRK = label_array[i - 1].BRK;
                SE = label_array[i - 1].SE;

                Info += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", LN, PN, SN, DES, REV, CNT, OldPN, BRK, SE, Environment.NewLine);
            }
            return Info;
        }

		/// <summary>
		/// Gets all of the info from the save file and returns it in a List form.
		/// </summary>
		/// <param name="Info"> the string found in the last save file </param>
		/// <returns>List with all of the parts and serial numbers last saved</returns>
		public static List<LabelInfo> storeAllInfo(string Info)
		{
			Info = Info.Remove(0, 57);
			Info = Info.Replace(System.Environment.NewLine, ",");
			string[] info = Info.Split(',');
			for (int i = 0; i < info.Length - 1; i+=7)
			{
				int LN_s		= currentRow;
				string PN_s		= info[i + 1];
				string SN_s		= info[i + 2];
				string DES_s	= info[i + 3];
				string REV_s	= info[i + 4];
				string CNT_s	= info[i + 5];
				string OldPN_s	= info[i + 6];
                string BRK_s    = info[i + 7];
                string SE_s     = info[i + 7];
                string RT_s     = info[i + 7];

                //if revision is blank, replace with a space
                if (REV_s == "")
					REV_s = " ";

				//adds current part info to the label list

				label_list.Add(new LabelInfo(LN_s, PN_s, SN_s, DES_s, REV_s, CNT_s, OldPN_s, BRK_s, SE_s, RT_s));
				currentRow += 1;
			}

			return label_list;
		}

		public static List<LabelInfo> storeAccedianInfo(DataTable Info)
		{

			return label_list;
		}
        
        #region print stuff	
        public static void Print_OuterBox_Button_L()
        {
            try
            {
                Save_MenuItem_L();
                printFunction("OuterBox Only Batch.btbat");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Fail", MessageBoxButton.OK);
            }
        }

        public static void Print_QSFP_Only()
        {
            Save_MenuItem_L();
            printFunction("QSFPOnlyBatch.btbat");
        }

        public static void Print_Recode_B_L()
        {
            Save_MenuItem_L();
            printFunction("Recode Sheet Batch.btbat");
        }

        public static void Print_SFP_Only()
        {
            Save_MenuItem_L();
            printFunction("SFPOnlyBatch.btbat");
        }

        public static void Print_SFPP_Only()
        {
            Save_MenuItem_L();
            printFunction("SFP+OnlyBatch.btbat");
        }

        public static void Print_XFP_Only()
        {
            Save_MenuItem_L();
            printFunction("XFPOnlyBatch.btbat");
        }

		public static void Print_Copper_Only()
		{
			Save_MenuItem_L();
			printFunction("CopperOnlyBatch.btbat");
		}

        public static void Print_DAC_Only()
        {
            Save_MenuItem_L();
            printFunction("DAC_BAG_Batch.btbat");
        }

        public static void Print_LabelNum(string LabelNum)
        {
            Save_MenuItem_L();
            printFunction("label_" + LabelNum + ".btbat");
        }

        public static void Print_TestLabelNum(string LabelNum)
        {
            Save_MenuItem_L();
            printFunction("Testlabel_" + LabelNum + ".btbat");
        }
        public static void Print_New_Outer_Box_Only()
        {
            Save_MenuItem_L();
            printFunction("New_Outer_Box_Batch.btbat");
        }
        public static void Print_Tray_Label_Only()
        {
            Save_MenuItem_L();
            printFunction("Tray_Label_Batch.btbat");
        }
        public static void Print_Custom_Only()
        {
            Save_MenuItem_L();
            Forms.OpenFileDialog ofd = new Forms.OpenFileDialog();
            if (ofd.ShowDialog() == Forms.DialogResult.OK && System.IO.Path.GetExtension(ofd.FileName) == ".btw")
            {
                string fullPath = ofd.FileName;
                string filename = ofd.SafeFileName;
                string path = fullPath.Replace(filename, "");
                Forms.PrintDialog pd = new Forms.PrintDialog();
                if (pd.ShowDialog() == Forms.DialogResult.OK)
                {
                    string printer = pd.PrinterSettings.PrinterName;
                    string bat_str = "<?xml version='1.0'?>" +
                        "<XMLScript Version='2.0'>" +
                        "<Command>" +
                        "<Print ReturnPrintData='false'>" +
                        "<Format SaveAtEndOfJob='false'>" + fullPath + "</Format>" +
                        "<PrintSetup><Printer>" + printer +  "</Printer>" +
                        "<UseDatabase>true</UseDatabase>" +
                        "<RecordRange>1...</RecordRange>" +
                        "</PrintSetup></Print>" +
                        "</Command>" +
                        "</XMLScript>";
                    string custom_path = MyPath.Combine(mainfilepathandsubfolder, "custom.btbat");
                    System.IO.File.WriteAllText(custom_path, bat_str);
                    printFunction("custom.btbat");
                }
            }
            else
            {
                MessageBox.Show("Please select a BarTender Template file (.btw).");
                return;
            }
        }

        public static void printFunction(string printFile)
        {
            ProcessStartInfo print = new ProcessStartInfo();
            print.FileName = MyPath.Combine(Labels.SaveLocationProperty, printFile);
            MessageBox.Show(string.Format("File to print is: {0}", print.FileName));
            print.UseShellExecute = true;
            print.RedirectStandardOutput = false;
            Process.Start(print);
        }
        #endregion

        private void LabelsLoaded(object sender, RoutedEventArgs e)
        {
            updateLocation_statusBar(SaveLocationProperty);
        }
    }
}