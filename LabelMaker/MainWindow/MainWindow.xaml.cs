using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using LabelMaker.Region_Select;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using Npgsql;
using Renci.SshNet;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Win32;
using MongoDB.Bson;
using System.Text.RegularExpressions;


namespace LabelMaker
{
    
    public partial class MainWindow : Window
    {
        public static string PART_DIRECTORY = @"C:\\LabelMaker\\PARTS\\";
        public static string RECORD_DIRECTORY = @"C:\\LabelMaker\\Records\\Normal";
        
        public MainWindow()
        {
            try { InitializeComponent(); }
            catch (Exception ex) { MessageBox.Show("MainWindow\n\n" + ex.ToString()); }
            #region fill comboBoxes
            Labels.fillComboBoxType(Type_comboBox);
            Labels.fillComboBoxDataRate(DataRate_comboBox);
            Labels.fillComboBoxWDM(WDM_comboBox);
            Labels.fillComboBoxWave(Wave_comboBox);
            Labels.fillComboBoxCompliance(Compliance_comboBox);
            Labels.fillComboBoxSMMM(SMMM_comboBox);
            Labels.fillComboBoxDist(Dist_comboBox);
            Labels.fillComboBoxTemp(Temp_comboBox);
            Labels.fillComboBoxBreakout(BreakoutQuantity_comboBox);
            Labels.fillComboBoxSecondEnd(SecondEnd_comboBox);
            Labels.fillComboBoxTestLabel(TestLabel_ComboBox);
            Labels.fillComboBoxCountry(Country_comboBox);
            Labels.fillComboBoxDAC(CABLE_comboBox);
            Labels.fillComboBoxRangeType(Range_Type_combobox);
            Labels.fillComboBoxLabel(label_combobox);
            fillComboBoxSearch();
            #endregion
            setRoutedCommands();
            Labels.fileLoc_statBar_rec = fileLocation_statusBarItem;
            Labels.fileLoc_statBar_rec.Content = Labels.SaveLocationProperty;
            // Location to save parts
            createDir(PART_DIRECTORY);

            // Location to save records (label info)
            createDir(RECORD_DIRECTORY);
        }

        /// <summary>
        /// Sets routed commands such as Ctrl keys and F1
        /// </summary>
        private void setRoutedCommands()
        {
            RoutedCommand CtrlOpen = new RoutedCommand();
            RoutedCommand CtrlSave = new RoutedCommand();
            RoutedCommand F1About = new RoutedCommand();
            RoutedCommand CtrlNewLine = new RoutedCommand();

            CommandBindings.Add(new CommandBinding(CtrlSave, Save_MenuItem_Click));
            CommandBindings.Add(new CommandBinding(F1About, About_MenuItem_Click));
            CommandBindings.Add(new CommandBinding(CtrlNewLine, AddLine_Button_Click));

            CtrlOpen.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            CtrlSave.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            F1About.InputGestures.Add(new KeyGesture(Key.F1));
            CtrlNewLine.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        }

        /// <summary>
        /// Generates description for single part based on values in combo boxes
        /// </summary>
        /// <returns>string of the current part's description</returns>
        private string makeSingleDescription()
        {
            string type = Type_comboBox.Text;
            string dataRate = DataRate_comboBox.Text;
            string dac = CABLE_comboBox.Text;
            string wdm = WDM_comboBox.Text;
            string wave = Wave_comboBox.Text;
            string SMMM = SMMM_comboBox.Text;
            string dist = Dist_comboBox.Text;
            string temp = Temp_comboBox.Text;
            string rangeType = Range_Type_combobox.Text;
            string compliance = Compliance_comboBox.Text;
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", type, wdm, dataRate, compliance, dac, wave, SMMM, dist, temp, rangeType);
        }

        /// <summary>
        /// Adds part info entered into the main output grid and label list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddLine_Button_Click(object sender, RoutedEventArgs e)
        {
            output_grid.ItemsSource = null;
            output_grid.ItemsSource = Labels.AddLine_Button_L(Part_textBox,
                                    Serial_textBox,
                                    makeSingleDescription(),
                                    Rev_textBox,
                                    Country_comboBox,
                                    OldPart_textBox,
                                    BreakoutQuantity_comboBox,
                                    SecondEnd_comboBox,
                                    output_grid, Range_Type_combobox);




        }



        //checks if it can auto fill part info based off part number
        private void check_for_part_info(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"C:\\LabelMaker\\PARTS\\" + Part_textBox.Text + ".json"))
            {
                Part L;
                L = JsonConvert.DeserializeObject<Part>(ReadJson(Part_textBox.Text + ".json"));

                Part_textBox.Text = L.PN;
                Type_comboBox.Text = L.TYP;
                Rev_textBox.Text = L.REV;
                DataRate_comboBox.Text = L.DAT;
                WDM_comboBox.Text = L.WDM;
                Wave_comboBox.Text = L.WL;
                SMMM_comboBox.Text = L.SMMM;
                Dist_comboBox.Text = L.DIS;
                Temp_comboBox.Text = L.TMP;
                Range_Type_combobox.Text = L.RT;
                Compliance_comboBox.Text = L.COMP;

            }
            else
            {

                Serial_textBox.Text = "Part info not found in directories";

            }
        }


        //more reliable function for splitting a string
        private List<string> crawl(string s)
        {
            List<string> list = new List<string>();


            string[] split = s.Split(' ');

            foreach (string str in split)
            {
                if (str.Length > 0)
                {
                    list.Add(str);
                }
            }

            return list;
        }

        //create a file in a directory, and include its contents
        public static void createFile(string dir, string content)
        {
            string fileName = dir;
            try
            {
                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    byte[] title = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(title, 0, title.Length);
                    //byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");
                    //fs.Write(author, 0, author.Length);
                }
                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        //create a directory if not already done so
        public static void createDir(string s)
        {
            if (Directory.Exists(s))
            {
                //nothing  
                //MessageBox.Show("Directory is already exist");
            }
            else
            {
                Directory.CreateDirectory(s);
                //MessageBox.Show("Directory has been created");
            }
        }


        public static string ReadJson(string filename)
        {
            string json = "";



            using (StreamReader r = new StreamReader(@"C:\\LabelMaker\\PARTS\\" + filename))
            {
                json = r.ReadToEnd();
            }




            return json;
        }


        //function to add parts by serial number from first to last
        private void addConsecSN_Button_Click(object sender, RoutedEventArgs e)
        {
            int serial1Bad = ValidateSerialNumber(SerialCons1_textBox.Text);
            int serial2Bad = ValidateSerialNumber(SerialCons2_textBox.Text);
            string errorSummary = "";
            if (serial1Bad != serial2Bad)
            {
                errorSummary = "Multiple problems";
            } else
            {
                switch (serial1Bad)
                {
                    case -3:
                        errorSummary = "Incorrect day";
                        break;
                    case -2:
                        errorSummary = "Invalid month";
                        break;
                    case -1:
                        errorSummary = "Bad format";
                        break;
                }
            }
            MessageBoxResult result;
            if (serial1Bad != 0 || serial2Bad != 0)
            {
                result = MessageBox.Show("Serials do not follow Sandstone Template.\n Do you still want to add them?", $"Bad serials ({errorSummary})", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Cancel) return;
            }

            

            output_grid.ItemsSource = null;
            try
            {
                output_grid.ItemsSource = Labels.addConsecSN_Button_L(
                                            SerialCons1_textBox,
                                            SerialCons2_textBox,
                                            Part_textBox,
                                            Serial_textBox,
                                            makeSingleDescription(),
                                            Rev_textBox,
                                            Country_comboBox,
                                            OldPart_textBox,
                                            BreakoutQuantity_comboBox,
                                            SecondEnd_comboBox,
                                            output_grid, Range_Type_combobox);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Error adding consecutive serial numbers. Please check your inputs.");
            }
        }

        //clear the data in the output grid
        private void clear_Button_Click(object sender, RoutedEventArgs e)
        {
            output_grid.ItemsSource = Labels.clear_content();
        }

        #region print stuff
        //functions for print buttons
        private void Print_OuterBox_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_OuterBox_Button_L();
            printPanel.IsEnabled = true;
        }

        private void Print_SFPAndOuterBox_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_SFP_Only();
            printPanel.IsEnabled = true;
        }

        private void Print_SFPPAndOuterBox_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_SFPP_Only();
            printPanel.IsEnabled = true;
        }

        private void Print_XFPAndOuterBox_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_XFP_Only();
            printPanel.IsEnabled = true;
        }

        private void Print_Copper_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_Copper_Only();
            printPanel.IsEnabled = true;
        }

        private void Print_Recode_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_Recode_B_L();
            printPanel.IsEnabled = true;
        }

        private void Print_QSFP_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_QSFP_Only();
            printPanel.IsEnabled = true;
        }

        private void Print_Custom_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_Custom_Only();
            printPanel.IsEnabled = true;
        }
        private void Print_DAC_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_DAC_Only();
            printPanel.IsEnabled = true;
        }
        private void Print_New_Outer_Box_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_New_Outer_Box_Only();
            printPanel.IsEnabled = true;
        }
        private void Print_Tray_Label_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            Labels.Print_Tray_Label_Only();
            printPanel.IsEnabled = true;
        }
        private void Print_Button_Click(object sender, RoutedEventArgs e)
        {
            string LabelNum = label_combobox.Text;// print to "file name" + LabelNum
            if (LabelNum == "")
            {
                MessageBox.Show("You must select a label before clicking PRINT.");
                return;
            }
            else
            {
                printPanel.IsEnabled = false;
                Labels.Print_LabelNum(LabelNum);
                printPanel.IsEnabled = true;
            }
        }


        private void Test_Label_Click(object sender, RoutedEventArgs e)
        {
            string LabelNum = label_combobox.Text;// print to "file name" + LabelNum
            if (LabelNum == "")
            {
                MessageBox.Show("You must select a label before clicking TEST.");
                return;
            }
            else
            {
                printPanel.IsEnabled = false;
                Labels.Print_TestLabelNum(LabelNum);
                printPanel.IsEnabled = true;
            }
        }

        //print test label
        private void test_button_click(object sender, RoutedEventArgs e)
        {
            printPanel.IsEnabled = false;
            switch (TestLabel_ComboBox.Text)
            {
                case "SFP Label":
                    Labels.Print_SFP_Only();
                    break;
                case "SFP+ Label":
                    Labels.Print_SFPP_Only();
                    break;
                case "XFP Label":
                    Labels.Print_XFP_Only();
                    break;
                case "QSFP Label":
                    Labels.Print_QSFP_Only();
                    break;
                case "Copper Label":
                    Labels.Print_Copper_Only();
                    break;
                case "Outer Box Label":
                    Labels.Print_OuterBox_Button_L();
                    break;
                default:
                    break;
            }
            printPanel.IsEnabled = true;
        }
        #endregion

        private void MainWindowInitialized(object sender, EventArgs e)
        {
            fileLocation_statusBarItem.Content = Labels.SaveLocationProperty;
        }

        #region edit buttons

        //Edit buttons open requested label template
        //Only works if user is sands
        private void Edit_OuterBox_Click(object sender, RoutedEventArgs e)
        {
            /*string file = "";
            if (LabelMaker.Region_Select.RegionSelect.UK)
            {
                file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels UK\Outside Box Label [NEW]-dual sides.btw";
            }
            else
            {
                file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\Outside Box Label [NEW]-dual sides.btw";
            }
            */
            //bool testCountryBool = LabelMaker.Region_Select.RegionSelect.UK;     //WORKS
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\Outside Box Label [NEW]-dual sides.btw";
            Process.Start(file);
        }

        private void Edit_SFP_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\SFP Private Label [NEW].btw";
            Process.Start(file);
        }

        private void Edit_SFP_Plus_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\SFPP Private Label [NEW].btw";
            Process.Start(file);
        }

        private void Edit_XFP_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\XFP Private Label [NEW].btw";
            Process.Start(file);
        }

        private void Edit_QSFP_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\QSFP Private Label.btw";
            Process.Start(file);
        }

        private void Edit_Copper_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\Copper Private Label [NEW].btw";
            Process.Start(file);
        }

        private void Edit_Recode_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\Recode Sheet.btw";
            Process.Start(file);
        }

        private void Edit_Arrow_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\SFP Private Label [ARROW].btw";
            Process.Start(file);
        }

        private void Edit_DAC_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\DAC_BAG_LABEL.btw";
            Process.Start(file);
        }
        private void Edit_New_Outer_Box_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\New_Outer_Box.btw";
            Process.Start(file);
        }
        private void Edit_Tray_Label_Click(object sender, RoutedEventArgs e)
        {
            string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\TRAY_LABEL.btw";
            Process.Start(file);
        }
        private void Edit_Label_Click(object sender, RoutedEventArgs e)
        {
            string LabelNum = label_combobox.Text;
            if (LabelNum == "")
            {
                MessageBox.Show("You must select a label before clicking EDIT.");
                return;
            }
            else
            {
                string file = @"C:\Users\" + Environment.UserName + @"\PICS Telecom\Sandstone Technologies - Sandstone Software Applications\Label God Resources\Labels US\Label_" + LabelNum + ".btw";
                Process.Start(file);
            }
            
        }
        #endregion

        #region database
        //setup for database
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private string sql = null;
        string connstring = String.Format("Server={0};" + "User Id={1};Database={2}; Integrated Security = True;", "US-SANDSQL-01.picstelecom.com", "sands@PICSTELECOM.COM", "postgres");

        //function that opens and selects data from database
        private void Database_Load()
        {
            // Load the part from the database
            string partText = Part_textBox.Text;

            if (partText.Length < 1)
            {
                MessageBox.Show("Enter a part before loading from the database.", "No Part Entered");
                return;
            }

            SandstormMongoDriver sandstormMongoDriver = new SandstormMongoDriver(SandstormMongoDriver.CONNECTION_STRING);
            BsonDocument doc = sandstormMongoDriver.GetPartBson(partText);

            

            if (doc == null)
            {
                MessageBox.Show(partText + " could not be found in the database.");
                return;
            }
            string result_partType       = (string) doc["Form Factor"];
            string result_dataRate       = (string) doc["Generic Rate"];
            string result_wdm            = (string) ((doc["WDM"] == BsonNull.Value) ? "" : doc["WDM"]);
            string result_wavelength     = (string)((doc["Full Wavelength"] == BsonNull.Value) ? "" : doc["Full Wavelength"]);
            string result_media          = (string) ((doc["Media"] == BsonNull.Value) ? "" : doc["Media"]);
            double result_distance       = (double) ((doc["Distance"] == BsonNull.Value) ? 0 : doc["Distance"]);
            string result_distance_units = (string) ((doc["Distance Units"] == BsonNull.Value) ? 0 : doc["Distance Units"]);
            string result_temp           = (string) ((doc["Full Temp"] == BsonNull.Value) ? "" : doc["Full Temp"]);

            // Do intermediate calculations to turn values from database into proper textbox fields
            string full_distance = (result_distance != 0) ? result_distance.ToString() + result_distance_units.ToUpper()[0] : "";
            string full_media = (result_media.ToUpper()).Replace("F", "");
            string full_temp = (result_temp).ToUpper();


            // Enter part information into respective fields
            Type_comboBox.SelectedItem = result_partType.Trim() ;
            DataRate_comboBox.SelectedItem = result_dataRate.Trim();
            WDM_comboBox.SelectedItem = result_wdm.Trim();
            Wave_comboBox.SelectedItem = result_wavelength.Trim();
            SMMM_comboBox.SelectedItem = full_media.Trim();
            Dist_comboBox.SelectedItem = full_distance.Trim();
            Temp_comboBox.SelectedItem = full_temp.Trim();
        }
        #endregion
        // Defines whether or not a serial number follows the Sandstone Template
        public int ValidateSerialNumber(string serial)
        {
            const string serialPattern = @"^([A-Z]+)(\d{2})(\d{2})(\d{2})(\d{4})$";
            Regex serialRegex = new Regex(serialPattern, RegexOptions.Compiled);
            MatchCollection serialMatches = serialRegex.Matches(serial);
            if (serialMatches.Count < 1)
            {
                // -1 Meaning that ther serial is not a Sandstone Template
                return -1;
            }
            GroupCollection groups = serialMatches[0].Groups;
            string manufacturer_piece = groups[1].Value;
            string year_piece = groups[2].Value;
            string month_piece = groups[3].Value;
            string day_piece = groups[4].Value;
            string id_piece = groups[5].Value;

            int[] days_in_months = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int month_num = Int32.Parse(month_piece);
            int day_num = Int32.Parse(day_piece);

            if (month_num < 0 || month_num > 12)
            {
                return -2;
            }

            int correct_num_days = days_in_months[month_num - 1];

            if (day_num > correct_num_days)
            {
                return -3;
            }

            return 0;



           






        }
        

        #region excel load
        //fills data grid with all label information from an excel file
        private void Load_General_Items()
        {
            //username of the account logged in
            string username = Environment.UserName;
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlBook;
            Microsoft.Office.Interop.Excel.Range xlRange;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBook = xlApp.Workbooks.Open("C:\\Users\\" + username + "\\PICS Telecom\\Sandstone Technologies - Sandstone Software Applications\\Label God Resources\\Labels US\\label maker main window data.xlsx");
                /*xlBook = xlApp.Workbooks.Open("C:\\label maker stuff\\label maker main window data.xlsx");*/
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
                load_grid.ItemsSource = dt.DefaultView;

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
        #endregion

        private void LoadData_Button_Click(object sender, RoutedEventArgs e)
        {
            ExcelLoaded = false;
            Database_Load();
        }

        #region double click grid
        //double click to load data into text/combo box
        //works for both database load and excel load
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;

            DataRowView rowview = load_grid.SelectedItem as DataRowView;

            int rowIndex = load_grid.Items.IndexOf(load_grid.SelectedCells[0].Item);

            if (ExcelLoaded == true)
            {
                for (int i = 0; i < load_grid.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        Part_textBox.Text = rowview[i].ToString();
                    }
                    else if (i == 1)
                    {
                        Type_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 2)
                    {
                        DataRate_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 3)
                    {
                        WDM_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 4)
                    {
                        Wave_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 5)
                    {
                        SMMM_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 6)
                    {
                        Dist_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 7)
                    {
                        Temp_comboBox.Text = rowview[i].ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < load_grid.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        Part_textBox.Text = rowview[i].ToString();
                    }
                    else if (i == 5)
                    {
                        Type_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 13)
                    {
                        DataRate_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 14)
                    {
                        WDM_comboBox.Text = rowview[i].ToString();
                    }
                    else if (i == 6)
                    {
                        string a = rowview[i].ToString();
                        if (a == "N/A" || a == "null")
                        {
                            Wave_comboBox.Text = "";
                        }
                        else
                        {
                            Wave_comboBox.Text = a;
                        }
                    }
                    else if (i == 4)
                    {
                        string a = rowview[i].ToString();
                        SMMM_comboBox.Text = a.Split('F')[0];
                    }
                    else if (i == 7)
                    {
                        string a = rowview[i].ToString();
                        if (a.Contains("k"))
                        {
                            Dist_comboBox.Text = a.Split('k')[0] + "K";
                        }
                        else if (a.Contains("m"))
                        {
                            Dist_comboBox.Text = a.Split('m')[0] + "M";
                        }
                        else
                        {
                            Dist_comboBox.Text = a;
                        }
                    }
                    else if (i == 11)
                    {
                        string a = rowview[i].ToString();
                        if (a == "")
                        {
                            Temp_comboBox.Text = rowview[i].ToString();
                        }
                        else
                        {
                            Temp_comboBox.Text = rowview[i].ToString() + "-TEMP";
                        }
                    }
                }
            }
        }
        #endregion

        //creates search bar
        private void Search_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format(Search_comboBox.Text + " LIKE '%{0}%'", Search_textBox.Text);
            load_grid.ItemsSource = dv;
        }


        //opens the form to add a part to the database
        private void AddPart_Button_Click(object sender, RoutedEventArgs e)
        {
            Form1 AddNewPart = new Form1();
            AddNewPart.Show();
        }

        bool ExcelLoaded = false; //bool to see if data is loaded by excel or database
        private void LoadExcel_Button_Click(object sender, RoutedEventArgs e)
        {
            ExcelLoaded = true;
            Load_General_Items();
        }

        //save part to directory in C drive
        private void Save_Part_Button_Click(object sender, RoutedEventArgs e)
        {
            Part L = new Part(Part_textBox.Text, Type_comboBox.Text, Rev_textBox.Text, DataRate_comboBox.Text, WDM_comboBox.Text, Wave_comboBox.Text, SMMM_comboBox.Text, Dist_comboBox.Text, Temp_comboBox.Text, Range_Type_combobox.Text, Compliance_comboBox.Text);
            String LJson = JsonConvert.SerializeObject(L);
            createFile(@"C:\\LabelMaker\\PARTS\\" + L.PN + ".json", LJson);
            //createFile(@"C:\\Users\\sands\\PICS Telecom\\Sandstone Technologies -Sandstone Software Applications\\Label God Resources\\PARTS" + L.PN + ".json", LJson);
            
        }
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Part_textBox.Text = "";
            Serial_textBox.Text = "";
            Type_comboBox.Text = "";
            Rev_textBox.Text = "";
            DataRate_comboBox.Text = "";
            WDM_comboBox.Text = "";
            Wave_comboBox.Text = "";
            SMMM_comboBox.Text = "";
            Dist_comboBox.Text = "";
            Temp_comboBox.Text = "";
            Range_Type_combobox.Text = "";
            Country_comboBox.SelectedItem = "China";
            OldPart_textBox.Text = "";
            BreakoutQuantity_comboBox.Text = "";
            SecondEnd_comboBox.Text = "";
            CABLE_comboBox.Text = "";
            SerialCons1_textBox.Text = "";
            SerialCons2_textBox.Text = "";
            label_combobox.Text = "";
            TestLabel_ComboBox.Text = "";
            output_grid.ItemsSource = Labels.clear_content();
        }

        private void label_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fix error
        }
        private void Wave_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fix error
        }
        private void Country_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fix error
        }

        private void label_combobox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //fix error
        }

        private void Load_Info_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                StreamReader fileReader = new StreamReader(fileName);
                // Consume the first line with headers
                fileReader.ReadLine();
                List<LabelInfo> partEntries = new List<LabelInfo>();
                // Readlines until the end of the file
                output_grid.Items.Refresh();
                while (fileReader.Peek() >= 0)
                {
                    try
                    {
                        String line = fileReader.ReadLine();
                        String[] pieces = line.Split(',');
                        LabelInfo currentPart = new LabelInfo(
                                Int32.Parse(pieces[0]),
                                pieces[1],
                                pieces[2],
                                pieces[3],
                                pieces[4],
                                pieces[5],
                                pieces[6],
                                pieces[7],
                                pieces[8],
                                pieces[9]
                            );

                        partEntries.Add(currentPart);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("File has the wrong format.\n\n" + exception.Message);
                        return;
                    }
                }
                Labels.label_list = partEntries;
                output_grid.ItemsSource = Labels.label_list;
                output_grid.Items.Refresh();
            }
        }
    }
}